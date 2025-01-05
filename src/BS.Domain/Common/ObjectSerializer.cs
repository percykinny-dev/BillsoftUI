namespace BS.Domain.Common;

public static class ObjectSerializer
{
    public static string Serialize<T>(T obj)
    {
        if (obj == null)
            return string.Empty;

        return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
    }

    public static T Deserialize<T>(string json)
    {
        if (string.IsNullOrEmpty(json))
            return default;

        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
    }
}
