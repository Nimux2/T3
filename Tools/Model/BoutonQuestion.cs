using Godot;

namespace T3Projet.Tools.Models;

public partial class BoutonQuestion : Button
{
    
    /// <summary>
    /// Méthode Signal qui permet de transmettre les effets de la question
    /// </summary>
    [Signal]
    public delegate void BoutonQuestionPressedEventHandler(int diag, int stress, int temps);
    
    // Attribute d'élément graphique.
    private Label questionText;
    
    // Attribute pour les informations de la question.
    private Question question = new Question();
    public Question Question
    {
        get => question;
        set
        {
            question = value;
            questionText.Text = question.QuestionText;
        }
    }
    
    /// <summary>
    /// Méthode qui charge l'élément pour le texte et initialise l'émission du signal custom à la pression du bouton.
    /// </summary>
    /// <returns></returns>
    public override void _Ready()
    {
        questionText = GetChild<Label>(0);
        Pressed += () => EmitSignal(SignalName.BoutonQuestionPressed, this.question.EffetDiag,
            this.question.EffetStress, this.question.EffetTemps);
    }
}