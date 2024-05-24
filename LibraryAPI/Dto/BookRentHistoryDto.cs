namespace LibraryAPI.Dto
{
    public class BookRentHistoryDto
    {
        public string BookName { get; set; }
        public string UserName { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
