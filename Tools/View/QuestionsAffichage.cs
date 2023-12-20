using Godot;
using T3Projet.Tools.Models;

namespace T3Projet.Tools.View;

public partial class QuestionsAffichage : Node2D
{
    // Liste des attributes des éléments graphique.
    private BoutonQuestion boutonQuestion1;
    private BoutonQuestion boutonQuestion2;
    private BoutonQuestion boutonQuestion3;
    private BoutonQuestion boutonQuestion4;
    private ColorRect masque;
    
    // Singleton pour l'initialisation du jeu.
    public static QuestionsAffichage instance;
    
    /// <summary>
    /// Méthode qui charge les éléments pour le texte et initialise le singleton.
    /// </summary>
    /// <returns></returns>
    public override void _Ready()
    {
        boutonQuestion1 = GetChild<BoutonQuestion>(0);
        boutonQuestion2 = GetChild<BoutonQuestion>(1);
        boutonQuestion3 = GetChild<BoutonQuestion>(2);
        boutonQuestion4 = GetChild<BoutonQuestion>(3);
        masque = GetChild<ColorRect>(4);
        instance = this;
    }
    /// <summary>
    /// Méthode qui change l'état du masque qui bloque/débloque les boutons de question par rapport l'état présédent.
    /// </summary>
    /// <returns></returns>
    public void ChangerEtatMasque()
    {
        masque.Visible = !masque.Visible;
    }
    
    /// <summary>
    /// Méthode qui change l'état du masque qui bloque/débloque les boutons de question par rapport à "active".
    /// </summary>
    /// <param name="active"></param>
    /// <returns></returns>
    public void ChangerEtatMasque(bool active)
    {
        masque.Visible = active;
    }
    
    /// <summary>
    /// Méthode qui permet l'ajout de la question "question" au bouton d'index "index".
    /// </summary>
    /// <param name="question"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public void AfficheQuestion(Question question , int index)
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
    
    /// <summary>
    /// Méthode pour réinitialiser les boutons de question
    /// </summary>
    /// <returns></returns>
    public void AfficheQuestionReset()
    {
        boutonQuestion1.Question = new Question();
        boutonQuestion2.Question = new Question();
        boutonQuestion3.Question = new Question();
        boutonQuestion4.Question = new Question();
    }
    
}