namespace MoviesAPI.DTOs
{
    public class GenrasDTO
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
