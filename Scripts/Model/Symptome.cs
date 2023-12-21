using System.Collections.Generic;
using System.Data;
using Godot;
using Godot.Collections;
using Mono.Data.Sqlite;

namespace T3Projet.Scripts.Models;

public partial class Symptome : Node
{
    private int ID;
    private string nom;
    private List<Question> questions = new List<Question>();
    private List<Réponse> réponses = new List<Réponse>();

    public Symptome(int ID)
    {
        this.ID = ID;
        ChargerNomSymptome();
        ChargerQuestions();
        ChargerRéponses();
    }

    private void ChargerNomSymptome()
    {
        try
        {
            SqliteCommand command = new SqliteCommand()
            {
                Connection = Database.GetConnection(),
                CommandType = CommandType.Text,
                CommandText = "SELECT nom FROM Symptomes WHERE id = @ID;",
            };
            command.Parameters.Add("@ID", DbType.Int32);
            command.Parameters[0].Value = this.ID;
            this.nom = (string)command.ExecuteScalar();
        }
        catch (SqliteException err)
        {
            GD.Print("Symptome 1 : ERREUR DB Symptomes = " + err.Message);
        }
    }

    private void ChargerQuestions()
    {
        try
        {
            SqliteCommand command = new SqliteCommand()
            {
                Connection = Database.GetConnection(),
                CommandType = CommandType.Text,
                CommandText = "SELECT id_question FROM Questions_Symptomes WHERE id_symptome = @ID;",
            };
            command.Parameters.Add("@ID", DbType.Int32);
            command.Parameters[0].Value = this.ID;

            SqliteDataReader data = command.ExecuteReader();

            while (data.Read())
            {
                this.questions.Add(new Question(data.GetInt32(0)));
            }
        }
        catch (SqliteException err)
        {
            GD.Print("Symptome 2 : ERREUR DB = " + err.Message);
        }
    }

    private void ChargerRéponses()
    {
        try
        {
            SqliteCommand command = new SqliteCommand()
            {
                Connection = Database.GetConnection(),
                CommandType = CommandType.Text,
                CommandText = "SELECT id_reponse FROM Reponses_Symptomes WHERE id_symptome = @ID;",
            };
            command.Parameters.Add("@ID", DbType.Int32);
            command.Parameters[0].Value = this.ID;

            SqliteDataReader data = command.ExecuteReader();

            while (data.Read())
            {
                this.réponses.Add(new Réponse(data.GetInt32(0)));
            }
        }
        catch (SqliteException err)
        {
            GD.Print("Symptome 3 : ERREUR DB = " + err.Message);
        }
    }

    public List<Question> DonnerQuestions()
    {
        return questions;
    }

    public Réponse DonnerRéponse(int stress)
    {
        if (stress <= 30)
        {
            return réponses.Find(x => x.Stress == 1);
        } 
        else if (stress > 30 && stress <= 60)
        {
            return réponses.Find(x => x.Stress == 2);
        }
        else if(stress > 60 && stress <= 100)
        {
            return réponses.Find(x => x.Stress == 3);
        }
        else
        {
            return null;
        }
    }
}