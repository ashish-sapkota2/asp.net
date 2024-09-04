namespace DatingApp_Dapper.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public bool IsApproved { get; set; }
        public string? PublicId { get; set; }

        // Foreign key to link the photo to a specific user
        public int AppUserId {  get; set; }

        // Navigation property to represent the relationship
        public AppUsers Appusers {  get; set; }
    }
}
