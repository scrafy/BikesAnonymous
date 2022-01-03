namespace BikesAnonymous.Models.Common
{
    public class PaginationDTO
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int RecordsByPage { get; set; }
        public int TotalRecords { get; set; }
    }
}