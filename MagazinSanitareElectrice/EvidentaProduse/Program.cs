using System;

namespace EvidentaProduse
{
    class Program
    {
        static void Main()
        {
            // Inițializarea obiectului care administrează produsele
            AdministrareProduse_Memorie adminProduse = new AdministrareProduse_Memorie();
            string optiune;

            do
            {
                // Afișează meniul
                AfiseazaMeniu();

                // Citirea opțiunii introduse de utilizator
                optiune = Console.ReadLine();

                // În funcție de opțiunea aleasă, se execută funcționalitatea corespunzătoare
                switch (optiune)
                {
                    case "1":
                        adminProduse.AdaugaProdus();
                        break;
                    case "2":
                        adminProduse.AfiseazaProduse();
                        break;
                    case "3":
                        adminProduse.CautaProdus();
                        break;
                    case "4":
                        adminProduse.StergeProdus();
                        break;
                    case "5":
                        adminProduse.CreareListaProiect();
                        break;
                    case "6":
                        Console.WriteLine("Ieșire din aplicație.");
                        break;
                    default:
                        // Afișează un mesaj în cazul în care utilizatorul a ales o opțiune invalidă
                        Console.WriteLine("Opțiune invalidă. Alegeți din nou.");
                        break;
                }
            } while (optiune != "6");
        }

        static void AfiseazaMeniu()
        {
            // Afișează opțiunile din meniul aplicației
            Console.WriteLine("\n===== Meniu Magazin =====");
            Console.WriteLine("1. Adăugare produs");
            Console.WriteLine("2. Afișare produse");
            Console.WriteLine("3. Căutare produs după ID");
            Console.WriteLine("4. Ștergere produs");
            Console.WriteLine("5. Creare listă pentru proiect");
            Console.WriteLine("6. Ieșire");
            Console.Write("Alege o opțiune: ");
        }
    }
}
