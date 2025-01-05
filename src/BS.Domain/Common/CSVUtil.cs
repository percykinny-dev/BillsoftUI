namespace BS.Domain.Common;

public class CSVUtil
{

    public static string ToCsv<T>(List<T> list)
    {
        StringBuilder sb = new StringBuilder();

        //Get the properties for type T for the headers
        PropertyInfo[] propInfos = typeof(T).GetProperties();
        for (int i = 0; i <= propInfos.Length - 1; i++)
        {
            var displayName = GetColumnDisplayName(propInfos[i]);
            sb.Append(displayName);

            if (i < propInfos.Length - 1)
            {
                sb.Append(",");
            }
        }

        sb.AppendLine();

        //Loop through the collection, then the properties and add the values
        for (int i = 0; i <= list.Count - 1; i++)
        {
            T item = list[i];
            for (int j = 0; j <= propInfos.Length - 1; j++)
            {
                object o = item.GetType().GetProperty(propInfos[j].Name).GetValue(item, null);
                if (o != null)
                {
                    string value = o.ToString();

                    //Check if the value contans a comma and place it in quotes if so
                    if (value.Contains(","))
                    {
                        value = string.Concat("\"", value, "\"");
                    }

                    //Replace any \r or \n special characters from a new line with a space
                    if (value.Contains("\r"))
                    {
                        value = value.Replace("\r", " ");
                    }
                    if (value.Contains("\n"))
                    {
                        value = value.Replace("\n", " ");
                    }

                    sb.Append(value);
                }

                if (j < propInfos.Length - 1)
                {
                    sb.Append(",");
                }
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }

    private static string GetColumnDisplayName(PropertyInfo property)
    {
        var atts = property.GetCustomAttributes(typeof(DisplayAttribute), true);
        if (atts.Length == 0)
            return property.Name;
        return (atts[0] as DisplayAttribute).Name;
    }

    public static DataTable ConvertCSVtoDataTable(Stream attachment)
    {
        DataTable dt = new DataTable();
        using (StreamReader sr = new StreamReader(attachment))
        {
            string[] headers = sr.ReadLine().Split(',');
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = sr.ReadLine().Split(',');
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
                dt.AcceptChanges();
            }

        }


        return dt;
    }
}
