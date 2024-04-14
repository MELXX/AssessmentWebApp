using System.ComponentModel.DataAnnotations;

namespace AssessmentWebApp.Data.Models
{
    public class User
    {
        [Key]
        public int Identifier { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LoginEmail { get; set; }
        public DateTime Date { get; set; }
        public float Values { get; set; }
    }
}
