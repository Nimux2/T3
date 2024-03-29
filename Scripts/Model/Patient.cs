using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using Mono.Data.Sqlite;
using T3Projet.Tools.Gestion;

namespace T3Projet.Scripts.Models;

public class Patient
{
    // Liste des attributs du patient.
    private int ID;
    private string nom;
    public string Nom { get => nom; }
    public int Stress { get; set; }
    public int Diag { get; set; }
    private int imageID;
    
    private ImagesPatient images;
    public Patient(int ID)
    {
        this.ID = ID;
        ChargerInformationPatient();
        ChargerImagesPatient();
    }
    
    /// <summary>
    /// Méthode qui charge les informations du patient dans la base de donnée.
    /// </summary>
    /// <returns></returns>
    private void ChargerInformationPatient()
    {
        if (ID >= 1)
        {
            try
            {
                SqliteCommand command = new SqliteCommand()
                {
                    Connection = Database.GetConnection(),
                    CommandType = CommandType.Text,
                    CommandText = "SELECT * FROM Patients WHERE id = @ID;",
                };
                command.Parameters.Add("@ID", DbType.Int32);
                command.Parameters["@ID"].Value = ID;

                SqliteDataReader data = command.ExecuteReader();

                while (data.Read())
                {
                    this.nom = FormaterNom(data.GetString(1));
                    this.Stress = data.GetInt32(2);
                    this.imageID = data.GetInt32(4);
                }
            }
            catch (SqliteException err)
            {
                GD.Print("Patient 1 : ERROR DB Patients = " + err.Message);
            }
        }
    }
    
    /// <summary>
    /// Méthode qui charge la liste des nom d'images pour le patient dans la base de donnée.
    /// </summary>
    /// <returns></returns>
    private void ChargerImagesPatient()
    {
        if (imageID >= 1)
        {
            try
            {
                SqliteCommand command = new SqliteCommand()
                {
                    Connection = Database.GetConnection(),
                    CommandType = CommandType.Text,
                    CommandText = "SELECT * FROM ImagesPatient WHERE id = @ID;",
                };
                command.Parameters.Add("@ID", DbType.Int32);
                command.Parameters["@ID"].Value = ID;

                SqliteDataReader data = command.ExecuteReader();

                while (data.Read())
                {
                    this.images = new ImagesPatient();
                    for (int i = 1; i < 6; i++)
                    {
                        if (!data.IsDBNull(i))
                        {
                            this.images.AjouterImage(data.GetValue(i).ToString() , i); // Index correspond au valeurs de l'enum ImagesPatient.Types
                        }
                        else
                        {
                            this.images.AjouterImage("Default", i);
                        }
                    }
                }
            }
            catch (SqliteException err)
            {
                GD.Print("Patient 2 : ERROR DB Patients = " + err.Message);
            }
        }
    }
    
    /// <summary>
    /// Méthode qui permet de formater le nom.
    /// </summary>
    /// <param name="nom"></param>
    /// <returns></returns>
    private string FormaterNom(string nom)
    {
        if (!char.IsUpper((nom.ToCharArray())[0]))
        {
            return nom.Substring(0, 1).ToUpper() + nom.Substring(1);
        }
        else
        {
            return nom;
        }
    }
    
    /// <summary>
    /// Méthode qui retourne le nom de l'image en fonction de l'enum "type".
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public string DonnerNomImageCaractere(ImagesPatient.Types type)
    {
        return images.GetImageForEnum(type);
    }
    
    /// <summary>
    /// Méthode qui génère un id par rapport au nombre de patient dans la base de donnée.
    /// </summary>
    /// <returns></returns>
    public static int GenererRandomIdPatient()
    {
        GD.Randomize();
        try
        {
            SqliteCommand command = new SqliteCommand()
            {
                Connection = Database.GetConnection(),
                CommandType = CommandType.Text,
                CommandText = "SELECT max(id) FROM Patients;",
            };
            int max = int.Parse(command.ExecuteScalar().ToString());
            return GD.RandRange(1, max);
        }
        catch (SqliteException err)
        {
            GD.Print("Patient 3 : ERREUR DB Patients = " + err.Message);
            return -1;
        }
    }
}