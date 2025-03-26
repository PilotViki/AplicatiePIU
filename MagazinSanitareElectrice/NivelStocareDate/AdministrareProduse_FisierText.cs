using System;
using System.IO;
using LibrarieModele;
using System.Collections.Generic;
using System.Linq;

namespace NivelStocareDate
{
    public class AdministrareProduse_FisierText
    {
        private string numeFisier;

        public AdministrareProduse_FisierText(string numeFisier)
        {
            this.numeFisier = numeFisier;

            try
            {
                if (!File.Exists(numeFisier))
                {
                    using (Stream streamFisier = File.Open(numeFisier, FileMode.Create)) { }
                   // Console.WriteLine($"Fișierul {numeFisier} a fost creat.");
                }
                else
                {
                   // Console.WriteLine($"Fișierul {numeFisier} există deja.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A apărut o eroare la crearea fișierului: {ex.Message}");
            }
        }

        public void AddProdus(Produs produs)
        {
            try
            {
                var produseExistente = GetProduse(out int nrProduse);

                // Obține ID-ul următor disponibil
                int idProdusNou = produseExistente.Count > 0 ? produseExistente.Max(p => p.IdProdus) + 1 : 1;

                using (StreamWriter sw = new StreamWriter(numeFisier, true))
                {
                    sw.WriteLine($"{idProdusNou}|{produs.Nume}|{produs.Pret}|{produs.Cantitate}|{produs.Material}|{produs.TipUtilizare}");
                }

                Console.WriteLine("Produs adăugat cu succes.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A apărut o eroare la adăugarea produsului: {ex.Message}");
            }
        }



        public List<Produs> GetProduse(out int nrProduse)
        {
            List<Produs> produse = new List<Produs>();
            nrProduse = 0;

            try
            {
                using (StreamReader sr = new StreamReader(numeFisier))
                {
                    string linie;
                    while ((linie = sr.ReadLine()) != null)
                    {
                        var dateProdus = linie.Split('|');
                        if (dateProdus.Length == 6)
                        {
                            //int idProdus = int.Parse(dateProdus[0]);  // Citim ID-ul corect din fișier
                            string nume = dateProdus[1];
                            double pret = double.Parse(dateProdus[2]);
                            int cantitate = int.Parse(dateProdus[3]);
                            TipMaterial material = (TipMaterial)Enum.Parse(typeof(TipMaterial), dateProdus[4]);
                            Utilizare tipUtilizare = (Utilizare)Enum.Parse(typeof(Utilizare), dateProdus[5]);

                            produse.Add(new Produs(nume, pret, cantitate, material, tipUtilizare)); // Adăugăm produsul cu ID-ul corect
                            nrProduse++;
                        }
                    }
                }
                Console.WriteLine("Produsele au fost încărcate cu succes.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A apărut o eroare la citirea fișierului: {ex.Message}");
            }

            return produse;
        }



        public Produs GetProdusById(int id)
        {
            try
            {
                using (StreamReader sr = new StreamReader(numeFisier))
                {
                    string linie;
                    while ((linie = sr.ReadLine()) != null)
                    {
                        var dateProdus = linie.Split('|');
                        if (dateProdus.Length == 6)
                        {
                            int idProdus = int.Parse(dateProdus[0]);
                            if (idProdus == id)
                            {
                                string nume = dateProdus[1];
                                double pret = double.Parse(dateProdus[2]);
                                int cantitate = int.Parse(dateProdus[3]);
                                // Corectarea valorilor Material și TipUtilizare
                                TipMaterial material = (TipMaterial)Enum.Parse(typeof(TipMaterial), dateProdus[4]);
                                Utilizare tipUtilizare = (Utilizare)Enum.Parse(typeof(Utilizare), dateProdus[5]);

                                return new Produs(nume, pret, cantitate, material, tipUtilizare);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A apărut o eroare la citirea fișierului: {ex.Message}");
            }

            return null;
        }

        public void StergeProdus(int id)
        {
            try
            {
                var produse = GetProduse(out int nrProduse);
                var produseRamase = new List<Produs>(produse.FindAll(p => p.IdProdus != id));

                using (StreamWriter sw = new StreamWriter(numeFisier, false))
                {
                    foreach (var produs in produseRamase)
                    {
                        sw.WriteLine($"{produs.IdProdus}|{produs.Nume}|{produs.Pret}|{produs.Cantitate}|{produs.Material}|{produs.TipUtilizare}");
                    }
                }

                Console.WriteLine("Produsul a fost șters cu succes.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A apărut o eroare la ștergerea produsului: {ex.Message}");
            }
        }


        public void ActualizeazaProdus(Produs produsActualizat)
        {
            try
            {
                var produse = GetProduse(out int nrProduse);
                var produsDeActualizat = produse.Find(p => p.IdProdus == produsActualizat.IdProdus);

                if (produsDeActualizat != null)
                {
                    produsDeActualizat.Nume = produsActualizat.Nume;
                    produsDeActualizat.Pret = produsActualizat.Pret;
                    produsDeActualizat.Cantitate = produsActualizat.Cantitate;
                    produsDeActualizat.Material = produsActualizat.Material;
                    produsDeActualizat.TipUtilizare = produsActualizat.TipUtilizare;

                    using (StreamWriter sw = new StreamWriter(numeFisier, false))
                    {
                        foreach (var produs in produse)
                        {
                            sw.WriteLine($"{produs.IdProdus}|{produs.Nume}|{produs.Pret}|{produs.Cantitate}|{produs.Material}|{produs.TipUtilizare}");
                        }
                    }

                    Console.WriteLine("Produsul a fost actualizat cu succes.");
                }
                else
                {
                    Console.WriteLine("Produsul cu ID-ul dat nu a fost găsit.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"A apărut o eroare la actualizarea produsului: {ex.Message}");
            }
        }
    }
}
