using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UsingCollectionsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Création de la SortedList pour stocker les étudiants
            SortedList listeEtudiants = new SortedList();
            
            Console.WriteLine("=== GESTION DES NOTES DE CLASSE ===\n");
            
            // Demander le nombre d'étudiants
            Console.Write("Combien d'étudiants voulez-vous saisir ? ");
            int nbEtudiants;
            while (!int.TryParse(Console.ReadLine(), out nbEtudiants) || nbEtudiants <= 0)
            {
                Console.Write("Veuillez entrer un nombre valide d'étudiants : ");
            }
            
            // Saisie des informations pour chaque étudiant
            for (int i = 0; i < nbEtudiants; i++)
            {
                Console.WriteLine($"\nSaisie des informations pour l'étudiant #{i+1}");
                
                Etudiant etudiant = new Etudiant();
                
                // Saisie du numéro d'ordre
                Console.Write("Numéro d'ordre (NO) : ");
                int no;
                while (!int.TryParse(Console.ReadLine(), out no) || no <= 0 || listeEtudiants.ContainsKey(no))
                {
                    if (listeEtudiants.ContainsKey(no))
                        Console.Write("Ce numéro existe déjà. Veuillez entrer un autre numéro : ");
                    else
                        Console.Write("Veuillez entrer un numéro valide : ");
                }
                etudiant.NO = no;
                
                // Saisie du nom
                Console.Write("Nom : ");
                etudiant.Nom = Console.ReadLine()?.Trim() ?? "";
                
                // Saisie du prénom
                Console.Write("Prénom : ");
                etudiant.Prenom = Console.ReadLine()?.Trim() ?? "";
                
                // Saisie de la note de contrôle continu
                Console.Write("Note du contrôle continu (sur 20) : ");
                double noteCC;
                while (!double.TryParse(Console.ReadLine(), out noteCC) || noteCC < 0 || noteCC > 20)
                {
                    Console.Write("Veuillez entrer une note valide (entre 0 et 20) : ");
                }
                etudiant.NoteCC = noteCC;
                
                // Saisie de la note de devoir
                Console.Write("Note du devoir (sur 20) : ");
                double noteDevoir;
                while (!double.TryParse(Console.ReadLine(), out noteDevoir) || noteDevoir < 0 || noteDevoir > 20)
                {
                    Console.Write("Veuillez entrer une note valide (entre 0 et 20) : ");
                }
                etudiant.NoteDevoir = noteDevoir;
                
                // Ajout de l'étudiant à la liste
                listeEtudiants.Add(etudiant.NO, etudiant);
            }
            
            // Configuration de la pagination
            Console.WriteLine("\n=== PARAMÈTRES D'AFFICHAGE ===");
            Console.Write("Nombre de lignes par page (1-15, défaut: 5) : ");
            string input = Console.ReadLine();
            int lignesParPage = 5; // Valeur par défaut
            
            if (!string.IsNullOrWhiteSpace(input))
            {
                while (!int.TryParse(input, out lignesParPage) || lignesParPage < 1 || lignesParPage > 15)
                {
                    Console.Write("Veuillez entrer un nombre entre 1 et 15 : ");
                    input = Console.ReadLine();
                }
            }
            
            // Calcul du nombre total de pages
            int totalPages = (int)Math.Ceiling((double)listeEtudiants.Count / lignesParPage);
            int pageActuelle = 1;
            bool continuer = true;
            
            // Calcul de la moyenne de la classe
            double sommeDesNotes = 0;
            foreach (DictionaryEntry entry in listeEtudiants)
            {
                Etudiant etudiant = (Etudiant)entry.Value;
                sommeDesNotes += etudiant.CalculerMoyenne();
            }
            double moyenneClasse = listeEtudiants.Count > 0 ? Math.Round(sommeDesNotes / listeEtudiants.Count, 2) : 0;
            
            // Affichage paginé des résultats
            while (continuer)
            {
                Console.Clear();
                Console.WriteLine($"=== LISTE DES NOTES (Page {pageActuelle}/{totalPages}) ===\n");
                
                // En-tête du tableau
                Console.WriteLine("{0,-5} {1,-15} {2,-15} {3,-10} {4,-10} {5,-10}",
                    "NO", "Nom", "Prénom", "Note CC", "Devoir", "Moyenne");
                Console.WriteLine(new string('-', 70));
                
                // Calcul des indices pour la pagination
                int debut = (pageActuelle - 1) * lignesParPage;
                int fin = Math.Min(debut + lignesParPage, listeEtudiants.Count);
                
                // Affichage des étudiants pour la page actuelle
                for (int i = debut; i < fin; i++)
                {
                    Etudiant etudiant = (Etudiant)listeEtudiants.GetByIndex(i);
                    double moyenne = etudiant.CalculerMoyenne();
                    
                    Console.WriteLine("{0,-5} {1,-15} {2,-15} {3,-10} {4,-10} {5,-10}",
                        etudiant.NO, etudiant.Nom, etudiant.Prenom, etudiant.NoteCC,
                        etudiant.NoteDevoir, moyenne);
                }
                
                // Affichage de la moyenne de la classe sur la dernière page
                if (pageActuelle == totalPages)
                {
                    Console.WriteLine(new string('-', 70));
                    Console.WriteLine($"Moyenne de la classe : {moyenneClasse}/20");
                }
                
                // Menu de navigation
                Console.WriteLine("\nOptions :");
                if (totalPages > 1)
                {
                    if (pageActuelle > 1)
                        Console.WriteLine("P - Page précédente");
                    if (pageActuelle < totalPages)
                        Console.WriteLine("S - Page suivante");
                }
                Console.WriteLine("Q - Quitter le programme");
                
                Console.Write("\nVotre choix : ");
                string choix = Console.ReadLine()?.ToUpper() ?? "";
                
                switch (choix)
                {
                    case "P" when pageActuelle > 1:
                        pageActuelle--;
                        break;
                    case "S" when pageActuelle < totalPages:
                        pageActuelle++;
                        break;
                    case "Q":
                        continuer = false;
                        break;
                }
            }
            
            Console.WriteLine("\nprogramme de gestion des notes End!");
        }
    }
}