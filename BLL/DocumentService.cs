using DAL;
namespace BLL;
public class DocumentService
{
    private static readonly RegexService regexService = new RegexService();
    private static readonly DBService <Student> sProvider = new DBService<Student>();
    private static readonly ListService <Student> sListService = new ListService<Student>();
    public List<Document?>? SortDocumentList(List<Document?>? list, int input)
    {
        List<Document?>? newList = null;
        try
        {
            switch (input)
            {
                case 1:
                {
                    newList = list.OrderBy(s => s.Name).ToList();
                    return newList;
                }
                case 2:
                {
                    newList = list.OrderBy(s => s.Author).ToList();
                    return newList;
                }
            }
        }
        catch (Exception) { /*ignored*/ }
        return newList;
    }
    public Document? SearchDocument(List<Document>? list, string inputName)
    {
        if (list == null)
        {
            throw new Exception("List is empty.");
        }

        Document? foundDocument = null;
        foundDocument = list.Find(document => document.Name == inputName);
        return foundDocument;
    }
    public bool CheckName(List<Document>? list, string inputName)
    {
        try
        {
            bool nameExists = list.Any(document => document.Name == inputName);
            return nameExists;
        }
        catch (Exception) { /*ignored*/ }
        return false;
    }
    public Document ChangeInfo(Document document, string info, int input)
    {
        List<Student> sList = sProvider.ReadDB(1);
        Student student = sList.Find(s => document.Owner != null && s.Equals(document.Owner));
        int index = student.IndexOf(document);
        Student newStudent = student;
        Document newDocument = document;
        if (input == 1 && regexService.InputDocumentName(info))
        {
            newDocument.Name = info;
        }
        if (input == 2 && regexService.InputAuthor(info))
        {
            newDocument.Author = info;
        }
        if (student != null && document != null)
        {
            if (index != -1)
            {
                newStudent.Documents[index] = newDocument;
                List<Student>? newSList = sListService.ChangeByIndex(sList, newStudent, sListService.GetIndex(sList, student));
                sProvider.WriteDB(newSList, 1);
            }
        }
        return newDocument;
    }
}