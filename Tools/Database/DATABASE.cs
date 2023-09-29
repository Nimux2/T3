using Godot;

//https://unintuitive.net/code/working-in-godot-with-sqlite-and-c/
using System.Data;
using Mono.Data.Sqlite;

public partial class DATABASE : Node
{
	//private var
	private SqliteConnection _connection;
	private SqliteCommand _command;
	public override void _Ready()
	{
		string path = $"Data Source={System.IO.Directory.GetCurrentDirectory()}/Tools/Database/database_t3.db;Version=3;";
		try
		{
			_connection = new SqliteConnection(path);
			_connection.Open();
			_command = new SqliteCommand()
			{
				Connection = _connection,
				CommandType = CommandType.Text,
				CommandText = "SELECT name FROM sqlite_master WHERE type = ?"
			};
			_command.Parameters.Add("id", DbType.String);
			_command.Parameters[0].Value = "table";
			GD.Print("Execute commande");
			SqliteDataReader tables_name = _command.ExecuteReader();
			while (tables_name.Read())
			{
				GD.Print($"table name : {tables_name["name"]}");
			}
		}
		catch (SqliteException err)
		{
			GD.Print(err.Message);
		}
		finally
		{
			_connection.Close();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
