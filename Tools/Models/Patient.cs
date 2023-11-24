using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using Mono.Data.Sqlite;
using T3Projet.Tools.Gestion;

namespace T3Projet.Tools.Models;

public class Patient
{
    private int ID;
    private string nom;
    public string Nom { get; }
    public int Stress { get; set; }
    public int Diag { get; set; }
    
    private ImagesPatient images;
    
    public Patient(int ID)
    {
        this.ID = ID;
        ChargerImagesPatient(ChargerInformationPatient());
    }
    private int ChargerInformationPatient()
    {
        if (ID >= 1)
        {
            try
            {
                SqliteCommand command = new SqliteCommand()
                {
                    Connection = DATABASE.GetConnection(),
                    CommandType = CommandType.Text,
                    CommandText = "SELECT * FROM Patients WHERE id = @ID;",
                };
                command.Parameters.Add("@ID", DbType.Int32);
                command.Parameters["@ID"].Value = ID;

                SqliteDataReader data = command.ExecuteReader();

                int imageID = 0;

                while (data.Read())
                {
                    this.nom = FormatNom(data.GetString(1));
                    this.Stress = data.GetInt32(2);
                    imageID = data.GetInt32(4);
                }

                return imageID;
            }
            catch (SqliteException err)
            {
                GD.Print("Patient 1 : ERROR DB Patients = " + err.Message);
                return -1;
            }
        }
        else
        {
            return -1;
        }
    }
    private void ChargerImagesPatient(int ID)
    {
        if (ID >= 1)
        {
            try
            {
                SqliteCommand command = new SqliteCommand()
                {
                    Connection = DATABASE.GetConnection(),
                    CommandType = CommandType.Text,
                    CommandText = "SELECT * FROM Image_Patient WHERE id = @ID;",
                };
                command.Parameters.Add("@ID", DbType.Int32);
                command.Parameters["@ID"].Value = ID;

                SqliteDataReader data = command.ExecuteReader();

                while (data.Read())
                {
                    this.images = new ImagesPatient(data.GetString(1), data.GetString(2), data.GetString(3),
                        data.GetString(4), data.GetString(5));
                }
            }
            catch (SqliteException err)
            {
                GD.Print("Patient 2 : ERROR DB Patients = " + err.Message);
            }
        }
    }
    private string FormatNom(string nom)
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
    public string DonnerNomImageCaractere(ImagesPatient.Types type)
    {
        if (type != images.ActualType)
        {
            return images.GetImageForEnum(type);
        }
        else
        {
            return null;
        }
    }
    
    public static int RandomIdPatient()
    {
        GD.Randomize();
        try
        {
            SqliteCommand command = new SqliteCommand()
            {
                Connection = DATABASE.GetConnection(),
                CommandType = CommandType.Text,
                CommandText = "SELECT max(*) FROM Patients;",
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