using Azure.Identity;

namespace DatingApp_Dapper.DTOs
{
    public class MemberDto
    {
        public string username { get; set; }
        public string Url { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age {
            get
            {
                return CalculateAge(DateOfBirth);
            }
        } 
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Gender { get; set; }
        public string? Introduction { get; set; }
        public string? LookingFor { get; set; }
        public string? Interest { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    private int CalculateAge(DateTime dob)
    {
        var today = DateTime.Today;
        var age = today.Year - dob.Year;
        if (dob.Date > today.AddYears(-age)) age--;
        return age;
    }

    }

}
