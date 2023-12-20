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
    // Liste des attributes de connection.
    private static SqliteConnection connection;
    private static string conn = $"Data Source={ProjectSettings.GlobalizePath($"res://Tools/Database/{NameFromConfig()}")};";
    public static string ConnectionString
    {
        get => conn;
        set
        {
            CloseConnection();
            conn = value;
            OpenConnection();
        }
    }
    
    /// <summary>
    /// Méthode qui retourne le nom du fichier de config pour la base de donnée.
    /// </summary>
    /// <returns>Retourne par défault "DBT3.db" sinon override le nom</returns>
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
    
    /// <summary>
    /// Méthode qui retourne le path du fichier de config pour la base de donnée.
    /// </summary>
    /// <returns>Retourne par défault le path racine du projet sinon override le path</returns>
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
    
    /// <summary>
    /// Méthode qui retourne la combinaison path et nom de la base de donnée.
    /// </summary>
    /// <returns>Retourne le path composer du nom de la base de donnée</returns>
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
    
    /// <summary>
    /// Méthode qui ouvre la connection à la base de donnée ou/et télécharge la base de donnée.
    /// </summary>
    /// <returns></returns>
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
    
    /// <summary>
    /// Méthode qui retourne la connection à la base de donnée.
    /// </summary>
    /// <returns></returns>
    public static SqliteConnection GetConnection()
    {
        if (connection == null || connection.State != ConnectionState.Open)
        {
            OpenConnection();
        }
        return connection;
    }
    
    /// <summary>
    /// Méthode qui ferme la connection à la base de donnée.
    /// </summary>
    /// <returns></returns>
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