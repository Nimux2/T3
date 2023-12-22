using Godot;

namespace T3Projet.Scripts.View;

public partial class DiagnosticAffichage : Node2D
{
    // Liste des attributs pour le texte par défault.
    private const string DEFAULT_TEXT_DIAG = "Vous avez trouvé la maladie : [maladie]";
    private const string DEFAULT_TEXT_PAS_DIAG = "Vous n'avez pas trouvé la maladie : [maladie]";
    private const string DEFAULT_TEXT_RETARD = "Vous avez [temps] minutes de retard.";
    private const string DEFAULT_TEXT_AVANCE = "Vous avez [temps] minutes d'avance.";
    private const string DEFAULT_TEXT_DIAG_STRESS = "Vous n'avez pas trouvé la maladie : [maladie].\n Le patient est parti à cause du stress.";
	
    // Liste des attributs des éléments graphique.
    private Label infoDiagnostic;
    private Label infoRetard;
    
    // Singleton pour l'initialisation du jeu.
    public static DiagnosticAffichage instance;
    
    /// <summary>
    /// Méthode qui charge les éléments pour le texte et initialise le singleton.
    /// </summary>
    /// <returns></returns>
    public override void _Ready()
    {
        infoDiagnostic = GetChild<Label>(3);
        infoRetard = GetChild<Label>(4);
        instance = this;
    }
    
    /// <summary>
    /// Méthode qui affiche un diagnostic juste avec retard.
    /// </summary>
    /// <param name="maladie"></param>
    /// <param name="retard"></param>
    /// <returns></returns>
    public void AfficherDiagnosticVraiRetard(string maladie , int retard)
    {
        this.Visible = true;
        infoDiagnostic.Text = DEFAULT_TEXT_DIAG.Replace("[maladie]", maladie);
        infoRetard.Text = DEFAULT_TEXT_RETARD.Replace("[temps]", retard.ToString());
    }
    
    /// <summary>
    /// Méthode qui affiche un diagnostic juste avec avance.
    /// </summary>
    /// <param name="maladie"></param>
    /// <param name="avance"></param>
    /// <returns></returns>
    public void AfficherDiagnosticVraiAvance(string maladie , int avance)
    {
        this.Visible = true;
        infoDiagnostic.Text = DEFAULT_TEXT_DIAG.Replace("[maladie]", maladie);
        infoRetard.Text = DEFAULT_TEXT_AVANCE.Replace("[temps]", avance.ToString());
    }
    
    /// <summary>
    /// Méthode qui affiche un diagnostic faux avec retard.
    /// </summary>
    /// <param name="maladie"></param>
    /// <param name="retard"></param>
    /// <returns></returns>
    public void AfficherDiagnosticFauxRetard(string maladie , int retard)
    {
        this.Visible = true;
        infoDiagnostic.Text = DEFAULT_TEXT_PAS_DIAG.Replace("[maladie]", maladie);
        infoRetard.Text = DEFAULT_TEXT_RETARD.Replace("[temps]", retard.ToString());
    }
    
    /// <summary>
    /// Méthode qui affiche un diagnostic faux avec avance.
    /// </summary>
    /// <param name="maladie"></param>
    /// <param name="avance"></param>
    /// <returns></returns>
    public void AfficherDiagnosticFauxAvance(string maladie , int avance)
    {
        this.Visible = true;
        infoDiagnostic.Text = DEFAULT_TEXT_PAS_DIAG.Replace("[maladie]", maladie);
        infoRetard.Text = DEFAULT_TEXT_AVANCE.Replace("[temps]", avance.ToString());
    }
    
    /// <summary>
    /// Méthode qui affiche un diagnostic faux pour cause de stress avec retard.
    /// </summary>
    /// <param name="maladie"></param>
    /// <param name="retard"></param>
    public void AfficherDiagnosticStressRetard(string maladie , int retard)
    {
        this.Visible = true;
        infoDiagnostic.Text = DEFAULT_TEXT_DIAG_STRESS.Replace("[maladie]", maladie);
        infoRetard.Text = DEFAULT_TEXT_RETARD.Replace("[temps]", retard.ToString());
    }
    /// <summary>
    /// Méthode qui affiche un diagnostic faux pour cause de stress avec avance.
    /// </summary>
    /// <param name="maladie"></param>
    /// <param name="avance"></param>
    public void AfficherDiagnosticStressAvance(string maladie , int avance)
    {
        this.Visible = true;
        infoDiagnostic.Text = DEFAULT_TEXT_DIAG_STRESS.Replace("[maladie]", maladie);
        infoRetard.Text = DEFAULT_TEXT_AVANCE.Replace("[temps]", avance.ToString());
    }
    
    /*
     *  Signal généré par Godot. ("Controlleur")
     */
    public void OnBoutonContinuerPressed()
    {
        this.Visible = false;
    }
    
}