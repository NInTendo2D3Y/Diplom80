using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Class
{
    public static class Connector
    {
        private static DB.DiplomEntities DatabaseConnector;

        public static DB.DiplomEntities GetDatabase()
        {
            if(DatabaseConnector == null)
            {
                DatabaseConnector= new DB.DiplomEntities();
            }
            return DatabaseConnector;
        }
    }
}
