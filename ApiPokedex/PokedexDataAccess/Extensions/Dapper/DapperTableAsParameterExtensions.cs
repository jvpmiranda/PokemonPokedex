using Dapper;
using System.Data;

namespace PokedexDataAccess.Extensions.Dapper;

public static class DapperTableAsParameterExtensions
{
    public static SqlMapper.ICustomQueryParameter AsTableValuedParameter<T>(this IEnumerable<T> list, string tableAsParameterType)
    {
        DataTable dt = new DataTable();
        var properties = typeof(T).GetProperties();
        foreach (var prop in properties)
            dt.Columns.Add(prop.Name, prop.PropertyType);

        foreach (var item in list)
        {
            List<object> values = new List<object>();
            foreach (var prop in properties)
                values.Add(prop.GetValue(item));

            dt.Rows.Add(values.ToArray());
        }
        
        return dt.AsTableValuedParameter(tableAsParameterType);
    }
}
