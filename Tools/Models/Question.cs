using System.Data;
using Godot;
using Mono.Data.Sqlite;

namespace T3Projet.Tools.Models;

public class Question
{

    private int ID;
    private int ordre;
    private string question;
    public string QuestionText
    {
        get => question;
    }
    private int effetStress;
    public int EffetStress
    {
        get => effetStress;
    }
    private int effetDiag;
    public int EffetDiag
    {
        get => effetDiag;
    }

    public Question() { }

    public Question(int ID)
    {
        this.ID = ID;
        ChargerQuestion();
    }
    private void ChargerQuestion()
    {
        try
        {
            SqliteCommand command = new SqliteCommand()
            {
                Connection = DATABASE.GetConnection(),
                CommandType = CommandType.Text,
                CommandText = "SELECT * FROM Questions WHERE id = @ID;",
            };
            command.Parameters.Add("@ID", DbType.Int32);
            command.Parameters[0].Value = ID;

            SqliteDataReader data = command.ExecuteReader();

            while (data.Read())
            {
                this.ordre = data.GetInt32(1);
                this.question = data.GetString(2);
                this.effetStress = data.GetInt32(3);
                this.effetDiag = data.GetInt32(4);
            }
        }
        catch (SqliteException err)
        {
            GD.Print("ERREUR DB Questions : " + err.Message);
        }
    }
}