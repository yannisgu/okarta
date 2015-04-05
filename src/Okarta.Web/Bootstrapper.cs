using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Reflection;
using ConfigR;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Responses.Negotiation;
using Newtonsoft.Json;
using Okarta.Data;

namespace Okarta.Web
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        public Bootstrapper()
        {
            Config.Global.LoadScriptFile("Settings.csx");
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, OkartaMigrationConfiguration>()); 
            if (Config.Global.Get<bool>("insertTestData"))
            {
                var testData = JsonConvert.DeserializeObject<TestData>(File.ReadAllText("TestData.json"));
                using (var dbContext = new DataContext())
                {
                    dbContext.Maps.AddOrUpdate(testData.Maps.ToArray());
                    dbContext.SaveChanges();
                }
            }
        }

        protected override NancyInternalConfiguration InternalConfiguration
        {
            get
            {
                var processors = new[]
                {
                    typeof (JsonProcessor)
                };

                return NancyInternalConfiguration.WithOverrides(x => x.ResponseProcessors = processors);
            }
        }
    }
}