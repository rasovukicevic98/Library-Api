namespace LibraryAPI.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public int YearOfBirth { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
