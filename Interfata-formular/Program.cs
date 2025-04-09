using System;
using System.Drawing;
using System.Windows.Forms;
using LibrarieModele;
using NivelStocareDate;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace EvidentaProduse
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormularProduse());
        }
    }

    public class FormularProduse : Form
    {
        private AdministrareProduse_FisierText adminProduse;
        private string pathFisier;

        // Controale
        private Label lblNume;
        private TextBox txtNume;
        private Label lblPret;
        private TextBox txtPret;
        private Label lblCantitate;
        private TextBox txtCantitate;
        private Label lblMaterial;
        private ComboBox cmbMaterial;
        private Label lblUtilizare;
        private ComboBox cmbUtilizare;
        private Button btnAdauga;
        private Button btnAfiseaza;
        private Button btnSterge;
        private Button btnCauta;
        private TextBox txtCautare;
        private ListBox lstProduse;
        private Label lblNumarProduse;

        // Constante pentru dimensiuni
        private const int LATIME_CONTROL = 150;
        private const int INALTIME_CONTROL = 20;
        private const int DIMENSIUNE_PAS_Y = 30;
        private const int DIMENSIUNE_PAS_X = 170;
        private const int MARGINE = 20;

        // Culori personalizate
        private readonly Color culoareFundal = Color.White;
        private readonly Color culoarePrimara = Color.SteelBlue;
        private readonly Color culoareText = Color.DimGray;
        private readonly Color culoareHighlight = Color.LightSteelBlue;

        public FormularProduse()
        {
            // Initializare fisier
            pathFisier = Path.Combine("NivelStocareDate", "produse.txt");
            string director = Path.GetDirectoryName(pathFisier);
            if (!Directory.Exists(director))
                Directory.CreateDirectory(director);

            adminProduse = new AdministrareProduse_FisierText(pathFisier);

            // Configurare formular
            this.Size = new Size(750, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 9);
            this.ForeColor = culoareText;
            this.Text = "Gestionare Produse";

            // Adaugare controale
            AdaugaControale();
        }

        private void AdaugaControale()
        {
            // Functii de stilizare
            void StilizeazaLabel(Label label)
            {
                label.ForeColor = culoarePrimara;
                label.BackColor = Color.Transparent;
                label.Font = new Font(this.Font, FontStyle.Bold);
            }

            void StilizeazaTextBox(TextBox textBox)
            {
                textBox.BackColor = culoareFundal;
                textBox.ForeColor = culoareText;
                textBox.BorderStyle = BorderStyle.FixedSingle;
                textBox.Margin = new Padding(3, 3, 3, 10);
            }

            void StilizeazaComboBox(ComboBox combo)
            {
                combo.BackColor = culoareFundal;
                combo.ForeColor = culoareText;
                combo.FlatStyle = FlatStyle.Flat;
            }

            void StilizeazaButon(Button buton)
            {
                buton.BackColor = culoarePrimara;
                buton.ForeColor = Color.White;
                buton.FlatStyle = FlatStyle.Flat;
                buton.FlatAppearance.BorderSize = 0;
                buton.Font = new Font(this.Font, FontStyle.Bold);
                buton.Cursor = Cursors.Hand;
                buton.Size = new Size(LATIME_CONTROL, 30);
                buton.MouseEnter += (s, e) => buton.BackColor = Color.LightSlateGray;
                buton.MouseLeave += (s, e) => buton.BackColor = culoarePrimara;
            }

            // Nume
            lblNume = new Label();
            lblNume.Text = "Nume:";
            lblNume.Location = new Point(MARGINE, MARGINE);
            lblNume.Width = LATIME_CONTROL;
            StilizeazaLabel(lblNume);
            this.Controls.Add(lblNume);

            txtNume = new TextBox();
            txtNume.Location = new Point(DIMENSIUNE_PAS_X, MARGINE);
            txtNume.Width = LATIME_CONTROL;
            StilizeazaTextBox(txtNume);
            this.Controls.Add(txtNume);

            // Pret
            lblPret = new Label();
            lblPret.Text = "Preț:";
            lblPret.Location = new Point(MARGINE, MARGINE + DIMENSIUNE_PAS_Y);
            lblPret.Width = LATIME_CONTROL;
            StilizeazaLabel(lblPret);
            this.Controls.Add(lblPret);

            txtPret = new TextBox();
            txtPret.Location = new Point(DIMENSIUNE_PAS_X, MARGINE + DIMENSIUNE_PAS_Y);
            txtPret.Width = LATIME_CONTROL;
            StilizeazaTextBox(txtPret);
            this.Controls.Add(txtPret);

            // Cantitate
            lblCantitate = new Label();
            lblCantitate.Text = "Cantitate:";
            lblCantitate.Location = new Point(MARGINE, MARGINE + 2 * DIMENSIUNE_PAS_Y);
            lblCantitate.Width = LATIME_CONTROL;
            StilizeazaLabel(lblCantitate);
            this.Controls.Add(lblCantitate);

            txtCantitate = new TextBox();
            txtCantitate.Location = new Point(DIMENSIUNE_PAS_X, MARGINE + 2 * DIMENSIUNE_PAS_Y);
            txtCantitate.Width = LATIME_CONTROL;
            StilizeazaTextBox(txtCantitate);
            this.Controls.Add(txtCantitate);

            // Material
            lblMaterial = new Label();
            lblMaterial.Text = "Material:";
            lblMaterial.Location = new Point(MARGINE, MARGINE + 3 * DIMENSIUNE_PAS_Y);
            lblMaterial.Width = LATIME_CONTROL;
            StilizeazaLabel(lblMaterial);
            this.Controls.Add(lblMaterial);

            cmbMaterial = new ComboBox();
            cmbMaterial.Location = new Point(DIMENSIUNE_PAS_X, MARGINE + 3 * DIMENSIUNE_PAS_Y);
            cmbMaterial.Width = LATIME_CONTROL;
            cmbMaterial.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaterial.DataSource = Enum.GetValues(typeof(TipMaterial));
            StilizeazaComboBox(cmbMaterial);
            this.Controls.Add(cmbMaterial);

            // Utilizare
            lblUtilizare = new Label();
            lblUtilizare.Text = "Utilizare:";
            lblUtilizare.Location = new Point(MARGINE, MARGINE + 4 * DIMENSIUNE_PAS_Y);
            lblUtilizare.Width = LATIME_CONTROL;
            StilizeazaLabel(lblUtilizare);
            this.Controls.Add(lblUtilizare);

            cmbUtilizare = new ComboBox();
            cmbUtilizare.Location = new Point(DIMENSIUNE_PAS_X, MARGINE + 4 * DIMENSIUNE_PAS_Y);
            cmbUtilizare.Width = LATIME_CONTROL;
            cmbUtilizare.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUtilizare.DataSource = Enum.GetValues(typeof(Utilizare));
            StilizeazaComboBox(cmbUtilizare);
            this.Controls.Add(cmbUtilizare);

            // Butoane
            btnAdauga = new Button();
            btnAdauga.Text = "Adaugă Produs";
            btnAdauga.Location = new Point(MARGINE, MARGINE + 5 * DIMENSIUNE_PAS_Y);
            StilizeazaButon(btnAdauga);
            btnAdauga.Click += btnAdauga_Click;
            this.Controls.Add(btnAdauga);

            btnAfiseaza = new Button();
            btnAfiseaza.Text = "Afișează Produse";
            btnAfiseaza.Location = new Point(DIMENSIUNE_PAS_X, MARGINE + 5 * DIMENSIUNE_PAS_Y);
            StilizeazaButon(btnAfiseaza);
            btnAfiseaza.Click += btnAfiseaza_Click;
            this.Controls.Add(btnAfiseaza);

            // Cautare
            txtCautare = new TextBox();
            txtCautare.Location = new Point(MARGINE, MARGINE + 6 * DIMENSIUNE_PAS_Y);
            txtCautare.Width = LATIME_CONTROL;
            txtCautare.Text = "Caută produs...";
            txtCautare.GotFocus += (s, e) => { if (txtCautare.Text == "Caută produs...") txtCautare.Text = ""; };
            txtCautare.LostFocus += (s, e) => { if (string.IsNullOrWhiteSpace(txtCautare.Text)) txtCautare.Text = "Caută produs..."; };
            StilizeazaTextBox(txtCautare);
            this.Controls.Add(txtCautare);

            btnCauta = new Button();
            btnCauta.Text = "Caută";
            btnCauta.Location = new Point(DIMENSIUNE_PAS_X, MARGINE + 6 * DIMENSIUNE_PAS_Y);
            StilizeazaButon(btnCauta);
            btnCauta.Click += btnCauta_Click;
            this.Controls.Add(btnCauta);

            // Sterge
            btnSterge = new Button();
            btnSterge.Text = "Șterge";
            btnSterge.Location = new Point(MARGINE, MARGINE + 7 * DIMENSIUNE_PAS_Y);
            StilizeazaButon(btnSterge);
            btnSterge.Click += btnSterge_Click;
            this.Controls.Add(btnSterge);

            // Lista produse
            lstProduse = new ListBox();
            lstProduse.Location = new Point(2 * DIMENSIUNE_PAS_X + MARGINE, MARGINE);
            lstProduse.Size = new Size(300, 300);
            lstProduse.BackColor = culoareFundal;
            lstProduse.ForeColor = culoareText;
            lstProduse.BorderStyle = BorderStyle.FixedSingle;
            lstProduse.Font = new Font("Consolas", 9);
            lstProduse.SelectedIndexChanged += (s, e) =>
            {
                if (lstProduse.SelectedIndex != -1)
                    lstProduse.BackColor = culoareHighlight;
                else
                    lstProduse.BackColor = culoareFundal;
            };
            this.Controls.Add(lstProduse);

            // Numar produse
            lblNumarProduse = new Label();
            lblNumarProduse.Location = new Point(2 * DIMENSIUNE_PAS_X + MARGINE, MARGINE + 310);
            lblNumarProduse.Width = 300;
            lblNumarProduse.ForeColor = culoarePrimara;
            lblNumarProduse.Font = new Font(this.Font, FontStyle.Italic);
            this.Controls.Add(lblNumarProduse);
        }

        private void btnAdauga_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNume.Text))
            {
                MessageBox.Show("Introduceți un nume valid pentru produs.", "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtPret.Text, out double pret) || pret <= 0)
            {
                MessageBox.Show("Introduceți un preț valid.", "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCantitate.Text, out int cantitate) || cantitate <= 0)
            {
                MessageBox.Show("Introduceți o cantitate validă.", "Eroare",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Produs produsNou = new Produs(
                txtNume.Text,
                pret,
                cantitate,
                (TipMaterial)cmbMaterial.SelectedItem,
                (Utilizare)cmbUtilizare.SelectedItem
            );

            adminProduse.AddProdus(produsNou);
            MessageBox.Show("Produs adăugat cu succes!", "Succes",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            AfiseazaProduse();
            ResetareControale();
        }

        private void btnAfiseaza_Click(object sender, EventArgs e)
        {
            AfiseazaProduse();
        }

        private void AfiseazaProduse()
        {
            List<Produs> produse = adminProduse.GetProduse(out int nrProduse);
            lstProduse.Items.Clear();

            foreach (Produs produs in produse)
            {
                lstProduse.Items.Add(produs.ToString());
            }

            lblNumarProduse.Text = $"Număr produse: {nrProduse}";
        }

        private void btnSterge_Click(object sender, EventArgs e)
        {
            if (lstProduse.SelectedIndex == -1)
            {
                MessageBox.Show("Selectați un produs pentru ștergere.", "Atenție",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedItem = lstProduse.SelectedItem.ToString();
            int id = int.Parse(selectedItem.Split(',')[0].Split(':')[1].Trim());

            adminProduse.StergeProdus(id);
            MessageBox.Show("Produs șters cu succes!", "Succes",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            AfiseazaProduse();
        }

        private void btnCauta_Click(object sender, EventArgs e)
        {
            string criteriu = txtCautare.Text.Trim();
            if (string.IsNullOrWhiteSpace(criteriu))
            {
                MessageBox.Show("Introduceți un criteriu de căutare.", "Atenție",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<Produs> produse = adminProduse.GetProduse(out int nrProduse);
            lstProduse.Items.Clear();

            foreach (Produs produs in produse)
            {
                if (produs.ToString().IndexOf(criteriu, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    lstProduse.Items.Add(produs.ToString());
                }
            }
        }

        private void ResetareControale()
        {
            txtNume.Text = "";
            txtPret.Text = "";
            txtCantitate.Text = "";
            txtCautare.Text = "";
            cmbMaterial.SelectedIndex = 0;
            cmbUtilizare.SelectedIndex = 0;
        }
    }
}