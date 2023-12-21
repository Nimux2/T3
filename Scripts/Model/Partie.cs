using Godot;
using T3Projet.Scripts.View;

namespace T3Projet.Scripts.Models;

public class Partie
{
    // Liste des attributs de la partie.
    public int NbConsultation {get; set;}
    public int NbPatient { get; set; }
    public int RetardAvance {get; set;}
    public int DiagFaux {get; set;}
    public int StressEleve {get; set;}
    public double Argent {get; set;}
    public int Temps { get; set; }
    
    // Liste des constantes de jeu. (Pas des vrai constante car elles peuvent être modifié par le fichier de config)
    public static double PRIX_CONSULTATION = 25.0;
    public static int MIN_PATIENT = 8;
    public static int MAX_PATIENT = 16;
    public static int HEURE_DEPART = 8;
    public static int MINUTE_DEPART = 0;
    public static int HEURE_FIN = 12;
    public static int MINUTE_FIN = 0;
    
    public Partie()
    {
        NbConsultation = 0;
        GD.Randomize();
        NbPatient = GD.RandRange(MIN_PATIENT , MAX_PATIENT);
        RetardAvance = 0;
        DiagFaux = 0;
        StressEleve = 0;
        Argent = 0;
        Temps = 0;
    }
    
    /// <summary>
    /// Méthode qui permet le changement de tout les informations de la partie en fin de consultation.
    /// </summary>
    /// <param name="partieDataAffichage"></param>
    /// <returns></returns>
    public void ChangerInfoPartie(PartieDataAffichage partieDataAffichage)
    {
        partieDataAffichage.ChangerArgentMedecin(this.Argent);
        partieDataAffichage.ChangerPatientEnAttente(this.NbPatient - 1 - this.NbConsultation);
        partieDataAffichage.ChangerBonDiagnostic(this.NbConsultation - this.DiagFaux);
        partieDataAffichage.ChangerMauvaisDiagnostic(this.DiagFaux);
        partieDataAffichage.ChangerStressEleve(this.StressEleve);
    }
    
    /// <summary>
    /// Méthode qui permet le changement de l'affichage du temps après une question.
    /// </summary>
    /// <param name="partieDataAffichage"></param>
    /// <returns></returns>
    public void ChangerTemps(PartieDataAffichage partieDataAffichage)
    {
        partieDataAffichage.ChangerTempsJeu(HEURE_DEPART + (Temps / 60) , MINUTE_DEPART + (Temps - ((Temps / 60) * 60)));
    }
    
    /// <summary>
    /// Méthode qui permet de calculer le retard ou l'avance.
    /// </summary>
    /// <returns></returns>
    public void CalculRetardAvance()
    {
        int tmp = ((HEURE_FIN - HEURE_DEPART) * 60 + MINUTE_FIN - MINUTE_DEPART);
        int tempsPatient = tmp / NbPatient;
        this.RetardAvance = this.Temps - tempsPatient * NbConsultation;
    }
    
    /// <summary>
    /// Méthode qui retourne si le médecin a fini sa journée.
    /// </summary>
    /// <returns></returns>
    public bool TempsEstPassee()
    {
        return (((HEURE_FIN - HEURE_DEPART) * 60 + MINUTE_FIN - MINUTE_DEPART) <= Temps);
    }
    
    /// <summary>
    /// Méthode qui retourne si le médecin n'a plus de patient en salle d'attente.
    /// </summary>
    /// <returns></returns>
    public bool APlusDePatient()
    {
        return (NbConsultation == NbPatient);
    }
}