using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace disease_prognosis
{
    public partial class Form1 : Form
    {
        public Form1(string cardNumber = "", string age = "")
        {
            InitializeComponent();

            maskedCardNumber.Text = cardNumber;
            txtAge.Text = age;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            string cardNumber = maskedCardNumber.Text.Replace("-", "").Trim();

            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                MessageBox.Show("Введите номер медицинской карты!", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                maskedCardNumber.Focus();
                return;
            }

            if (cardNumber.Length < 12)
            {
                MessageBox.Show("Номер карты должен содержать 12 цифр!\nФормат: XXX-XXXXXX-XXX",
                               "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                maskedCardNumber.SelectAll();
                maskedCardNumber.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAge.Text))
            {
                MessageBox.Show("Введите возраст пациентки!", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAge.Focus();
                return;
            }

            if (!int.TryParse(txtAge.Text, out int age) || age < 10 || age > 90)
            {
                MessageBox.Show("Некорректный возраст!",
                               "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAge.SelectAll();
                txtAge.Focus();
                return;
            }

            Form2 form2 = new Form2(maskedCardNumber.Text, age);
            form2.Show();
            this.Hide();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите очистить данные?", "Подтверждение", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                maskedCardNumber.Clear();
                txtAge.Clear();
            }
        }

        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
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

        private void txtCardNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
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
