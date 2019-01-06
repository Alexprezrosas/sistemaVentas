using AccesoBD;

namespace TEKNO_v1
{
    class Cragar_Tekno_BD
    {
        private static BaseDatos db;
        public static BaseDatos getDB()
        {
            if (db == null)
                db = new BaseDatos();
            return db;
        }
    }
}
