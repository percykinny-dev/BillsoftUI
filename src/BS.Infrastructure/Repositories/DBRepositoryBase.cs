namespace BS.Infrastructure.Repositories;

public class DBRepositoryBase
{
    protected readonly IConfiguration configuration;

    protected string connectionString
    {
        get
        {
            return configuration.GetValue<string>("ConnectionStrings:BillsoftDB");
        }
    }

    public DBRepositoryBase(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    protected virtual List<T> ToList<T>(IDataReader rdr)
    {
        List<T> ret = new List<T>();
        T entity;
        Type typ = typeof(T);
        PropertyInfo col;
        List<PropertyInfo> columns = new List<PropertyInfo>();

        PropertyInfo[] props = typ.GetProperties();

        for (int index = 0; index < rdr.FieldCount; index++)
        {
            // See if column name maps directly to property name
            col = props.FirstOrDefault(c => c.Name == rdr.GetName(index));
            if (col != null)
            {
                columns.Add(col);
            }
        }

        // Loop through all records
        while (rdr.Read())
        {
            // Create new instance of Entity
            entity = Activator.CreateInstance<T>();
            // Loop through columns to assign data
            for (int i = 0; i < columns.Count; i++)
            {
                if (rdr[columns[i].Name].Equals(DBNull.Value))
                {
                    columns[i].SetValue(entity, null, null);
                }
                else
                {
                    columns[i].SetValue(entity, rdr[columns[i].Name], null);
                }
            }
            ret.Add(entity);
        }
        return ret;
    }
}
