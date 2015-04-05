using BamPi.EntityFramework;
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
            DataContext = new BamPiEfDataConext(() => new DataContext());

            Get["/maps"] = Query<Map>();
        }
    }
}