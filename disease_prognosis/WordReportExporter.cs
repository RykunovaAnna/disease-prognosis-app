using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xceed.Document.NET;
using Xceed.Words.NET;


namespace disease_prognosis
{
    public static class WordReportExporter
    {
        public static string Export(
            string cardNumber,
            double[] inputs,
            double result,
            string[] topFactorNames,
            string outputFolder)
        {
            if (inputs == null || inputs.Length < 35)
                throw new ArgumentException("Некорректный массив входных данных.");

            if (string.IsNullOrWhiteSpace(outputFolder))
                throw new ArgumentException("Не указана папка для сохранения.");

            Directory.CreateDirectory(outputFolder);

            string safeCardNumber = MakeSafeFileName(cardNumber);
            string filePath = Path.Combine(
                outputFolder,
                $"report-{safeCardNumber}-{DateTime.Now:dd-MM-yyyy}.docx");

            double probability = RiskAssessmentHelper.CalculateProbability(result);
            string recommendation = RiskAssessmentHelper.GetRecommendation(probability);

            string conclusion;
            if (probability < 0.3)
                conclusion = "выявлен низкий репродуктивный риск.";
            else if (probability < 0.5)
                conclusion = "выявлен пограничный репродуктивный риск.";
            else if (probability < 0.7)
                conclusion = "выявлен повышенный репродуктивный риск.";
            else
                conclusion = "выявлен высокий репродуктивный риск.";

            using (var doc = DocX.Create(filePath))
            {
                doc.MarginLeft = 55f;
                doc.MarginRight = 55f;
                doc.MarginTop = 45f;
                doc.MarginBottom = 45f;

                doc.SetDefaultFont(new Font("Times New Roman"), 12d);

                AddOrganizationBlock(doc);
                AddMainTitle(doc);
                AddInfoLine(doc, cardNumber);
                AddPatientDataBlock(doc, inputs);
                AddAnamnesisBlock(doc, inputs);
                AddResultBlock(doc, probability, conclusion);
                AddFactorsBlock(doc, topFactorNames);
                AddRecommendationsBlock(doc, recommendation);
                AddFooterNote(doc);

                doc.Save();
            }

            return filePath;
        }


        private static void AddOrganizationBlock(DocX doc)
        {
            var p1 = doc.InsertParagraph();
            p1.Alignment = Alignment.left;
            p1.SpacingAfter(0);
            p1.Append("Название организации")
              .Bold()
              .FontSize(14);

            var p2 = doc.InsertParagraph();
            p2.Alignment = Alignment.left;
            p2.SpacingAfter(0);
            p2.Append("Адрес")
              .FontSize(12);

            doc.InsertParagraph().SpacingAfter(10);
        }

        private static void AddMainTitle(DocX doc)
        {
            var title = doc.InsertParagraph();
            title.Alignment = Alignment.center;
            title.SpacingBefore(0);
            title.SpacingAfter(8);
            title.Append("ПРОТОКОЛ ОЦЕНКИ РЕПРОДУКТИВНОГО РИСКА")
                 .Bold()
                 .FontSize(14);
        }

        private static void AddInfoLine(DocX doc, string cardNumber)
        {
            Table infoTable = doc.AddTable(1, 2);
            infoTable.Design = TableDesign.None;
            infoTable.AutoFit = AutoFit.Window;
            infoTable.Alignment = Alignment.left;

            var left = infoTable.Rows[0].Cells[0].Paragraphs[0];
            left.Alignment = Alignment.left;
            left.Append("Номер медицинской карты: ").Bold();
            left.Append(cardNumber);

            var right = infoTable.Rows[0].Cells[1].Paragraphs[0];
            right.Alignment = Alignment.right;
            right.Append("Дата формирования: ").Bold();
            right.Append(DateTime.Now.ToString("dd.MM.yyyy"));

            infoTable.Rows[0].Cells[0].Width = 290;
            infoTable.Rows[0].Cells[1].Width = 190;

            foreach (var cell in infoTable.Rows[0].Cells)
            {
                cell.MarginTop = 0;
                cell.MarginBottom = 0;
                cell.MarginLeft = 0;
                cell.MarginRight = 0;

                foreach (var p in cell.Paragraphs)
                {
                    p.SpacingBefore(0);
                    p.SpacingAfter(0);
                }
            }

            doc.InsertTable(infoTable);
            doc.InsertParagraph().SpacingAfter(8);
        }

