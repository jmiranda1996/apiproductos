using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
//using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using ApiProductos.Tables;

namespace ApiProductos.Data
{
    public class BaseRepository
    {
        public virtual async Task<IEnumerable<T>> DbQueryAsync<T>(string sql, object parameters = null)
        {
            IEnumerable<T> data;

            using (IDbConnection dbCon = DbConnection.CreateConnection())
            {
                dbCon.Open();

                if (parameters == null)
                    data = await dbCon.QueryAsync<T>(sql);

                else data = await dbCon.QueryAsync<T>(sql, parameters);

                dbCon.Close();
            }

            return data;
        }

        public virtual async Task<T> DbQuerySingleAsync<T>(string sql, object parameters)
        {
            T data;

            using (IDbConnection dbCon = DbConnection.CreateConnection())
            {
                dbCon.Open();

                if(parameters == null)
                    data = await dbCon.QueryFirstOrDefaultAsync<T>(sql);

                else data = await dbCon.QueryFirstOrDefaultAsync<T>(sql, parameters);

                dbCon.Close();
            }

            return data;
        }

        public virtual async Task<int> DbExecuteAsync(string sql, object parameters)
        {
            int affectRows = 0;
            using (IDbConnection dbCon = DbConnection.CreateConnection())
            {
                dbCon.Open();

                affectRows = await dbCon.ExecuteAsync(sql, parameters);

                dbCon.Close();
            }
            return affectRows;
        }
    }
}
