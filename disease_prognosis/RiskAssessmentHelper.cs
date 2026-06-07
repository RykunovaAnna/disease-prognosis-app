using System;

namespace disease_prognosis
{
    public static class RiskAssessmentHelper
    {
        public const double ProbabilityThreshold = 0.5;
        public const double Yes = 102;
        public const double No = 101;

        public static double CalculateProbability(double score)
        {
            return 1.0 / (1.0 + Math.Exp(-score));
        }

        public static string GetRiskLevel(double probability)
        {
            if (probability < 0.3)
                return "Низкий";

            if (probability < 0.5)
                return "Пограничное состояние";

            if (probability < 0.7)
                return "Повышенный";

            return "Высокий";
        }

        public static string GetRecommendation(double probability)
        {
            if (probability < 0.3)
                return "Патологических признаков не выявлено. Показано стандартное профилактическое наблюдение.";

            if (probability < 0.5)
                return "Показано динамическое наблюдение и повторная оценка состояния.";

            if (probability < 0.7)
                return "Целесообразно проведение дополнительных лабораторных и инструментальных исследований.";

            return "Показана консультация профильного специалиста и углублённая диагностика.";
        }

        public static string ToYesNo(double value, bool inverted = false)
        {
            if (!inverted)
                return value == Yes ? "да" : "нет";
            else
                return value == No ? "да" : "нет";
        }
    }
}