using System;

namespace Tunts.Models
{
    public class Grade
    {
        public Grade(int p1, int p2, int p3)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
        }

        public int P1 { get; private set; }
        public int P2 { get; private set; }
        public int P3 { get; private set; }
        public double FinalGrade { get; private set; }

        public double CalculateAverage()
        {
            return (P1 + P2 + P3) / 3;
        }        

        public void CalculateFinalExameGrade(double average)
        {
            FinalGrade = Math.Ceiling(100 - average);
        }
    }
}