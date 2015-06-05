using System;
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
using Newtonsoft.Json;
using NHibernate.Linq;
using Okarta.Data;
using Okarta.Data.Entities;

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
                    foreach (var map in testData.Maps)
                    {
                        if (session.Query<Map>().Any(_ => _.Id == map.Id))
                        {
                            session.Merge(map);
                        }
                        else
                        {
                            session.Save(map);
                        }
                    }
                    session.Flush();
                }
            }
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