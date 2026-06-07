using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace disease_prognosis
{
    public partial class Form4 : Form
    {
        private string cardNumber;
        private double result;
        private double[] inputs;        

        private bool isClosingForNewPatient = false;

        public Form4(string cardNumber, double result, double[] inputs)
        {
            InitializeComponent();
            this.cardNumber = cardNumber;
            this.result = result;
            this.inputs = inputs;
        }
        
        private void btnNewPatient_Click(object sender, EventArgs e)
        {
            isClosingForNewPatient = true;
            Form1 form = new Form1();
            form.Show();
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            double probability = RiskAssessmentHelper.CalculateProbability(result);
            bool isSick = probability >= RiskAssessmentHelper.ProbabilityThreshold;

            // Основной диагноз
            lblResultInfo.Text = isSick ? "Больна" : "Здорова";

            // Процент вероятности
            lblProbabilityInfo.Text = Math.Round(probability * 100, 2) + " %";

            // Уровень риска
            lblRiskLevelInfo.Text = RiskAssessmentHelper.GetRiskLevel(probability);

            // Рекомендации
            lblRecommendationInfo.Text = RiskAssessmentHelper.GetRecommendation(probability);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (inputs == null || inputs.Length < 35)
            {
                MessageBox.Show("Ошибка: некорректные входные данные.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                string rootFolder = Path.Combine(Application.StartupPath, "medical_cards");

                string safeCardNumber = string.IsNullOrWhiteSpace(cardNumber)
                    ? "unknown_card"
                    : cardNumber.Replace("/", "_").Replace("\\", "_");

                string patientFolder = Path.Combine(rootFolder, safeCardNumber);

                Directory.CreateDirectory(rootFolder);
                Directory.CreateDirectory(patientFolder);

                ExcelReportExporter.ExportPatientAndDataset(
                    cardNumber,
                    inputs,
                    result,
                    rootFolder);

                string[] topNames;
                double[] topValues;
                SvmModel.GetTopRiskFactors(
                    inputs,
                    SvmModel.TopRiskFactorCount,
                    out topNames,
                    out topValues);

                string savedWordPath = WordReportExporter.Export(
                    cardNumber,
                    inputs,
                    result,
                    topNames,
                    patientFolder
                );

                Process.Start(new ProcessStartInfo
                {
                    FileName = savedWordPath,
                    UseShellExecute = true
                });

                MessageBox.Show(
                    "Данные успешно сохранены!",
                    "Успех",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения файла: " + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isClosingForNewPatient)
                return;

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

        private void btnTopFactors_Click(object sender, EventArgs e)
        {
            string[] names;
            double[] values;

            SvmModel.GetTopRiskFactors(inputs, SvmModel.TopRiskFactorCount, out names, out values);

            if (names.Length == 0)
            {
                MessageBox.Show(
                    "Не выявлено признаков с положительным вкладом в повышение риска.",
                    "Топ факторов",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            Form5 form5 = new Form5(names, values);
            form5.ShowDialog();
        }
    }
}