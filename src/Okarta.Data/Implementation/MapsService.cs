using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;
using Okarta.Data.Entities;
using Okarta.Data.Services;

namespace Okarta.Data.Implementation
{
    public class MapsService : DataService,IMapsService
    {
        public IEnumerable<Map> Query()
        {
            return Session.Query<Map>();
        }

        public MapsService(SessionProvider provider) : base(provider)
        {
        }
    }
}
