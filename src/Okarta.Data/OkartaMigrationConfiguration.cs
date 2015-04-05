using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigR;

namespace Okarta.Data
{
    public class OkartaMigrationConfiguration :  DbMigrationsConfiguration<DataContext>
    {
        public OkartaMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = Config.Global.Get<bool>("isLocal");
        }
    }
}
