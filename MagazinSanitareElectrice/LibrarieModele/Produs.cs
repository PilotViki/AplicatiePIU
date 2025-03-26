using System;

namespace LibrarieModele
{
    // Enumerare pentru Material
    public enum TipMaterial
    {
        Ceramica,
        Metal,
        Plastic,
        Alama,
        Sticla
    }


    // Enumerare pentru Utilizare (cu atribut [Flags])
    [Flags]
    public enum Utilizare
    {
        Niciuna = 0,
        Baie = 1,
        Bucatarie = 2,
        Exterior = 3,
        Interior = 4
       
    }

    public class Produs
    {
        public int IdProdus { get; set; }
        public string Nume { get; set; }
        public double Pret { get; set; }
        public int Cantitate { get; set; }
        public TipMaterial Material { get; set; }
        public Utilizare TipUtilizare { get; set; }

        // Variabilă statică pentru generarea ID-urilor
        private static int NextId = 1;

        // Constructor cu parametri corectat
        public Produs(string nume, double pret, int cantitate, TipMaterial material, Utilizare tipUtilizare)
        {
            IdProdus = GetNextId();  // Generăm ID-ul folosind metoda GetNextId
            Nume = nume;
            Pret = pret;
            Cantitate = cantitate;
            Material = material;  // Folosește Material
            TipUtilizare = tipUtilizare;  // Folosește TipUtilizare
        }

        // Metoda pentru a obține următorul ID
        public static int GetNextId()
        {
            return NextId++;  // Returnează ID-ul curent și apoi îl incrementează
        }

        // Suprascrierea metodei ToString pentru a oferi un format specificat de afișare
        public override string ToString()
        {
            return $"{IdProdus}|{Nume}|{Pret}|{Cantitate}|{Material}|{TipUtilizare}";
        }
    }
}
