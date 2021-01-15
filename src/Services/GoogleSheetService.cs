using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using System.IO;
using System;
using System.Collections.Generic;
using Tunts.Models;
using System.Text.RegularExpressions;

namespace Tunts.Services
{
    public class GoogleSheetService
    {
        private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        private readonly static string ApplicationName = "Tunts";
        private readonly string SpreadsheetId = "1lBnje8ltjsJysgougqYtKaeBoRa3g6MWHR812xJj2QY";
        private readonly string SheetName = "engenharia_de_software";
        private SheetsService _service;

        public GoogleSheetService()
        {
            GoogleCredential credential;

            using (var stream =
                new FileStream("secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                        .CreateScoped(Scopes);
            }

            _service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        public StudentEntryModel ReadEntries()
        {
            var range = $"{SheetName}!A2:F27";
            var request = _service.Spreadsheets.Values.Get(SpreadsheetId, range);

            var response = request.Execute();
            var values = response.Values;
            var studentEntries = new StudentEntryModel();
            
            if (values != null && values.Count > 0)
            {                  
                var onlyNumber = @"\d+";                
                var totalClasses = int.Parse(Regex.Match( Convert.ToString(values[0][0]), onlyNumber).Value);
            
                studentEntries.ClasseInfo = new ClassInfo(totalClasses);
                int studentRowStart = 2;
                for(int index = studentRowStart; index < values.Count; index++)
                    studentEntries.Students.Add(ConvertToStudent(values[index]));
            }

            return studentEntries;
        }
        public void UpdateEntries(List<Student> students)
        {
            var range = $"{SheetName}!G4:H27";
            var valueRange = new ValueRange();
            var objectList = new List<IList<object>>();           

            foreach (var student in students)
                objectList.Add(new List<object>() { student.GetSituation(), student.Grade.FinalGrade });

            valueRange.Values = objectList;
            var updateRequest = _service.Spreadsheets.Values.Update(valueRange, SpreadsheetId, range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            var updateResponse = updateRequest.Execute();
        }

        private Student ConvertToStudent(IList<object> row)
        {
            return new Student(
                Convert.ToInt32(row[0]),
                Convert.ToString(row[1]),
                Convert.ToInt32(row[2]),
                new Grade(Convert.ToInt32(row[3]), Convert.ToInt32(row[4]), Convert.ToInt32(row[5]))
            );
        }
    }
}
