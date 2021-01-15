using System;
using Tunts.Services;

namespace Tunts
{
    class Program
    {
        static void Main(string[] args)
        {
            var googleService = new GoogleSheetService();   
            Console.WriteLine("Reading rows from the sheet");
            var studentEntries = googleService.ReadEntries();
            Console.WriteLine("Calculating student grade");
            var studentsUpdated = StudentSheetProcessor.ProcessStudentsSituation(studentEntries);
            Console.WriteLine("Updating sheet");
            googleService.UpdateEntries(studentsUpdated);
            Console.WriteLine("Press a Key to exit");
            Console.ReadKey();            
        }    
    }
}
