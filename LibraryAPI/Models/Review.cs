namespace LibraryAPI.Models
{
    public class Review
    {
        public string UserId { get; set; }
        public User Reviewer { get; set; }
        
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int Rating { get; set; }
        public string Comment { get; set; }
        
    }
}
