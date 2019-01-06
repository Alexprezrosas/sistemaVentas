using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEKNO_v1
{
    public class BaseDatos
    {
        public static AccesoBD.BaseDatos db;

        public static AccesoBD.BaseDatos GetBaseDatos()
        {
            if (db == null)
                db = new AccesoBD.BaseDatos();
            return db;
        }
    }
}
