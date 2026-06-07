using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace disease_prognosis
{
    public partial class Form5 : Form
    {
        private string[] factorNames;
        private double[] factorValues;

        public Form5(string[] names, double[] values)
        {
            InitializeComponent();
            factorNames = names;
            factorValues = values;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Titles.Clear();
            chart1.Legends.Clear();

            if (factorNames == null || factorValues == null || factorNames.Length == 0 || factorValues.Length == 0)
            {
                MessageBox.Show("Нет данных для построения графика.");
                return;
            }

            chart1.BackColor = Color.White;
            chart1.BorderlineDashStyle = ChartDashStyle.Solid;
            chart1.BorderlineColor = Color.Gainsboro;
            chart1.BorderlineWidth = 1;

            ChartArea area = new ChartArea("MainArea");
            chart1.ChartAreas.Add(area);

            // Ось X 
            area.AxisX.Title = "Факторы";
            area.AxisX.TitleFont = new Font("Segoe UI", 10, FontStyle.Bold);
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 9);
            area.AxisX.MajorGrid.LineColor = Color.Gainsboro;
            area.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            area.AxisX.LineColor = Color.Gray;
            area.AxisX.Minimum = 0;
            area.AxisX.IsReversed = true;

            // Ось Y 
            area.AxisY.Title = "Вклад в риск";
            area.AxisY.TitleFont = new Font("Segoe UI", 10, FontStyle.Bold);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 9);
            area.AxisY.MajorGrid.Enabled = false;
            area.AxisY.LineColor = Color.Transparent;
            area.AxisY.IsReversed = false; // самый значимый сверху
            area.AxisY.LabelStyle.IsEndLabelVisible = false;

            Series series = new Series("Factors");
            series.ChartType = SeriesChartType.Bar;
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "F3";
            series.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            series.Color = Color.FromArgb(76, 132, 255);
            series.BorderWidth = 1;
            series["PointWidth"] = "0.6";
            series.SmartLabelStyle.Enabled = false;

            for (int i = 0; i < factorNames.Length; i++)
            {
                int pointIndex = series.Points.AddXY(WrapLabel(factorNames[i], 20), factorValues[i]);
                series.Points[pointIndex].LabelForeColor = Color.Black;
            }

            chart1.Series.Add(series);
        }

        private string WrapLabel(string text, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";

            if (text.Length <= maxLength)
                return text;

            int spaceIndex = text.LastIndexOf(' ', maxLength);

            if (spaceIndex <= 0)
                spaceIndex = maxLength;

            return text.Substring(0, spaceIndex) + "\n" + text.Substring(spaceIndex).Trim();
        }
    }
}