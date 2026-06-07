using System;
using System.Windows.Forms;

namespace disease_prognosis
{
    public partial class Form3: Form
    {
        private string cardNumber;
        private int age;
        private double height;
        private double weight;
        private double bmi;
        private int menarche;
        private int preterm;
        private int miscarriages;
        private int loss2;
        private int loss3;
        private int partnerSmoke;

        private const double Yes = 102;
        private const double No = 101;

        private const int FeatureCount = 35;

        public Form3(string cardNumber, int age, double height, double weight, double bmi, 
            int menarche, int preterm, int miscarriages, int loss2, int loss3, int partnerSmoke)
        {
            InitializeComponent();
            this.cardNumber = cardNumber;
            this.age = age;
            this.height = height;
            this.weight = weight;
            this.bmi = bmi;
            this.menarche = menarche;
            this.preterm = preterm;
            this.miscarriages = miscarriages;
            this.loss2 = loss2;
            this.loss3 = loss3;
            this.partnerSmoke = partnerSmoke;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(cardNumber, age, height.ToString(), weight.ToString(), bmi.ToString("F2"), 
                menarche.ToString(), preterm.ToString(), miscarriages.ToString(), loss2.ToString(), loss3.ToString(), partnerSmoke.ToString());
            form2.Show();
            this.Hide();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите очистить все данные?", "Подтверждение", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                foreach (TabPage tab in tabControl1.TabPages)
                {
                    foreach (Control control in tab.Controls)
                    {
                        if (control is CheckBox checkBox)
                        {
                            checkBox.Checked = false;
                        }
                    }
                }
            }
        }

        private void btnClearCurrent_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите очистить данные с этой вкладки?", "Подтверждение", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                foreach (Control control in tabControl1.SelectedTab.Controls)
                {
                    if (control is CheckBox checkBox)
                    {
                        checkBox.Checked = false;
                    }
                }
            }
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            double[] inputs = new double[FeatureCount];

            // Непрерывные переменные
            inputs[0] = age;                                        // Возраст
            inputs[1] = bmi;                                        // ИМТ
            inputs[2] = menarche;                                   // Менархе (лет)
            inputs[3] = preterm;                                    // Преждевременные роды
            inputs[4] = miscarriages;                               // Самоаборты/замершие
            inputs[5] = loss2;                                      // Потеря плода во 2 триместре
            inputs[6] = loss3;                                      // Потеря плода в 3 триместре
            inputs[7] = partnerSmoke;                               // Курит партнер

            // Пороки сердца
            inputs[8] = chkHeartDefect.Checked ? Yes : No;

            // Артериальная гипертензия
            inputs[9] = chkHypertension.Checked ? Yes : No;

            // Наследственные НМК
            inputs[10] = chkNmk.Checked ? No : Yes;

            // Заболевания ССС
            inputs[11] = chkCVD.Checked ? No : Yes;

            // Дисплазия
            inputs[12] = chkDysplasia.Checked ? No : Yes;

            // СПКЯ
            inputs[13] = chkPCOS.Checked ? Yes : No;

            // Наследственные Тромбофилии 
            inputs[14] = chkThrombophilia.Checked ? No : Yes;

            // Лейден 
            inputs[15] = chkLeiden.Checked ? No : Yes;

            // II фактор (протромбин)
            inputs[16] = chkFactor2.Checked ? No : Yes;

            // PAI 1
            inputs[17] = chkPAI1.Checked ? Yes : No;

            // Протеин S 
            inputs[18] = chkProteinS.Checked ? No : Yes;

            // Протеин C 
            inputs[19] = chkProteinC.Checked ? Yes : No;

            // Антитромбин III 
            inputs[20] = chkAntithrombin.Checked ? No : Yes;

            // АФС
            inputs[21] = chkAPS.Checked ? Yes : No;

            // Гипергомоцистеинемия
            inputs[22] = chkHyperhomocysteinemia.Checked ? Yes : No;

            // АГ на фоне б-ти
            inputs[23] = chkPregnancyHypertension.Checked ? No : Yes;

            // ФПН
            inputs[24] = chkFPN.Checked ? No : Yes;

            // Эклампсия
            inputs[25] = chkEclampsia.Checked ? No : Yes;

            // ЗРП
            inputs[26] = chkGRP.Checked ? No : Yes;

            // ПОНРП
            inputs[27] = chkPONRP.Checked ? Yes : No;

            // ПРПО
            inputs[28] = chkPRPO.Checked ? Yes : No;

            // Антенатальная гибель плода
            inputs[29] = chkAntenatalDeath.Checked ? Yes : No;

            // Кровотечение на фоне б-ти
            inputs[30] = chkBleeding.Checked ? Yes : No;

            // ИМВП во время б-ти
            inputs[31] = chkUTI.Checked ? Yes : No;

            // Послеродовый эндометрит
            inputs[32] = chkEndometritis.Checked ? Yes : No;

            // Субинволюция матки
            inputs[33] = chkSubinvolution.Checked ? No : Yes;

            // Мастит после родов
            inputs[34] = chkMastitis.Checked ? No : Yes;

            double result = SvmModel.Predict(inputs);
            Form4 form4 = new Form4(cardNumber, result, inputs);
            form4.Show();
            this.Hide();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
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
