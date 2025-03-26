using System;
using LibrarieModele;
using NivelStocareDate;
using System.IO;

namespace EvidentaProduse
{
    class Program
    {
        static void Main()
        {
            // Creează calea fișierului
            string pathFisier = Path.Combine("NivelStocareDate", "produse.txt");

              // Verificăm dacă directorul există
            string director = Path.GetDirectoryName(pathFisier);
            if (!Directory.Exists(director))
            {
                Directory.CreateDirectory(director);
                Console.WriteLine($"Directorul {director} a fost creat.");
            }

            // Creează instanța AdministrareProduse_FisierText cu calea fișierului
            AdministrareProduse_FisierText adminProduse = new AdministrareProduse_FisierText(pathFisier);

            string optiune;

            do
            {
                // Afișăm meniul
                AfiseazaMeniu();
                optiune = Console.ReadLine();

                // Alegerea opțiunii dorite
                switch (optiune)
                {
                    case "1":
                        // Adăugăm un produs
                        Produs produsNou = CreeazaProdus();
                        if (produsNou != null)
                        {
                            adminProduse.AddProdus(produsNou);  // Adăugăm produsul în fișier
                            Console.WriteLine("Produs adăugat cu succes.");
                        }

                        // După adăugarea unui produs, afișăm toate produsele din fișier
                        var produseDinFisier1 = adminProduse.GetProduse(out int nrProduse1);
                        Console.WriteLine($"Produse salvate: {nrProduse1}");
                        foreach (var produs in produseDinFisier1)
                        {
                            Console.WriteLine(produs.ToString());
                        }
                        break;

                    case "2":
                        // Afișăm lista de produse din fișier
                        Console.WriteLine("Afișăm produsele existente:");
                        var produseDinFisier = adminProduse.GetProduse(out int nrProduse);
                        Console.WriteLine($"Număr produse: {nrProduse}");
                        if (nrProduse == 0)
                        {
                            Console.WriteLine("Nu există produse în fișier.");
                        }
                        else
                        {
                            foreach (var produs in produseDinFisier)
                            {
                                Console.WriteLine(produs.ToString());
                            }
                        }
                        break;

                    case "3":
                        // Căutăm un produs
                        Console.Write("Introduceți ID-ul, Numele, Materialul sau Tipul de utilizare al produsului: ");
                        string cautare = Console.ReadLine();
                        // Adaugă logica de căutare produs
                        break;

                    case "4":
                        // Ștergem un produs
                        Console.Write("Introduceți ID-ul produsului de șters: ");
                        if (int.TryParse(Console.ReadLine(), out int idStergere))
                        {
                            adminProduse.StergeProdus(idStergere);
                        }
                        break;

                    case "5":
                        // Creăm o listă pentru proiect
                        //adminProduse.();
                        break;

                    case "6":
                        // Citim produsele din fișier și le afișăm
                        Console.WriteLine("Citire produse din fișier:");
                        var produseDinFisier6 = adminProduse.GetProduse(out int nrProduseFisier);
                        Console.WriteLine($"Produse citite din fișier: {nrProduseFisier}");
                        foreach (var produs in produseDinFisier6)
                        {
                            Console.WriteLine(produs.ToString());
                        }
                        break;

                    case "7":
                        // Ieșim din aplicație
                        Console.WriteLine("Ieșire din aplicație.");
                        return;

                    default:
                        Console.WriteLine("Opțiune invalidă. Alegeți din nou.");
                        break;
                }
            } while (optiune != "7");
        }

        static void AfiseazaMeniu()
        {
            // Meniul aplicației
            Console.WriteLine("\n===== Meniu Magazin =====");
            Console.WriteLine("1. Adăugare produs");
            Console.WriteLine("2. Afișare produse");
            Console.WriteLine("3. Căutare produs după ID, Nume, Material sau TipUtilizare");
            Console.WriteLine("4. Ștergere produs");
            Console.WriteLine("5. Creare listă pentru proiect");
            Console.WriteLine("6. Citire produse din fișier");
            Console.WriteLine("7. Ieșire");
            Console.Write("Alege o opțiune: ");
        }

        static Produs CreeazaProdus()
        {
            // Crearea unui nou produs din inputul utilizatorului
            Console.Write("Introduceți numele produsului: ");
            string nume = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nume))
            {
                Console.WriteLine("Numele produsului nu poate fi gol.");
                return null;
            }

            Console.Write("Introduceți prețul produsului: ");
            if (!double.TryParse(Console.ReadLine(), out double pret) || pret <= 0)
            {
                Console.WriteLine("Preț invalid.");
                return null;
            }

            int cantitate = 0;
            do
            {
                Console.Write("Introduceți cantitatea produsului: ");
            } while (!int.TryParse(Console.ReadLine(), out cantitate) || cantitate <= 0);

            Console.WriteLine("Selectați materialul produsului: ");
            foreach (var mat in Enum.GetValues(typeof(TipMaterial)))
            {
                Console.WriteLine($"{(int)mat} - {mat}");
            }
            TipMaterial material =  (TipMaterial)int.Parse(Console.ReadLine());

            Console.WriteLine("Selectați tipul de utilizare: ");
            foreach (var tip in Enum.GetValues(typeof(Utilizare)))
            {
                Console.WriteLine($"{(int)tip} - {tip}");
            }
            Utilizare tipUtilizare = (Utilizare)int.Parse(Console.ReadLine());

            // Returnăm produsul creat
            return new Produs(nume, pret, cantitate, material, tipUtilizare);
        }
    }
}