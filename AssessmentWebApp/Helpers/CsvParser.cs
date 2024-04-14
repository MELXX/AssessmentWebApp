using AssessmentWebApp.Data.Models;
using Csv;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AssessmentWebApp.Helpers
{
    public class CsvParser
    {
        public static List<User> LoadDataFromCSV()
        {
            var csv = File.ReadAllText(@"Data\username_or_email.csv");
            List<User> lst = new List<User>();
            foreach (var line in CsvReader.ReadFromText(csv))
            {
                // Header is handled, each line will contain the actual row data
                try
                {
                    var temp = new User()
                    {
                        Username = line[0],
                        LoginEmail = line[1],
                        Identifier = int.Parse(line[2]),
                        FirstName = line[3],
                        LastName = line[4],
                        Date = DateTime.Parse(line[5].Split(' ')[0]),
                        Values = float.Parse(line[6].Replace('.', ','))
                    };

                    new UserValidator().ValidateAndThrow(temp);

                    lst.Add(temp);
                }
                catch (Exception e){}
            }
            return lst;
            //dbContext.Add<T>()
        }
    }
}
