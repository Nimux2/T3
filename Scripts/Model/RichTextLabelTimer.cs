using Godot;
using System;

namespace T3Projet.Scripts.Models;

public partial class RichTextLabelTimer : Timer
{
    /// <summary>
    /// Méthode Signal qui permet de transmettre la fin de l'affichage caractère par caractère.
    /// </summary>
    [Signal]
    public delegate void CharParCharFinEventHandler();
    
    // Attribut d'élément graphique.
    private RichTextLabel richTextLabelabel;
    
    // Liste des attributs pour l'affichage caractère par caractère.
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
    /// Méthode qui charge l'élément pour le text et charge la vitesse d'écriture pour l'affichage caractère par caractère.
    /// </summary>
    /// <returns></returns>
    public override void _Ready()
    {
        try
        {
            ConfigFile config = new ConfigFile();
            config.Load("res://Config/gameconfig.cfg");
            if (config.HasSection("Speak") && config.HasSectionKey("Speak", "speed"))
            {
                charSpeed = config.GetValue("Speak", "speed").As<double>();
            }
        }
        catch(Exception err) 
        {
            GD.Print("ERROR : Config = " + err.Message);
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
    /// Méthode pour afficher caractère par caractère du text "text".
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public void EcrireCharParChar(string text)
    {
        richTextLabelabel.Text = string.Empty;
        index = 0;
        this.text = text;
        this.Start();
    }
    
    /// <summary>
    /// Méthode qui affiche le prochain caractère ou émet le signal de fin d'affichage.
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