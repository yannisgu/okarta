using System;
using NHibernate;

namespace Okarta.Data.Implementation
{
    public abstract class DataService : IDisposable
    {
        public ISession Session { get; private set; }

        protected DataService(SessionProvider provider)
        {
            Session = provider.Factory.OpenSession();
        }

        public void Dispose()
        {
            Session.Dispose();    
        }
    }
}