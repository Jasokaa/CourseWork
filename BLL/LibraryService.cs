using DAL;
namespace BLL;
public class LibraryService
{
    public bool TakeDocument(Student student, Document? document)
    {
        if (document != null && document.Owner == null && student.CanTakeDocument())
        {
            student.AddDocument(document);
            document.AddOwner(student);
            return true;
        }
        return false;
    }
    public bool ReturnDocument(Student? student, Document? document)
    {
        if (document != null && student != null && document.Owner != null)
        {
            if (student.Equals(document.Owner))
            {
                student.DeleteDocument(document);
                document.DeleteOwner();
                return true;
            }
        }
        return false;
    }
}