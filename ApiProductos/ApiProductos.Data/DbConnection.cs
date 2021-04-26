using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ApiProductos.Data
{
    public enum DbProvider { MSSQL }
    public class DbConnection
    {
        public static string striconn { get; set; }
        public static DbProvider dbprovid { get; set; }

        public static IDbConnection CreateConnection()
        {
            IDbConnection dbconnec;

            switch (dbprovid)
            {
                default:
                    dbconnec = new SqlConnection(striconn);
                    break;
            }

            return dbconnec;
        }
    }
}
