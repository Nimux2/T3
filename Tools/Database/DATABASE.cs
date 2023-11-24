using Godot;

//https://unintuitive.net/code/working-in-godot-with-sqlite-and-c/
using System.Data;
using Mono.Data.Sqlite;

public partial class DATABASE : Node
{
    private static SqliteConnection connection;
    //public static string conn = $"Data Source={System.IO.Directory.GetCurrentDirectory()}/Tools/Database/DBT3.db;Version=3;";
    private static string conn = $"Data Source=/home/trott/Documents/Cours/t3/T3_Projet/Tools/Database/DBT3.db;Version=3;";

    public static string Connection_String
    {
        get => conn;
        set
        {
            CloseConnection();
            conn = value;
            OpenConnection();
        }
    }
    private static void OpenConnection()
    {
        try
        {
            connection = new SqliteConnection(conn);
            connection.Open();
        }
        catch (SqliteException err)
        {
            GD.Print(err.Message);
        }
    }
    public static SqliteConnection GetConnection()
    {
        if (connection == null)
        {
            OpenConnection();
        }
        return connection;
    }
    public static void CloseConnection()
    {
        if (connection != null)
        {
            try
            {
                connection.Close();
            }
            catch (SqliteException err)
            {
                GD.Print(err.Message);
            }
        }
    }
}