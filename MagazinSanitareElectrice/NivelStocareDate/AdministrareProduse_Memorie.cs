using LibrarieModele;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NivelStocareDate
{
    public class AdministrareProduse_Memorie
    {
        private const int NR_MAX_PRODUSE = 100;
        private Produs[] produse;
        private int nrProduse;
        private int idCurent;  // Variabila pentru ID-ul curent

        public AdministrareProduse_Memorie()
        {
            produse = new Produs[NR_MAX_PRODUSE];
            nrProduse = 0;
            idCurent = 1; // Începem ID-ul de la 1
        }

        // Adăugarea unui produs
        public void AdaugaProdus(Produs produs)
        {
            if (nrProduse < NR_MAX_PRODUSE)
            {
                produs.IdProdus = idCurent++; // Atribuim ID-ul curent și îl incrementăm
                produse[nrProduse++] = produs; // Adăugăm produsul în array
            }
            else
            {
                Console.WriteLine("Nu se mai pot adăuga produse, stoc maxim atins!");
            }
        }

        // Obținerea tuturor produselor
        public Produs[] GetProduse(out int nrProduse)
        {
            nrProduse = this.nrProduse;
            return produse.Take(nrProduse).ToArray(); // Returnăm doar produsele adăugate
        }

        // Căutare produs după ID sau Nume și afișarea lui
        public Produs CautaProdus(string optiune)
        {
            Produs produsGasit = null;

            // Căutăm după ID
            if (int.TryParse(optiune, out int idProdus))
            {
                produsGasit = produse.FirstOrDefault(p => p != null && p.IdProdus == idProdus);
            }
            // Căutăm după Nume
            else
            {
                produsGasit = produse.FirstOrDefault(p => p != null && p.Nume.Equals(optiune, StringComparison.OrdinalIgnoreCase));
            }

            if (produsGasit != null)
            {
                Console.WriteLine($"Produsul găsit: {produsGasit}");
            }
            else
            {
                Console.WriteLine("Produsul nu a fost găsit.");
            }

            return produsGasit;
        }

        // Ștergere produs
        public void StergeProdus(int idProdus)
        {
            var produsDeSters = produse.FirstOrDefault(p => p?.IdProdus == idProdus);
            if (produsDeSters != null)
            {
                produse = produse.Where(p => p?.IdProdus != idProdus).ToArray();
                nrProduse--;
                Console.WriteLine("Produs șters cu succes!");
            }
            else
            {
                Console.WriteLine("Produsul nu a fost găsit!");
            }
        }

        // Afișare produse
        public void AfiseazaProduse()
        {
            if (nrProduse == 0)
            {
                Console.WriteLine("Nu există produse în stoc.");
            }
            else
            {
                foreach (var produs in produse)
                {
                    if (produs != null)
                    {
                        Console.WriteLine(produs);
                    }
                }
            }
        }

        // Creare listă pentru proiect
        public void CreareListaProiect()
        {
            List<Produs> listaProiect = new List<Produs>();
            string optiune;

            do
            {
                Console.Write("Introduceți ID-ul sau Numele produsului de adăugat (sau 'ok' pentru a termina): ");
                optiune = Console.ReadLine().Trim();
                
                if (optiune.ToLower() != "ok")
                {
                    Produs produsAdaugat = CautaProdus(optiune);
                    if (produsAdaugat != null)
                    {
                        Console.Write("Introduceți cantitatea dorită: ");
                        if (int.TryParse(Console.ReadLine(), out int cantitateDorita) && cantitateDorita > 0)
                        {
                            if (cantitateDorita <= produsAdaugat.Cantitate)
                            {
                                listaProiect.Add(new Produs(produsAdaugat.Nume, produsAdaugat.Pret, cantitateDorita, produsAdaugat.Material, produsAdaugat.TipUtilizare));
                                produsAdaugat.Cantitate -= cantitateDorita; // Reducem cantitatea din stoc
                                Console.WriteLine($"Produs {produsAdaugat.Nume} adăugat la proiect.");
                            }
                            else
                            {
                                Console.WriteLine("Cantitatea solicitată nu este disponibilă.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Cantitate invalidă.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Produsul nu a fost găsit.");
                    }
                }
            } while (optiune.ToLower() != "ok");

            // Afișăm lista proiectului
            double totalPret = 0;
            Console.WriteLine("\nLista de produse pentru proiect:");
            foreach (var produs in listaProiect)
            {
                Console.WriteLine(produs);
                totalPret += produs.Pret * produs.Cantitate; // Adunăm prețul total pentru cantitățile adăugate
            }

            Console.WriteLine($"\nTotal produse: {listaProiect.Count}, Total cost: {totalPret} RON");
        }
    }
}
