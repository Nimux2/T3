using Godot;

namespace T3Projet.Tools.View;

public partial class DebutAffichage : Node2D
{
    // Liste des attributes pour le texte par défault.
    private const string DEFAULT_TEXT_DEBUT = "Voulez-vous commencer une partie ?";
    private const string DEFAULT_TEXT_RECOMMENCE = "Voulez-vous recommencer une partie ?";
    
    // Attribute d'élément graphique.
    private Label infoDebut;
    
    // Singleton pour l'initialisation du jeu.
    public static DebutAffichage instance;
    
    /// <summary>
    /// Méthode qui charge l'élément pour le texte et initialise le singleton.
    /// </summary>
    /// <returns></returns>
    public override void _Ready()
    {
        infoDebut = GetChild<Label>(3);
        instance = this;
    }
    
    /// <summary>
    /// Méthode pour afficher un début de partie.
    /// </summary>
    /// <returns></returns>
    public void AfficheDebutDebut()
    {
        infoDebut.Text = DEFAULT_TEXT_DEBUT;
        this.Visible = true;
    }
    
    /// <summary>
    /// Méthode pour afficher un recommencement de partie.
    /// </summary>
    /// <returns></returns>
    public void AfficheDebutRecommence()
    {
        infoDebut.Text = DEFAULT_TEXT_RECOMMENCE;
        this.Visible = true;
    }
    
    /*
     *  Signal généré par Godot. ("Controlleur")
     */
    public void OnBoutonDebutPressed()
    {
        this.Visible = false;
    }
    
}