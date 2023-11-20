using Godot;
using System;
using System.Threading.Tasks;
using Godot.Collections;

public partial class Personnage : Sprite2D
{
    private RichTextLabel answer;
    private Array<Node> allNodes;
    private string name = "Anonyme";
    private CustomSignals signal;
    public override void _Ready()
    {
        signal = GetNode<CustomSignals>("/root/CustomSignal");
        signal.PersonnageChangePersonnage += (name, imageName) => ChangePersonnage(name, imageName);
        signal.PersonnageSpeech += texte => Speech(texte);
        signal.PersonnageSpeechChar += texte => SpeechChar(texte);
        allNodes = GetTree().GetNodesInGroup("Personnage");
        answer = allNodes[3] as RichTextLabel;
        this.Speech("Hello Doctor, I am [name] !");
        AutoPlacement();
    }
    private void ChangePersonnage(string name , string imageName)
    {
        this.name = FormatName(name);
        this.ChangePersonnageCaractere(imageName);
    }
    private void ChangePersonnageCaractere(string imageName)
    {
        this.ChangeTexture(imageName);
        this.AutoPlacement();
    }
    private void Speech(string texte)
    {
        string temp = texte.Replace("[name]", this.name);
        answer.Text = temp;
    }
    private void ChangeTexture(string imageName)
    {
        string path =  "./patient/" + imageName + ".png";
        Texture2D texture = GD.Load<Texture2D>(path);
        this.Texture = texture;
        answer.Text = String.Empty;
    }
    private void AutoPlacement()
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

    private string FormatName(string name)
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
    //ligne de code sous commentaire => code pour du TTS (text to speak)
    private void SpeechChar(string texte)
    {
        answer.Text = string.Empty;
        Action<object> actionDisplay = delegate(object o)
        {
            char[] temp = ((string)o).ToCharArray();
            foreach (var _char in temp)
            {
                answer.Text += _char;
                Task.Delay(80).Wait();
            }
        };
        
        /*
        Action<object> actionSpeak = delegate(object o)
        {
            GD.Print("Start speak");
            string temp = ((string)o);
            string[] voices = DisplayServer.TtsGetVoicesForLanguage("fr");
            string voiceId = voices[0];
            DisplayServer.TtsSpeak(temp , voiceId);
            GD.Print("End speak");
        };
        */
        
        Task task1 = new Task(actionDisplay, texte.Replace("[name]", this.name));
        //Task task2 = new Task(actionSpeak, texte.Replace("[name]", this.name));
        task1.Start();
        //task2.Start();
    }
}