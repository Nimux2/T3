using System;
using System.Collections.Generic;
using System.Data;
using Godot;
using Mono.Data.Sqlite;

namespace T3Projet.Scripts.Models;

public class Maladie
{
    // Liste des attributes de la maladie
    private int ID;
    private string nom;
    public string Nom
    {
        get => nom;
    }
    private List<Symptome> symptomes = new List<Symptome>();
    private Symptome symptomeCourant;

    public Maladie(int ID)
    {
        this.ID = ID;
        ChargerNomMaladie();
        ChargerSymptomes();
    }
    
    /// <summary>
    /// Méthode qui charge le nom de la maladie dans la base de donnée.
    /// </summary>
    /// <returns></returns>
    private void ChargerNomMaladie()
    {
        try
        {
            SqliteCommand command = new SqliteCommand()
            {
                Connection = Database.GetConnection(),
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
    
    /// <summary>
    /// Méthode qui charge les symptome associé à la maladie dans la base de donnée.
    /// </summary>
    /// <returns></returns>
    private void ChargerSymptomes()
    {
        try
        {
            SqliteCommand command = new SqliteCommand()
            {
                Connection = Database.GetConnection(),
                CommandType = CommandType.Text,
                CommandText = "SELECT id_symptome FROM Maladies_Symptomes WHERE id_maladie = @ID;",
            };
            command.Parameters.Add("@ID", DbType.Int32);
            command.Parameters[0].Value = this.ID;

            SqliteDataReader data = command.ExecuteReader();

            while (data.Read())
            {
                symptomes.Add(new Symptome(data.GetInt32(0)));
            }
        }
        catch (SqliteException err)
        {
            GD.Print("Maladie 2 : ERREUR DB = " + err.Message);
        }
    }
    
    /// <summary>
    /// Méthode qui génére un id par rapport au nombre de maladie dans la base de donnée.
    /// </summary>
    /// <returns></returns>
    public static int GenererRandomIdMaladie()
    {
        GD.Randomize();
        try
        {
            SqliteCommand command = new SqliteCommand()
            {
                Connection = Database.GetConnection(),
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
    
    /// <summary>
    /// Méthode qui retourne la liste de question suivante de façon aléatoire.
    /// </summary>
    /// <returns>Retourne la liste de question suivante si plus de question retourne null</returns>
    public List<Question> DonnerQuestionsSuivante()
    {
        if (symptomes.Count == 0)
        {
            return null;
        }
        GD.Randomize();
        try
        {
            int index = GD.RandRange(0, symptomes.Count - 1);
            symptomeCourant = symptomes[index];
            symptomes.RemoveAt(index);
            return symptomeCourant.DonnerQuestions();
        }
        catch (Exception err)
        {
            GD.Print("ERROR : " + err.Message);
            return null;
        }
    }
    /// <summary>
    /// Méthode qui retourne la réponse en fonction du niveau de stress "stress" pour le symptome courant -> question.
    /// </summary>
    /// <param name="stress"></param>
    /// <returns></returns>
    public Réponse DonnerRéponseAQuestion(int stress)
    {
        return symptomeCourant.DonnerRéponse(stress);
    }
}