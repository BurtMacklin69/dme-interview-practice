using Microsoft.Data.SqlClient;

namespace Dme.ExtractorApp.Helpers;

internal static class DatabaseInitializer
{
    public const string DatabaseName = "DmeLupinovUsers";

    public static void EnsureDatabaseCreated()
    {
        var connection = new SqlConnection(
            "Data Source=(localdb)\\MSSQLLocalDB;" +
            "Initial Catalog=master;" +
            "Integrated Security=True;" +
            "Connect Timeout=30;" +
            "Encrypt=False;" +
            "Trust Server Certificate=False;" +
            "Application Intent=ReadWrite;" +
            "Multi Subnet Failover=False");

        using (connection)
        {
            connection.Open();

            if (DatabaseExists(connection))
            {
                connection.Close();
                return;
            }

            CreateDatabase(connection);
            connection.Close();
        }
    }

    private static void CreateDatabase(SqlConnection connection)
    {
        var sql = $@"
				    CREATE DATABASE
				        [{DatabaseName}]
				    ON PRIMARY (
				       NAME={DatabaseName},
				       FILENAME = '{Path.Combine(PathHelper.GetCurrentPath(), $"{DatabaseName}.mdf")}'
				    )
				    LOG ON (
				        NAME={DatabaseName}_log,
				        FILENAME = '{Path.Combine(PathHelper.GetCurrentPath(), $"{DatabaseName}_log.ldf")}'
				    )";

        var command = new SqlCommand(sql, connection);
        command.ExecuteNonQuery();
    }

    private static bool DatabaseExists(SqlConnection connection)
    {
        const string cmdText = $"SELECT * FROM sys.databases where Name='{DatabaseName}'";
        using var cmd = new SqlCommand(cmdText, connection);
        using var reader = cmd.ExecuteReader();
        return reader.HasRows;
    }
}