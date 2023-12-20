using Godot;

namespace T3Projet.Tools.Models;

public partial class RichTextLabelTimer : Timer
{
    [Signal]
    public delegate void CharParCharFinEventHandler();
    
    private static double charSpeed = 0.06;
    public static double CharSpeed 
    {
        get => charSpeed;
        set
        {
            charSpeed = value;
        }
    }

    private RichTextLabel richTextLabelabel;
    private string text;
    private int index;
    public override void _Ready()
    {
        ConfigFile config = new ConfigFile();
        config.Load("res://Config/gameconfig.cfg");
        if (config.HasSection("Speak") && config.HasSectionKey("Speak" , "speed"))
        {
            charSpeed = config.GetValue("Speak", "speed").As<double>();
        }
        this.WaitTime = CharSpeed;
        richTextLabelabel= GetChild<RichTextLabel>(0);
        this.Timeout += () => AfficherChar();
    }
    public void EcrireSimple(string text)
    {
        richTextLabelabel.Text = text;
    }
    public void EcrireCharParChar(string text)
    {
        GD.Print("Speed : " + CharSpeed);
        richTextLabelabel.Text = string.Empty;
        index = 0;
        this.text = text;
        this.Start();
    }
    private void AfficherChar()
    {
        if (index < text.Length)
        {
            richTextLabelabel.Text += text[index];
            index++;
        }
        else
        {
            this.Stop();
            EmitSignal(SignalName.CharParCharFin);
        }
    }
    
}