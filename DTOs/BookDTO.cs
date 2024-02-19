namespace myappdotnet.DTOs;
public class BookDTO
{
    public string? Title { get; set; }
    public ICollection<AuthorDTO>? Authors { get; set; }
    public int Publication_year { get; set; }
}
