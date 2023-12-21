using System;
using Godot;

//https://unintuitive.net/code/working-in-godot-with-sqlite-and-c/
using System.Data;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using Mono.Data.Sqlite;
using FileAccess = System.IO.FileAccess;

public class Database
{
    // Liste des attributes de connection.
    private static SqliteConnection connection;
    private static string conn = $"Data Source={ProjectSettings.GlobalizePath($"res://Scripts/Database/{ChargerNomDepuisConfig()}")};";
    public static string ConnectionString
    {
        get => conn;
        set
        {
            FermerConnection();
            conn = value;
            OuvrirConnection();
        }
    }
    
    /// <summary>
    /// Méthode qui charge le nom du fichier depuis le fichier de config pour la base de donnée.
    /// </summary>
    /// <returns>Retourne par défault "DBT3.db" sinon override le nom</returns>
    private static string ChargerNomDepuisConfig()
    {
        try
        {
            ConfigFile config = new ConfigFile();
            config.Load("res://Config/gameconfig.cfg");
            if (config.HasSection("Database") && config.HasSectionKey("Database", "name"))
            {
                return config.GetValue("Database", "name").As<string>();
            }
            else
            {
                return "DBT3.db";
            }
        }
        catch (Exception err)
        {
            GD.Print("ERROR : Config = " + err.Message);
            return "DBT3.db";
        }
    }
    
    /// <summary>
    /// Méthode qui charge le path du fichier depuis le fichier de config pour la base de donnée.
    /// </summary>
    /// <returns>Retourne par défault le path racine du projet sinon override le path</returns>
    private static string ChargerPathDepuisConfig()
    {
        try
        {
            ConfigFile config = new ConfigFile();
            config.Load("res://Config/gameconfig.cfg");
            if (config.HasSection("Database") && config.HasSectionKey("Database", "path"))
            {
                return config.GetValue("Database", "path").As<string>();
            }
            else
            {
                return null;
            }
        }
        catch(Exception err)
        {
            GD.Print("ERROR : Config = " + err.Message);
            return null;
        }
    }
    
    /// <summary>
    /// Méthode qui retourne la combinaison path et nom de la base de donnée.
    /// </summary>
    /// <returns>Retourne le path composer du nom de la base de donnée</returns>
    private static string CréerPathComplet()
    {
        string temp = ChargerPathDepuisConfig();
        if (temp != null)
        {
            if (temp.LastIndexOf('/') != temp.Length - 1)
            {
                temp += '/';
            }
            return  temp + ChargerNomDepuisConfig();
        }
        else
        {
            return ProjectSettings.GlobalizePath("res://") + ChargerNomDepuisConfig();
        }
    }
    
    /// <summary>
    /// Méthode qui donne le lien de téléchargement de la base de donnée 
    /// </summary>
    /// <returns>Retourne par défault le lien de téléchargement de la base de donnée sinon override le lien de téléchargement</returns>
    private static string ChargerLienTéléchargementDepuisConfig()
    {
        ConfigFile config = new ConfigFile();
        config.Load("res://Config/gameconfig.cfg");
        if (config.HasSection("Database") && config.HasSectionKey("Database" , "download_link"))
        {
            return config.GetValue("Database", "download_link").As<string>();
        }
        else
        {
            return "https://seafile.unistra.fr/f/cd9e1e78560a4bab9529/?dl=1";
        }
    }
    
    /// <summary>
    /// Méthode qui ouvre la connection à la base de donnée ou/et télécharge la base de donnée.
    /// </summary>
    /// <returns></returns>
    private static void OuvrirConnection()
    {
        GD.Print("Connection string : " + conn);
        try
        {
            if (!File.Exists(ProjectSettings.GlobalizePath($"res://Scripts/Database/{ChargerNomDepuisConfig()}")) && File.Exists(CréerPathComplet()))
            {
                conn = $"Data Source={CréerPathComplet()};";
            }
            if (!File.Exists(ProjectSettings.GlobalizePath($"res://Scripts/Database/{ChargerNomDepuisConfig()}")) && !File.Exists(CréerPathComplet()))
            {
                try
                {
                    WebClient client = new WebClient();
                    client.DownloadFile(ChargerLienTéléchargementDepuisConfig(), CréerPathComplet()); //partage seafile de trott
                    conn = $"Data Source={CréerPathComplet()};";
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
            OuvrirConnection();
        }
        return connection;
    }
    
    /// <summary>
    /// Méthode qui ferme la connection à la base de donnée.
    /// </summary>
    /// <returns></returns>
    public static void FermerConnection()
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