using Godot;

namespace T3Projet.Tools.Models;

public partial class BoutonQuestion : Button
{
    [Signal]
    public delegate void BoutonQuestionPressedEventHandler(int diag, int stress);
    
    private Label questionText;
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
    public override void _Ready()
    {
        questionText = GetChild<Label>(0);
        Pressed += () => EmitSignal(SignalName.BoutonQuestionPressed, this.question.EffetDiag,
            this.question.EffetStress);
    }
}