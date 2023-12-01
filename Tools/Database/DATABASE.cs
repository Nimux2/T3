using System;
using Godot;

//https://unintuitive.net/code/working-in-godot-with-sqlite-and-c/
using System.Data;
using Mono.Data.Sqlite;

public class DATABASE
{
    private static SqliteConnection connection;
    private static string conn = $"Data Source={ProjectSettings.GlobalizePath($"res://Tools/Database/{NameFromConfig()}")};";
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
    private static string NameFromConfig()
    {
        ConfigFile config = new ConfigFile();
        config.Load("res://Config/gameconfig.cfg");
        if (config.HasSection("Database") && config.HasSectionKey("Database" , "name"))
        {
            return config.GetValue("Database", "name").As<string>();
        }
        else
        {
            return "DBT3.db";
        }
    }

    private static string PathFromConfig()
    {
        ConfigFile config = new ConfigFile();
        config.Load("res://Config/gameconfig.cfg");
        if (config.HasSection("Database") && config.HasSectionKey("Database" , "path"))
        {
            return config.GetValue("Database", "path").As<string>();
        }
        else
        {
            return null;
        }
    }
    
    private static void OpenConnection()
    {
        GD.Print("Connection string : " + conn);
        try
        {
            connection = new SqliteConnection(conn);
            connection.Open();
        }
        catch (SqliteException err)
        {
            string temp = PathFromConfig();
            if (temp != null)
            {
                if (temp.LastIndexOf('/') != temp.Length - 1)
                {
                    temp += '/';
                }
                conn = $"Data Source={temp}{NameFromConfig()};";
            }
            else
            {
                conn = $"Data Source={ProjectSettings.GlobalizePath("res://")}{NameFromConfig()};";
            }
            GD.Print(err.Message);
            OpenConnection();
        }
    }
    public static SqliteConnection GetConnection()
    {
        if (connection == null || connection.State != ConnectionState.Open)
        {
            OpenConnection();
        }
        return connection;
    }
    public static void CloseConnection()
    {
        if (connection != null && connection.State == ConnectionState.Open)
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