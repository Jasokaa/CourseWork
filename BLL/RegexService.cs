using System.Text.RegularExpressions;
namespace BLL;

public class RegexService
{
    public bool InputName(string info)//firstName or lastName
    {
        Regex regex = new Regex(@"^[A-Z]{1}[a-z]+$");
        return regex.IsMatch(info);
    }
    public bool InputStudentCard(string info)
    {
        Regex regex = new Regex(@"^KB\d{8}$");
        return regex.IsMatch(info);
    }
    public bool InputGroup(string info)
    {
        Regex regex = new Regex(@"^[A-Z]{2}-\d{3}$");
        return regex.IsMatch(info);
    }
    
    public bool InputDocumentName(string info)
    {
        Regex regex = new Regex(@"^[A-Z\d][a-zA-Z\d]*(?:\s+[A-Z\d][a-zA-Z\d]*)*$");
        return regex.IsMatch(info);
    }
    public bool InputAuthor(string info)
    {
        Regex regex = new Regex(@"^[A-Z][a-z]*(?:\s+[A-Z][a-z]*)*$");
        return regex.IsMatch(info);
    }

    public bool InputIndex(string? info)
    {
        Regex regex = new Regex(@"[\d]");
        return regex.IsMatch(info);
    }
}