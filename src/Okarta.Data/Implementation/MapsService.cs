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

        public Map Get(Guid guid)
        {
            return Session.Get<Map>(guid);
        }

        public void Add(Map map)
        {
            Session.Save(map);
            Session.Flush();
        }

        public void Update(Map map)
        {
            Session.Merge(map);
            Session.Flush();
        }

        public MapsService(SessionProvider provider) : base(provider)
        {
        }
    }
}
