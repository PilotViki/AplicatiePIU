using System;
using System.Drawing;
using System.Windows.Forms;
using LibrarieModele;
using NivelStocareDate;
using System.IO;
using System.Collections.Generic;
using System.Linq;
// Ensure the namespace 'InterfataFormular' is correctly referenced
// Ensure the namespace 'InterfataFormular' is correctly referenced
// If the namespace is in another project, add a reference to that project in your solution.s   
using Interfata_formular;

namespace EvidentaProduse
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form form1 = new Form1();
            Application.Run(form1);
        }
    }
}
