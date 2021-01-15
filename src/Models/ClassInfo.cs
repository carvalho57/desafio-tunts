using System;

namespace Tunts.Models
{
    public class ClassInfo
    {
        private static double AbsencePercentage = 0.25;
        public ClassInfo(int totalClasses)
        {
            TotalClasses = totalClasses;
            MaxAbsence = TotalClasses * AbsencePercentage;
        }

        public int TotalClasses { get; private set; }
        public double MaxAbsence { get; private set; }
    }
}