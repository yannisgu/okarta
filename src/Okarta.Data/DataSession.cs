using ConfigR;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Okarta.Data.Entities;

namespace Okarta.Data
{
    public class DataSession
    {
        public ISessionFactory GetSession()
        {
            var connectionString = Config.Global.Get<string>("connection");

            var autoMap = AutoMap.AssemblyOf<Entity>()
                .Where(t => typeof (Entity).IsAssignableFrom(t));

            return Fluently.Configure()
                .Database(
                    MsSqlCeConfiguration.Standard.ConnectionString(connectionString))
                .Mappings(m => m.AutoMappings.Add(autoMap))
                .ExposeConfiguration(TreatConfiguration)
                .BuildSessionFactory();
        }

        protected virtual void TreatConfiguration(Configuration configuration)
        {
            var update = new SchemaUpdate(configuration);
            update.Execute(false, true);
        }
    }
}