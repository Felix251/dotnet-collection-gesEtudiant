using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UsingCollectionsDemo
{
    public class Etudiant
    {
        public int NO { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public double NoteCC { get; set; }
        public double NoteDevoir { get; set; }
        
        // Calcul de la moyenne (33% pour le contrôle continu)
        public double CalculerMoyenne()
        {
            return Math.Round(NoteCC * 0.33 + NoteDevoir * 0.67, 2);
        }
        
        // Pour faciliter l'affichage
        public override string ToString()
        {
            return $"NO: {NO}, Nom: {Nom}, Prénom: {Prenom}, Note CC: {NoteCC}, Note Devoir: {NoteDevoir}, Moyenne: {CalculerMoyenne()}";
        }
    }
}