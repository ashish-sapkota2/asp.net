using Test.API.Extensions;
using Test.API.Models;

namespace Test.API.DTOs
{
    public class MemberDto
    {
            public int Id { get; set; }
            public string Username { get; set; }
            public string PhotoUrl { get; set; }
            public int Age { get; set; }
            public string KnownAs { get; set; }
            public DateTime Created { get; set; }
            public DateTime LastActive { get; set; }
            public string Gender { get; set; }
            public string Introduction { get; set; }
            public string LookingFor { get; set; }
            public int Interest { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }


    }

}
