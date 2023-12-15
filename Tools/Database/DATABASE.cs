using System;
using Godot;

//https://unintuitive.net/code/working-in-godot-with-sqlite-and-c/
using System.Data;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using Mono.Data.Sqlite;
using FileAccess = System.IO.FileAccess;

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

    private static string CreatePath()
    {
        string temp = PathFromConfig();
        if (temp != null)
        {
            if (temp.LastIndexOf('/') != temp.Length - 1)
            {
                temp += '/';
            }
            return  temp + NameFromConfig();
        }
        else
        {
            return ProjectSettings.GlobalizePath("res://") + NameFromConfig();
        }
    }
    
    private static void OpenConnection()
    {
        GD.Print("Connection string : " + conn);
        try
        {
            if (!File.Exists(ProjectSettings.GlobalizePath($"res://Tools/Database/{NameFromConfig()}")) && File.Exists(CreatePath()))
            {
                conn = $"Data Source={CreatePath()};";
            }
            if (!File.Exists(ProjectSettings.GlobalizePath($"res://Tools/Database/{NameFromConfig()}")) && !File.Exists(CreatePath()))
            {
                try
                {
                    WebClient client = new WebClient();
                    client.DownloadFile("https://seafile.unistra.fr/f/c7688231c5414fc283ac/?dl=1", CreatePath()); //partage seafile de trott
                    conn = $"Data Source={CreatePath()};";
                    connection = new SqliteConnection(conn);
                    connection.Open();
                }
                catch (Exception err)
                {
                    GD.Print("ERROR WebClient : " + err.Message);
                }
            }
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