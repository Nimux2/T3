using Godot;

namespace T3Projet.Tools.Models;

public partial class BoutonQuestion : Button
{
    private Label questionText;
    private Question question;
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
        Pressed += () => GamePlay.instance.OnBoutonQuestionPressed(this.question.EffetDiag , this.Question.EffetStress);
    }
    
    
}