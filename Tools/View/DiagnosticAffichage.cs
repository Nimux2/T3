using Godot;

namespace T3Projet.Tools.View;

public partial class DiagnosticAffichage : Node2D
{
    public static DiagnosticAffichage instance;
    
    private const string DEFAULT_TEXT_DIAG = "Vous avez trouvé la maladie : [maladie]";
    private const string DEFAULT_TEXT_PAS_DIAG = "Vous n'avez pas trouvé la maladie : [maladie]";
    private const string DEFAULT_TEXT_RETARD = "Vous avez [temps] minutes de retard.";
    private const string DEFAULT_TEXT_AVANCE = "Vous avez [temps] minutes d'avance.";
	
    private Label infoDiagnostic;
    private Label infoRetard;
    public override void _Ready()
    {
        infoDiagnostic = GetChild<Label>(3);
        infoRetard = GetChild<Label>(4);
        instance = this;
    }

    public void AfficheDiagnosticVraiRetard(string maladie , int retard)
    {
        this.Visible = true;
        infoDiagnostic.Text = DEFAULT_TEXT_DIAG.Replace("[maladie]", maladie);
        infoRetard.Text = DEFAULT_TEXT_RETARD.Replace("[temps]", retard.ToString());
    }
    public void AfficheDiagnosticVraiAvance(string maladie , int avance)
    {
        this.Visible = true;
        infoDiagnostic.Text = DEFAULT_TEXT_DIAG.Replace("[maladie]", maladie);
        infoRetard.Text = DEFAULT_TEXT_AVANCE.Replace("[temps]", avance.ToString());
    }
    public void AfficheDiagnosticFauxRetard(string maladie , int retard)
    {
        this.Visible = true;
        infoDiagnostic.Text = DEFAULT_TEXT_PAS_DIAG.Replace("[maladie]", maladie);
        infoRetard.Text = DEFAULT_TEXT_RETARD.Replace("[temps]", retard.ToString());
    }
    public void AfficheDiagnosticFauxAvance(string maladie , int avance)
    {
        this.Visible = true;
        infoDiagnostic.Text = DEFAULT_TEXT_PAS_DIAG.Replace("[maladie]", maladie);
        infoRetard.Text = DEFAULT_TEXT_AVANCE.Replace("[temps]", avance.ToString());
    }

    public void OnBoutonContinuerPressed()
    {
        this.Visible = false;
    }
    
}