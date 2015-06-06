using ConfigR;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Conventions.Helpers.Builders;
using Humanizer;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Okarta.Data.Entities;

namespace Okarta.Data
{
    public class SessionProvider
    {
        private ISessionFactory factory;
        public ISessionFactory Factory
        {
            get { return factory ?? (factory = GetSessionFactory()); }
        }

        public ISessionFactory GetSessionFactory()
        {
            var connectionString = Config.Global.Get<string>("connection");

            var autoMap = AutoMap.AssemblyOf<Entity>()
                .Where(t => typeof (Entity).IsAssignableFrom(t))
                .Conventions.Add(
                    Table.Is(_ => _.EntityType.Name.Pluralize()),
                    new IdConventionBuilder().Always(_ => _.GeneratedBy.Assigned()));

            return Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
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
