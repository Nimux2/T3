using Godot;
using T3Projet.Tools.Models;

namespace T3Projet.Tools.View;

public partial class QuestionsAffichage : Node2D
{
    private BoutonQuestion boutonQuestion1;
    private BoutonQuestion boutonQuestion2;
    private BoutonQuestion boutonQuestion3;
    private BoutonQuestion boutonQuestion4;
    private ColorRect masque;
    
    public static QuestionsAffichage instance;
    public override void _Ready()
    {
        boutonQuestion1 = GetChild<BoutonQuestion>(0);
        boutonQuestion2 = GetChild<BoutonQuestion>(1);
        boutonQuestion3 = GetChild<BoutonQuestion>(2);
        boutonQuestion4 = GetChild<BoutonQuestion>(3);
        masque = GetChild<ColorRect>(4);
        instance = this;
    }
    public void ChangerEtatMasque()
    {
        masque.Visible = !masque.Visible;
    }
    public void ChangerEtatMasque(bool active)
    {
        masque.Visible = active;
    }
    public void AfficheQuestionA(Question question , int index)
    {
        switch (index)
        {
            case 0:
                boutonQuestion1.Question = question;
                break;
            case 1:
                boutonQuestion2.Question = question;
                break;
            case 2:
                boutonQuestion3.Question = question;
                break;
            case 3:
                boutonQuestion4.Question = question;
                break;
        }
    }

    public void AfficheQuestionReset()
    {
        boutonQuestion1.Question = new Question();
        boutonQuestion2.Question = new Question();
        boutonQuestion3.Question = new Question();
        boutonQuestion4.Question = new Question();
    }
    
}