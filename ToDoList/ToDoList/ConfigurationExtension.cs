
namespace ToDoList
{
    public static class ConfigurationExtension
    {
        public static string GetSQLServerConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("SQLServer");
        }
        public static string GetDefaultXMLStorage(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("XmlFilePath");
        }
    }
}
