using Dme.Persistence;
using Microsoft.Data.SqlClient;

namespace Dme.ExtractorApp.Helpers;

internal static class DatabaseInitializer
{
	public static void EnsureDatabaseCreated(out bool created)
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
                created = false;
                return;
            }

            CreateDatabase(connection);
            connection.Close();
            created = true;
        }
    }

    private static void CreateDatabase(SqlConnection connection)
    {
        var sql = $@"
				    CREATE DATABASE
				        [{UsersDbContext.DatabaseName}]
				    ON PRIMARY (
				       NAME={UsersDbContext.DatabaseName},
				       FILENAME = '{Path.Combine(PathHelper.GetCurrentPath(), $"{UsersDbContext.DatabaseName}.mdf")}'
				    )
				    LOG ON (
				        NAME={UsersDbContext.DatabaseName}_log,
				        FILENAME = '{Path.Combine(PathHelper.GetCurrentPath(), $"{UsersDbContext.DatabaseName}_log.ldf")}'
				    )";

        var command = new SqlCommand(sql, connection);
        command.ExecuteNonQuery();
    }

    private static bool DatabaseExists(SqlConnection connection)
    {
        const string cmdText = $"SELECT * FROM sys.databases where Name='{UsersDbContext.DatabaseName}'";
        using var cmd = new SqlCommand(cmdText, connection);
        using var reader = cmd.ExecuteReader();
        return reader.HasRows;
    }
}