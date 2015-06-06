using BamPi.EntityFramework;
using BamPi.NHibernate;
using ConfigR;
using Okarta.Data;
using Okarta.Data.Entities;

namespace Okarta.Web
{
    using BamPi;

    public class MapsApiDefinition : ApiDefinition
    {
        public MapsApiDefinition()
        {
            DataContext = new BamPiNHibernateDataContext(new SessionProvider().GetSessionFactory());

            Get["/maps"] = Query<Map>();
            
        }
    }
}
