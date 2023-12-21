using Godot;

namespace T3Projet.Scripts.View;

public partial class DébutAffichage : Node2D
{
    // Liste des attributes pour le texte par défault.
    private const string DEFAULT_TEXT_DEBUT = "Voulez-vous commencer une partie ?";
    private const string DEFAULT_TEXT_RECOMMENCE = "Voulez-vous recommencer une partie ?";
    
    // Attribute d'élément graphique.
    private Label infoDébut;
    
    // Singleton pour l'initialisation du jeu.
    public static DébutAffichage instance;
    
    /// <summary>
    /// Méthode qui charge l'élément pour le texte et initialise le singleton.
    /// </summary>
    /// <returns></returns>
    public override void _Ready()
    {
        infoDébut = GetChild<Label>(3);
        instance = this;
    }
    
    /// <summary>
    /// Méthode pour afficher un début de partie.
    /// </summary>
    /// <returns></returns>
    public void AfficherDébutDébut()
    {
        infoDébut.Text = DEFAULT_TEXT_DEBUT;
        this.Visible = true;
    }
    
    /// <summary>
    /// Méthode pour afficher un recommencement de partie.
    /// </summary>
    /// <returns></returns>
    public void AfficherDébutRecommence()
    {
        infoDébut.Text = DEFAULT_TEXT_RECOMMENCE;
        this.Visible = true;
    }
    public void OnBoutonDébutPressed()
    {
        this.Visible = false;
    }
    
}