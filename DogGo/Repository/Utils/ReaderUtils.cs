using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Repositories
{
    public class ReaderUtils
    {
        public static string check4NullString(SqlDataReader reader, string ColName)
        {
            int colIndex = reader.GetOrdinal(ColName);

            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetString(colIndex);
            }
            else
            {
                return null;
            }
        }

        public static object GetNullableParam(object value) 
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            else
            {
                return value;
            }
        }
    }
}
