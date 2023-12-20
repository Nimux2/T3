using Godot;

namespace T3Projet.Tools.Models;

public partial class RichTextLabelTimer : Timer
{
    /// <summary>
    /// Méthode Signal qui permet de transmettre l'afin de l'affichage charactere par charactere.
    /// </summary>
    [Signal]
    public delegate void CharParCharFinEventHandler();
    
    // Attribute d'élément graphique.
    private RichTextLabel richTextLabelabel;
    
    // Liste des attributes pour l'affichage charactere par charactere.
    private string text;
    private int index;
    private static double charSpeed = 0.06;
    public static double CharSpeed 
    {
        get => charSpeed;
        set
        {
            charSpeed = value;
        }
    }
    
    /// <summary>
    /// Méthode qui charge l'élément pour le text et charge la vitesse d'écriture pour l'affichage charactere par charactere.
    /// </summary>
    /// <returns></returns>
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
    
    /// <summary>
    /// Méthode pour afficher le text "text".
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public void EcrireSimple(string text)
    {
        richTextLabelabel.Text = text;
    }
    
    /// <summary>
    /// Méthode pour afficher character par character du text "text".
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public void EcrireCharParChar(string text)
    {
        GD.Print("Speed : " + CharSpeed);
        richTextLabelabel.Text = string.Empty;
        index = 0;
        this.text = text;
        this.Start();
    }
    
    /// <summary>
    /// Méthode qui affiche le prochain charactere ou émet le signal de fin d'affichage.
    /// </summary>
    /// <returns></returns>
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