namespace Tunts.Models {
    public class Student {     
        public Student(int enrollment, string name, int absence, Grade grade)
        {
            Enrollment = enrollment;
            Name = name;
            Absence = absence;            
            Grade = grade;
            Situation = ESituation.NotInformed;
        }

        public int Enrollment { get; private set; }
        public string Name { get; private set; }
        public int Absence { get; private set; }    
        public ESituation Situation { get; private set; }
        public Grade Grade { get; private set; }        
   
        public void SetSituation(ESituation situation)
        {
            Situation = situation;
        }

        public string GetSituation()
        {
            if(Situation == ESituation.Approved)
                return "Aprovado";                
            else if (Situation == ESituation.DisapprovedForAbsence)
                return  "Reprovado por Falta";
            else if (Situation == ESituation.DisapprovedForGrade)
                return "Reprovado por Nota";
            else if (Situation == ESituation.FinalExam)
                return  "Exame Final";
            else return string.Empty;
        }
    }
}