using System;
using System.Windows.Forms;

namespace disease_prognosis
{
    public partial class Form2 : Form
    {
        private string cardNumber;

        private int age;
        public string SavedHeight { get; set; }
        public string SavedWeight { get; set; }
        public string SavedBMI { get; set; }
        public string SavedMenarche { get; set; }
        public string SavedMiscarriages { get; set; }
        public string SavedPreterm { get; set; }
        public string SavedLoss2 { get; set; }
        public string SavedLoss3 { get; set; }
        public string SavedPartnerSmoke { get; set; }

        public Form2(string cardNumber, int age, string height = "", 
            string weight = "", string bmi = "", string menarche = "", 
            string preterm = "", string miscarriages = "", 
            string loss2 = "", string loss3 = "", string partnerSmoke = "")
        {
            InitializeComponent();
            this.cardNumber = cardNumber;
            this.age = age;

            SavedHeight = height;
            SavedWeight = weight;
            SavedBMI = bmi;
            SavedMenarche = menarche;
            SavedMiscarriages = miscarriages;
            SavedPreterm = preterm;
            SavedLoss2 = loss2;
            SavedLoss3 = loss3;
            SavedPartnerSmoke = partnerSmoke;

            if (!string.IsNullOrEmpty(height)) txtHeight.Text = height;
            if (!string.IsNullOrEmpty(weight)) txtWeight.Text = weight;
            if (!string.IsNullOrEmpty(bmi)) txtBMI.Text = bmi;
            if (!string.IsNullOrEmpty(menarche)) txtMenarche.Text = menarche;
            if (!string.IsNullOrEmpty(miscarriages)) txtMiscarriages.Text = miscarriages;

            if (preterm == "1") rbPretermYes.Checked = true;
            else if (preterm == "0") rbPretermNo.Checked = true;

            if (loss2 == "1") rbLoss2Yes.Checked = true;
            else if (loss2 == "0") rbLoss2No.Checked = true;

            if (loss3 == "1") rbLoss3Yes.Checked = true;
            else if (loss3 == "0") rbLoss3No.Checked = true;

            if (partnerSmoke == "1") rbPartnerSmokeYes.Checked = true;
            else if (partnerSmoke == "0") rbPartnerSmokeNo.Checked = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHeight.Text) || string.IsNullOrWhiteSpace(txtWeight.Text) ||
                string.IsNullOrWhiteSpace(txtMenarche.Text) || string.IsNullOrWhiteSpace(txtMiscarriages.Text))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!rbPretermYes.Checked && !rbPretermNo.Checked)
            {
                MessageBox.Show("Укажите, были ли преждевременные роды!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!rbLoss2Yes.Checked && !rbLoss2No.Checked)
            {
                MessageBox.Show("Укажите, были ли потери беременности во 2 триместре!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!rbLoss3Yes.Checked && !rbLoss3No.Checked)
            {
                MessageBox.Show("Укажите, были ли потери беременности в 3 триместре!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!rbPartnerSmokeYes.Checked && !rbPartnerSmokeNo.Checked)
            {
                MessageBox.Show("Укажите, курит ли партнёр!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtHeight.Text, out double height) || height <= 140 || height >= 200)
            {
                MessageBox.Show("Введите корректный рост!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(txtWeight.Text, out double weight) || weight <= 30 || weight >= 200)
            {
                MessageBox.Show("Введите корректный вес!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtMenarche.Text, out int menarche) || menarche < 7 || menarche > 21)
            {
                MessageBox.Show("Введите корректный возраст менархе!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtMiscarriages.Text, out int miscarriages) || miscarriages > 10)
            {
                MessageBox.Show("Введите корректное количество самоабортов/замерших беременностей!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int preterm = rbPretermYes.Checked ? 1 : 0;
            int loss2 = rbLoss2Yes.Checked ? 1 : 0;
            int loss3 = rbLoss3Yes.Checked ? 1 : 0;
            int partnerSmoke = rbPartnerSmokeYes.Checked ? 1 : 0;

            double bmi = weight / (height * height / 10000);

            SavedHeight = txtHeight.Text;
            SavedWeight = txtWeight.Text;
            SavedBMI = txtBMI.Text;
            SavedMenarche = txtMenarche.Text;
            SavedMiscarriages = txtMiscarriages.Text;
            SavedPreterm = rbPretermYes.Checked ? "1" : "0";
            SavedLoss2 = rbLoss2Yes.Checked ? "1" : "0";
            SavedLoss3 = rbLoss3Yes.Checked ? "1" : "0";
            SavedPartnerSmoke = rbPartnerSmokeYes.Checked ? "1" : "0";

            Form3 form3 = new Form3(cardNumber, age, height, weight, bmi, menarche, preterm, miscarriages, loss2, loss3, partnerSmoke);
            form3.Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(cardNumber, age.ToString());
            form1.Show();
            this.Hide();
        }

        private void Form2_Activated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SavedHeight)) txtHeight.Text = SavedHeight;
            if (!string.IsNullOrEmpty(SavedWeight)) txtWeight.Text = SavedWeight;
            if (!string.IsNullOrEmpty(SavedBMI)) txtBMI.Text = SavedBMI;
            if (!string.IsNullOrEmpty(SavedMenarche)) txtMenarche.Text = SavedMenarche;
            if (!string.IsNullOrEmpty(SavedMiscarriages)) txtMiscarriages.Text = SavedMiscarriages;

            if (SavedPreterm == "1") rbPretermYes.Checked = true;
            else if (SavedPreterm == "0") rbPretermNo.Checked = true;

            if (SavedLoss2 == "1") rbLoss2Yes.Checked = true;
            else if (SavedLoss2 == "0") rbLoss2No.Checked = true;

            if (SavedLoss3 == "1") rbLoss3Yes.Checked = true;
            else if (SavedLoss3 == "0") rbLoss3No.Checked = true;

            if (SavedPartnerSmoke == "1") rbPartnerSmokeYes.Checked = true;
            else if (SavedPartnerSmoke == "0") rbPartnerSmokeNo.Checked = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите очистить данные?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                txtHeight.Clear();
                txtWeight.Clear();
                txtMenarche.Clear();
                txtMiscarriages.Clear();

                rbPretermYes.Checked = false;
                rbPretermNo.Checked = false;
                rbLoss2Yes.Checked = false;
                rbLoss2No.Checked = false;
                rbLoss3Yes.Checked = false;
                rbLoss3No.Checked = false;
                rbPartnerSmokeYes.Checked = false; 
                rbPartnerSmokeNo.Checked = false;

                // Очищаем сохранённые значения,
                // чтобы Form2_Activated не восстановил старые данные
                SavedHeight = "";
                SavedWeight = "";
                SavedBMI = "";
                SavedMenarche = "";
                SavedMiscarriages = "";
                SavedPreterm = "";
                SavedLoss2 = "";
                SavedLoss3 = "";
                SavedPartnerSmoke = "";
            }
        }

        private void txtNumeric_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true;
            }

            if (e.Shift && e.KeyCode == Keys.Insert)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                e.Handled = true;
            }
        }

        private void CalculateBMI()
        {
            if (double.TryParse(txtHeight.Text, out double height) && double.TryParse(txtWeight.Text, out double weight))
            {
                if (height > 0)
                {
                    height /= 100;
                    double bmi = weight / (height * height);
                    txtBMI.Text = bmi.ToString("F2");
                }
                else
                {
                    txtBMI.Text = "";
                }
            }
            else
            {
                txtBMI.Text = "";
            }
        }

        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            CalculateBMI();
        }

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            CalculateBMI();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show(
                    "Вы действительно хотите завершить работу программы?",
                    "Подтверждение выхода",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
