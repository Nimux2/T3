using System;
using System.Collections.Generic;
using System.Data;
using Godot;
using Mono.Data.Sqlite;

namespace T3Projet.Tools.Models;

public class Maladie
{
    private int ID;
    private string nom;
    public string Nom
    {
        get => nom;
    }
    private List<Symtome> symptomes = new List<Symtome>();
    private Symtome currentSymptome;

    public Maladie(int ID)
    {
        this.ID = ID;
        ChargerNomMaladie();
        ChargerSymptomes();
    }
    
    private void ChargerNomMaladie()
    {
        try
        {
            SqliteCommand command = new SqliteCommand()
            {
                Connection = DATABASE.GetConnection(),
                CommandType = CommandType.Text,
                CommandText = "SELECT nom FROM Maladies WHERE id = @ID;",
            };
            command.Parameters.Add("@ID",DbType.Int32);
            command.Parameters[0].Value = this.ID;
            this.nom = command.ExecuteScalar().ToString();
        }
        catch (SqliteException err)
        {
            GD.Print("Maladie 1 : ERREUR DB = " + err.Message);
        }
    }

    private void ChargerSymptomes()
    {
        try
        {
            SqliteCommand command = new SqliteCommand()
            {
                Connection = DATABASE.GetConnection(),
                CommandType = CommandType.Text,
                CommandText = "SELECT id_symptome FROM Maladies_Symptomes WHERE id_maladie = @ID;",
            };
            command.Parameters.Add("@ID", DbType.Int32);
            command.Parameters[0].Value = this.ID;

            SqliteDataReader data = command.ExecuteReader();

            while (data.Read())
            {
                symptomes.Add(new Symtome(data.GetInt32(0)));
            }
        }
        catch (SqliteException err)
        {
            GD.Print("Maladie 2 : ERREUR DB = " + err.Message);
        }
    }

    public static int RandomIdMaladie()
    {
        GD.Randomize();
        try
        {
            SqliteCommand command = new SqliteCommand()
            {
                Connection = DATABASE.GetConnection(),
                CommandType = CommandType.Text,
                CommandText = "SELECT max(id) FROM Maladies;",
            };
            int max = int.Parse(command.ExecuteScalar().ToString());
            return GD.RandRange(1, max);
        }
        catch (SqliteException err)
        {
            GD.Print("Maladie 3 : ERREUR DB = " + err.Message);
            return -1;
        }
    }

    public List<Question> QuestionsSuivante()
    {
        if (symptomes.Count == 0)
        {
            return null;
        }
        GD.Randomize();
        try
        {
            int index = GD.RandRange(0, symptomes.Count - 1);
            currentSymptome = symptomes[index];
            symptomes.RemoveAt(index);
            return currentSymptome.DonnerQuestions();
        }
        catch (Exception err)
        {
            GD.Print("ERROR : " + err.Message);
            return null;
        }
    }

    public Réponse RéponseQuestion(int stress)
    {
        return currentSymptome.DonnerRéponse(stress);
    }
}