using Godot;

namespace T3Projet.Tools.View;

public partial class PartieDataAffichage : Node2D
{
    public static PartieDataAffichage instance;

    private Label argentData;
    private Label patientData;
    private Label bonDiagnosticData;
    private Label mauvaisDiagnosticData;
    private Label stressEleveData;
    public override void _Ready()
    {
        this.argentData = GetChild(1).GetChild<Label>(1);
        this.patientData = GetChild(2).GetChild<Label>(1);
        this.bonDiagnosticData = GetChild(3).GetChild<Label>(1);
        this.mauvaisDiagnosticData = GetChild(4).GetChild<Label>(1);
        this.stressEleveData = GetChild(5).GetChild<Label>(1);
        instance = this;
    }
    public void ChangerArgentMedecin(int argent)
    {
        argentData.Text = argent.ToString() + " euros";
    }
    public void ChangerPatientEnAttente(int nbPatient)
    {
        patientData.Text = nbPatient.ToString();
        patientData.Text += (nbPatient == 0 || nbPatient == 1) ? " patient" : " patients";
    }
    public void ChangerBonDiagnostic(int bonDiagnostic)
    {
        bonDiagnosticData.Text = bonDiagnostic.ToString();
        bonDiagnosticData.Text += (bonDiagnostic == 0 || bonDiagnostic == 1) ? " patient" : " patients";
    }
    public void ChangerMauvaisDiagnostic(int mauvaisDiagnostic)
    {
        mauvaisDiagnosticData.Text = mauvaisDiagnostic.ToString();
        mauvaisDiagnosticData.Text += (mauvaisDiagnostic == 0 || mauvaisDiagnostic == 1) ? " patient" : " patients";
    }
    public void ChangerStressEleve(int stressEleve)
    {
        stressEleveData.Text = stressEleve.ToString();
        stressEleveData.Text += (stressEleve == 0 || stressEleve == 1) ? " patient" : " patients";
    }
}