﻿namespace LibraryAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public ICollection<Author> Authors { get; set; }
        public string Genre { get; set; }
        public int NumberOfPages { get; set; }
        public int YearPublished { get; set; }
        public int TotalCopies { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}