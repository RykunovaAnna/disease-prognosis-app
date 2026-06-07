using System;
using System.IO;
using System.Linq;
using ClosedXML.Excel;

namespace disease_prognosis
{
    public static class ExcelReportExporter
    {
        private static readonly string[] Headers =
        {
            "Номер медицинской карты",
            "Дата обследования",
            "Возраст",
            "ИМТ",
            "Возраст менархе",
            "Преждевременные роды",
            "Самоаборты или замершие беременности",
            "Потеря плода во 2 триместре",
            "Потеря плода в 3 триместре",
            "Курит партнёр",
            "Пороки сердца",
            "Артериальная гипертензия",
            "Наследственные НМК",
            "Заболевания ССС",
            "Дисплазия",
            "СПКЯ",
            "Наследственные тромбофилии",
            "Лейден",
            "II фактор (протромбин)",
            "PAI 1",
            "Протеин S",
            "Протеин C",
            "Антитромбин III",
            "АФС",
            "Гипергомоцистеинемия",
            "АГ на фоне беременности",
            "ФПН",
            "Эклампсия",
            "ЗРП",
            "ПОНРП",
            "ПРПО",
            "Антенатальная гибель плода",
            "Кровотечение на фоне беременности",
            "ИМВП во время беременности",
            "Послеродовый эндометрит",
            "Субинволюция матки",
            "Мастит после родов",
            "Вероятность заболевания",
            "Уровень риска",
            "Прогноз"
        };

        public static void ExportPatientAndDataset(
            string cardNumber,
            double[] inputs,
            double result,
            string rootFolder)
        {
            if (inputs == null || inputs.Length < 35)
                throw new ArgumentException("Некорректный массив входных данных.");

            if (string.IsNullOrWhiteSpace(rootFolder))
                throw new ArgumentException("Не указана папка для сохранения.");

            Directory.CreateDirectory(rootFolder);

            string safeCardNumber = MakeSafeFileName(cardNumber);
            string patientFolder = Path.Combine(rootFolder, safeCardNumber);
            Directory.CreateDirectory(patientFolder);

            string patientFilePath = Path.Combine(patientFolder, $"card-{safeCardNumber}.xlsx");
            string datasetFilePath = Path.Combine(rootFolder, "all_patients.xlsx");

            ExportToFile(patientFilePath, cardNumber, inputs, result, withFilter: false);
            ExportToFile(datasetFilePath, cardNumber, inputs, result, withFilter: true);
        }

        private static void ExportToFile(
            string filePath,
            string cardNumber,
            double[] inputs,
            double result,
            bool withFilter)
        {
            bool fileExists = File.Exists(filePath);

            using (var workbook = fileExists ? new XLWorkbook(filePath) : new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.FirstOrDefault() ?? workbook.AddWorksheet("Данные");
                bool isNewFile = worksheet.LastRowUsed() == null;

                if (isNewFile)
                {
                    WriteHeaders(worksheet);

                    if (withFilter)
                    {
                        worksheet.Range(1, 1, 1, Headers.Length).SetAutoFilter();
                        worksheet.SheetView.FreezeRows(1);
                    }
                }

                int newRow = (worksheet.LastRowUsed()?.RowNumber() ?? 0) + 1;

                WriteDataRow(worksheet, newRow, cardNumber, inputs, result);

                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(filePath);
            }
        }

        private static void WriteHeaders(IXLWorksheet worksheet)
        {
            for (int i = 0; i < Headers.Length; i++)
            {
                var cell = worksheet.Cell(1, i + 1);
                cell.Value = Headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                cell.Style.Fill.BackgroundColor = XLColor.LightGray;
            }
        }

        private static void WriteDataRow(
            IXLWorksheet worksheet,
            int row,
            string cardNumber,
            double[] inputs,
            double result)
        {
            double probability = RiskAssessmentHelper.CalculateProbability(result);
            bool isSick = probability >= RiskAssessmentHelper.ProbabilityThreshold;

            int col = 1;

            worksheet.Cell(row, col++).Value = cardNumber;
            worksheet.Cell(row, col++).Value = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

            worksheet.Cell(row, col++).Value = inputs[0];
            worksheet.Cell(row, col++).Value = inputs[1];
            worksheet.Cell(row, col++).Value = inputs[2];

            worksheet.Cell(row, col++).Value = inputs[3] == 1 ? "да" : "нет";
            worksheet.Cell(row, col++).Value = inputs[4];
            worksheet.Cell(row, col++).Value = inputs[5] == 1 ? "да" : "нет";
            worksheet.Cell(row, col++).Value = inputs[6] == 1 ? "да" : "нет";
            worksheet.Cell(row, col++).Value = inputs[7] == 1 ? "да" : "нет";

            worksheet.Cell(row, col++).Value = RiskAssessmentHelper.ToYesNo(inputs[8]);
            worksheet.Cell(row, col++).Value = RiskAssessmentHelper.ToYesNo(inputs[9]);
            worksheet.Cell(row, col++).Value = RiskAssessmentHelper.ToYesNo(inputs[10], true);
            worksheet.Cell(row, col++).Value = RiskAssessmentHelper.ToYesNo(inputs[11], true);
            worksheet.Cell(row, col++).Value = RiskAssessmentHelper.ToYesNo(inputs[12], true);
            worksheet.Cell(row, col++).Value = RiskAssessmentHelper.ToYesNo(inputs[13]);
            worksheet.Cell(row, col++).Value = RiskAssessmentHelper.ToYesNo(inputs[14], true);
            worksheet.Cell(row, col++).Value = RiskAssessmentHelper.ToYesNo(inputs[15], true);
            worksheet.Cell(row, col++).Value = RiskAssessmentHelper.ToYesNo(inputs[16], true);
            worksheet.Cell(row, col++).Value = RiskAssessmentHelper.ToYesNo(inputs[17]);
            worksheet.Cell(row, col++).Value = RiskAssessmentHelper.ToYesNo(inputs[18], true);
            worksheet.Cell(row, col++).Value = RiskAssessmentHelper.ToYesNo(inputs[19]);
            worksheet.Cell(row, col++).Value = RiskAssessmentHelper.ToYesNo(inputs[20], true);
            worksheet.Cell(row, col++).Value = RiskAssessmentHelper.ToYesNo(inputs[21]);
            worksheet.Cell(row, col++).Value = RiskAssessmentHelper.ToYesNo(inputs[22]);

            for (int i = 23; i <= 26; i++)
                worksheet.Cell(row, col++).Value = RiskAssessmentHelper.ToYesNo(inputs[i], true);

            for (int i = 27; i <= 32; i++)
                worksheet.Cell(row, col++).Value = RiskAssessmentHelper.ToYesNo(inputs[i]);

            worksheet.Cell(row, col++).Value = RiskAssessmentHelper.ToYesNo(inputs[33], true);
            worksheet.Cell(row, col++).Value = RiskAssessmentHelper.ToYesNo(inputs[34], true);

            worksheet.Cell(row, col++).Value = Math.Round(probability * 100, 2) + "%";
            worksheet.Cell(row, col++).Value = RiskAssessmentHelper.GetRiskLevel(probability);
            worksheet.Cell(row, col).Value = isSick ? "Больна" : "Здорова";
        }

        private static string MakeSafeFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return "unknown_card";

            foreach (char c in Path.GetInvalidFileNameChars())
                fileName = fileName.Replace(c, '_');

            return fileName.Replace("/", "_").Replace("\\", "_");
        }
    }
}