using Godot;
using System;
using System.Threading.Tasks;
using Godot.Collections;

public partial class Personnage : Sprite2D
{
    // Called when the node enters the scene tree for the first time.
    private RichTextLabel answer;
    private Array<Node> allNodes;
    private string name = "Anonyme";
    public override void _Ready()
    {
        allNodes = GetTree().GetNodesInGroup("Personnage");
        answer = allNodes[3] as RichTextLabel;
        this.speech("Hello Doctor, I am [name] !");
        autoPlacement();
    }
    public void changePersonnage(string name , string image_name)
    {
        this.name = formatName(name);
        this.changePersonnageCaractere(image_name);
    }
    public void changePersonnageCaractere(string image_name)
    {
        this.changeTexture(image_name);
        this.autoPlacement();
    }
    public void speech(string texte)
    {
        string temp = texte.Replace("[name]", this.name);
        answer.Text = temp;
    }
    private void changeTexture(string image_name)
    {
        string path =  "./patient/" + image_name + ".png";
        Texture2D texture = GD.Load<Texture2D>(path);
        this.Texture = texture;
        answer.Text = String.Empty;
    }
    private void autoPlacement()
    {
        Marker2D markerTop = allNodes[0] as Marker2D;
        Marker2D markerBottom = allNodes[2] as Marker2D;
        Vector2 originPositionCalcul = new Vector2();
        originPositionCalcul.X = markerBottom.GlobalPosition.X;
        originPositionCalcul.Y = (markerBottom.GlobalPosition.Y + markerTop.GlobalPosition.Y) / 2;
        this.GlobalPosition = originPositionCalcul;
        float scaleCalcul = (markerBottom.GlobalPosition.Y - markerTop.GlobalPosition.Y) / this.Texture.GetHeight();
        this.Scale = new Vector2(scaleCalcul, scaleCalcul);
    }

    private string formatName(string name)
    {
        if (!char.IsUpper((name.ToCharArray())[0]))
        {
            return name.Substring(0, 1).ToUpper() + name.Substring(1);
        }
        else
        {
            return name;
        }
    }

    public void speechChar(string texte)
    {
        answer.Text = string.Empty;
        Action<object> action = delegate(object o)
        {
            char[] temp = ((string)o).ToCharArray();
            foreach (var _char in temp)
            {
                answer.Text += _char;
                Task.Delay(80).Wait();
            }
        };
        Task task = new Task(action, texte.Replace("[name]", this.name));
        task.Start();
    }
    public static Personnage From(Sprite2D sprite2D)
    {
        return sprite2D as Personnage;
    }
}