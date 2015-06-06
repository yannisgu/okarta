using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Reflection;
using ConfigR;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Responses.Negotiation;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using NHibernate;
using NHibernate.Linq;
using Okarta.Data;
using Okarta.Data.Entities;
using Nancy.Authentication.Token;
using Okarta.Data.Implementation;
using Okarta.Data.Services;

namespace Okarta.Web
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        public Bootstrapper()
        {
            Config.Global.LoadScriptFile("Settings.csx");
            if (Config.Global.Get<bool>("insertTestData")) 
            { 
                var testData = JsonConvert.DeserializeObject<TestData>(File.ReadAllText("TestData.json"));
                using (var session = (new SessionProvider().GetSessionFactory().OpenSession()))
                {
                    SetupTestData(testData.Users, session);
                    SetupTestData(testData.Maps, session);
                    session.Flush();
                }
            }
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);


            container.Register<SessionProvider>().AsSingleton();
            container.Register<IMapsService, MapsService>();
            container.Register<IUserService, UserService>();
        }

        private static void SetupTestData<T>(List<T> items, ISession session) where T : Entity
        {
            foreach (var item in items)
            {
                if (session.Query<T>().Any(_ => _.Id == item.Id))
                {
                    session.Merge(item);
                }
                else
                {
                    session.Save(item);
                }
            }
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
           TokenAuthentication.Enable(pipelines, new TokenAuthenticationConfiguration(container.Resolve<ITokenizer>()));
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get
            {
                var processors = new[]
                {
                    typeof (JsonProcessor),
                    typeof(CustomViewProcessor )
                };

                return NancyInternalConfiguration.WithOverrides(x => x.ResponseProcessors = processors);
            }
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/assets", "/assets"));
        }
    }
}