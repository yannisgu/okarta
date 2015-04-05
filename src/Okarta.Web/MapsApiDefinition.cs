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
            DataContext = new BamPiNHibernateDataContext(new DataSession().GetSession());

            Get["/maps"] = Query<Map>();
        }
    }
}