        private static void AddPatientDataBlock(DocX doc, double[] inputs)
        {
            AddSectionHeader(doc, "Данные пациентки");

            Table table = doc.AddTable(3, 2);
            table.Design = TableDesign.TableGrid;
            table.AutoFit = AutoFit.Window;
            table.Alignment = Alignment.left;

            FillDataRow(table, 0, "Возраст", $"{(int)inputs[0]} лет");
            FillDataRow(table, 1, "Индекс массы тела (ИМТ)", $"{inputs[1]:0.00}".Replace(".", ","));
            FillDataRow(table, 2, "Менархе", $"{(int)inputs[2]} лет");

            table.Rows[0].Cells[0].Width = 230;
            table.Rows[0].Cells[1].Width = 250;
            table.Rows[1].Cells[0].Width = 230;
            table.Rows[1].Cells[1].Width = 250;
            table.Rows[2].Cells[0].Width = 230;
            table.Rows[2].Cells[1].Width = 250;

            foreach (var row in table.Rows)
            {
                foreach (var cell in row.Cells)
                {
                    cell.MarginTop = 1;
                    cell.MarginBottom = 1;
                    cell.MarginLeft = 3;
                    cell.MarginRight = 3;

                    foreach (var p in cell.Paragraphs)
                    {
                        p.SpacingBefore(0);
                        p.SpacingAfter(0);
                        p.FontSize(12);
                    }
                }
            }

            doc.InsertTable(table);
            doc.InsertParagraph().SpacingAfter(8);
        }

        private static void AddAnamnesisBlock(DocX doc, double[] inputs)
        {
            AddSectionHeader(doc, "Анамнез");

            List<string> items = GetAnamnesisItems(inputs);

            if (items.Count == 0)
            {
                var p = doc.InsertParagraph();
                p.SpacingBefore(0);
                p.SpacingAfter(6);
                p.Append("Клинически значимые анамнестические факторы не отмечены.");
            }
            else
            {
                foreach (string item in items)
                {
                    var p = doc.InsertParagraph();
                    p.SpacingBefore(0);
                    p.SpacingAfter(0);
                    p.Append(item);
                }

                doc.InsertParagraph().SpacingAfter(6);
            }
        }

        private static List<string> GetAnamnesisItems(double[] inputs)
        {
            var items = new List<string>();

            int miscarriageCount = (int)inputs[4];
            if (miscarriageCount > 0)
                items.Add($"Самоаборты/замершие беременности: {miscarriageCount}");

            AddIfTrue(items, "Преждевременные роды", inputs[3] == 1);
            AddIfTrue(items, "Потеря плода во 2 триместре", inputs[5] == 1);
            AddIfTrue(items, "Потеря плода в 3 триместре", inputs[6] == 1);
            AddIfTrue(items, "Наличие никотиновой зависимости у партнёра", inputs[7] == 1);

            AddIfTrue(items, "Пороки сердца", RiskAssessmentHelper.ToYesNo(inputs[8]) == "да");
            AddIfTrue(items, "Артериальная гипертензия", RiskAssessmentHelper.ToYesNo(inputs[9]) == "да");
            AddIfTrue(items, "Наследственные НМК", RiskAssessmentHelper.ToYesNo(inputs[10], true) == "да");
            AddIfTrue(items, "Заболевания ССС", RiskAssessmentHelper.ToYesNo(inputs[11], true) == "да");
            AddIfTrue(items, "Дисплазия", RiskAssessmentHelper.ToYesNo(inputs[12], true) == "да");
            AddIfTrue(items, "СПКЯ", RiskAssessmentHelper.ToYesNo(inputs[13]) == "да");
            AddIfTrue(items, "Наследственные тромбофилии", RiskAssessmentHelper.ToYesNo(inputs[14], true) == "да");
            AddIfTrue(items, "Лейден", RiskAssessmentHelper.ToYesNo(inputs[15], true) == "да");
            AddIfTrue(items, "II фактор (протромбин)", RiskAssessmentHelper.ToYesNo(inputs[16], true) == "да");
            AddIfTrue(items, "PAI 1", RiskAssessmentHelper.ToYesNo(inputs[17]) == "да");
            AddIfTrue(items, "Протеин S", RiskAssessmentHelper.ToYesNo(inputs[18], true) == "да");
            AddIfTrue(items, "Протеин C", RiskAssessmentHelper.ToYesNo(inputs[19]) == "да");
            AddIfTrue(items, "Антитромбин III", RiskAssessmentHelper.ToYesNo(inputs[20], true) == "да");
            AddIfTrue(items, "АФС", RiskAssessmentHelper.ToYesNo(inputs[21]) == "да");
            AddIfTrue(items, "Гипергомоцистеинемия", RiskAssessmentHelper.ToYesNo(inputs[22]) == "да");
            AddIfTrue(items, "АГ на фоне беременности", RiskAssessmentHelper.ToYesNo(inputs[23], true) == "да");
            AddIfTrue(items, "ФПН", RiskAssessmentHelper.ToYesNo(inputs[24], true) == "да");
            AddIfTrue(items, "Эклампсия", RiskAssessmentHelper.ToYesNo(inputs[25], true) == "да");
            AddIfTrue(items, "ЗРП", RiskAssessmentHelper.ToYesNo(inputs[26], true) == "да");
            AddIfTrue(items, "ПОНРП", RiskAssessmentHelper.ToYesNo(inputs[27]) == "да");
            AddIfTrue(items, "ПРПО", RiskAssessmentHelper.ToYesNo(inputs[28]) == "да");
            AddIfTrue(items, "Антенатальная гибель плода", RiskAssessmentHelper.ToYesNo(inputs[29]) == "да");
            AddIfTrue(items, "Кровотечение на фоне беременности", RiskAssessmentHelper.ToYesNo(inputs[30]) == "да");
            AddIfTrue(items, "ИМВП во время беременности", RiskAssessmentHelper.ToYesNo(inputs[31]) == "да");
            AddIfTrue(items, "Послеродовый эндометрит", RiskAssessmentHelper.ToYesNo(inputs[32]) == "да");
            AddIfTrue(items, "Субинволюция матки", RiskAssessmentHelper.ToYesNo(inputs[33], true) == "да");
            AddIfTrue(items, "Мастит после родов", RiskAssessmentHelper.ToYesNo(inputs[34], true) == "да");

            return items;
        }

