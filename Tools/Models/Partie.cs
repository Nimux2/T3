using T3Projet.Tools.View;

namespace T3Projet.Tools.Models;

public class Partie
{
    public int NbConsultation {get; set;}
    public int RetardAvance {get; set;}
    public int DiagFaux {get; set;}
    public int StressEleve {get; set;}
    public int Argent {get; set;}
    
    public Partie()
    {
        NbConsultation = 0;
        RetardAvance = 0;
        DiagFaux = 0;
        StressEleve = 0;
        Argent = 0;
    }
    
    public void ChangerInfoPartie(PartieDataAffichage partieDataAffichage)
    {
        partieDataAffichage.ChangerArgentMedecin(this.Argent);
        partieDataAffichage.ChangerPatientEnAttente(TimerController.CalculNbConsultation() - 1 - this.NbConsultation);
        partieDataAffichage.ChangerBonDiagnostic(this.NbConsultation - this.DiagFaux);
        partieDataAffichage.ChangerMauvaisDiagnostic(this.DiagFaux);
        partieDataAffichage.ChangerStressEleve(this.StressEleve);
    }
}