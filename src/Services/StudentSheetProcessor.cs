using System.Collections.Generic;
using Tunts.Models;

namespace Tunts.Services
{
    public static class StudentSheetProcessor
    {
        public static List<Student> ProcessStudentsSituation(StudentEntryModel studentEntry)
        {
            foreach (var student in studentEntry.Students)
            {
                VerifyStudentAbsence(student, studentEntry.ClasseInfo);
                if (student.Situation != ESituation.DisapprovedForAbsence)
                    ProcessStudentGrade(student);
            }
            return studentEntry.Students;
        }
        private static void ProcessStudentGrade(Student student)
        {
            var average = student.Grade.CalculateAverage();
            if (average < 50)
                student.SetSituation(ESituation.DisapprovedForGrade);
            else if (average >= 50 && average < 70)
            {
                student.SetSituation(ESituation.FinalExam);
                student.Grade.CalculateFinalExameGrade(average);
            }
            else
                student.SetSituation(ESituation.Approved);
        }


        private static void VerifyStudentAbsence(Student student, ClassInfo classe)
        {
            if (student.Absence > classe.MaxAbsence)
                student.SetSituation(ESituation.DisapprovedForAbsence);
        }
    }
}