        private static void AddResultBlock(DocX doc, double probability, string conclusion)
        {
            AddSectionHeader(doc, "Результат прогностической оценки");

            var p1 = doc.InsertParagraph();
            p1.SpacingAfter(0);
            p1.Append("Вероятность патологии: ").Bold();
            p1.Append($"{probability * 100:0.00} %".Replace(".", ","));

            var p2 = doc.InsertParagraph();
            p2.SpacingAfter(6);
            p2.Append("Заключение: ").Bold();
            p2.Append(conclusion);

            doc.InsertParagraph().SpacingAfter(6);
        }

        private static void AddFactorsBlock(DocX doc, string[] topFactorNames)
        {
            AddSectionHeader(doc, "Факторы риска, учтённые моделью");

            var factors = topFactorNames?
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .Distinct()
                .ToList() ?? new List<string>();

            if (factors.Count == 0)
            {
                var p = doc.InsertParagraph();
                p.SpacingAfter(6);
                p.Append("Значимые факторы риска не выделены.");
            }
            else
            {
                foreach (string factor in factors)
                {
                    var p = doc.InsertParagraph();
                    p.SpacingBefore(0);
                    p.SpacingAfter(0);
                    p.Append("- " + factor);
                }

                doc.InsertParagraph().SpacingAfter(6);
            }
        }

        private static void AddRecommendationsBlock(DocX doc, string recommendation)
        {
            AddSectionHeader(doc, "Рекомендации");

            var p1 = doc.InsertParagraph();
            p1.SpacingAfter(0);
            p1.Append(recommendation);

            var p2 = doc.InsertParagraph();
            p2.SpacingAfter(0);
            p2.Append("Интерпретация результата должна проводиться врачом с учётом клинико-анамнестических данных и результатов дополнительных методов обследования.");

            var p3 = doc.InsertParagraph();
            p3.SpacingAfter(6);
            p3.Append("При необходимости рекомендуется консультация профильного специалиста.");
        }

        private static void AddFooterNote(DocX doc)
        {
            var p = doc.InsertParagraph();
            p.Alignment = Alignment.center;
            p.SpacingBefore(6);
            p.SpacingAfter(0);

            p.AppendLine("Отчёт сформирован автоматически программным приложением.")
             .Italic()
             .FontSize(11);

            p.AppendLine();

            p.Append("Результаты носят консультативный характер и не являются медицинским диагнозом.")
             .Italic()
             .FontSize(11);
        }

        private static void AddSectionHeader(DocX doc, string text)
        {
            var header = doc.InsertParagraph();
            header.SpacingBefore(6);
            header.SpacingAfter(4);
            header.Append(text)
                  .Bold()
                  .FontSize(14);
        }

        private static void FillDataRow(Table table, int rowIndex, string left, string right)
        {
            table.Rows[rowIndex].Cells[0].Paragraphs[0].Append(left).Bold();
            table.Rows[rowIndex].Cells[1].Paragraphs[0].Append(right);
        }

        private static void AddIfTrue(List<string> items, string text, bool condition)
        {
            if (condition)
                items.Add(text);
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