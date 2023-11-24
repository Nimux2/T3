using System.Data;
using Godot;
using Mono.Data.Sqlite;

namespace T3Projet.Tools.Models;

public class Réponse
{
    private int ID;
    private string réponse;
    public string RéponseText 
    {
        get => réponse;
    }
    private int stress;
    public int Stress 
    {
        get => stress;
    }
    
    public Réponse(int ID)
    {
        this.ID = ID;
        ChargerRéponse();
    }
    private void ChargerRéponse()
    {
        try
        {
            SqliteCommand command = new SqliteCommand()
            {
                Connection = DATABASE.GetConnection(),
                CommandType = CommandType.Text,
                CommandText = "SELECT * FROM Reponses WHERE id = @ID;",
            };
            command.Parameters.Add("@ID", DbType.Int32);
            command.Parameters[0].Value = ID;

            SqliteDataReader data = command.ExecuteReader();

            while (data.Read())
            {
                this.réponse = data.GetString(1);
                this.stress = data.GetInt32(2);
            }
        }
        catch (SqliteException err)
        {
            GD.Print("ERREUR DB Questions : " + err.Message);
        }
    }
}