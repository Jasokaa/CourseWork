using DAL;
namespace BLL;

public class MethodsBLL
{
    private DBService <Student> sProvider = new DBService<Student>();
    private DBService<Document?> dProvider = new DBService<Document?>();
    private ListService<Student> slService = new ListService<Student>();
    private ListService<Document?> dlService = new ListService<Document?>();
    private LibraryService libraryService = new LibraryService();
    private StudentService studentService = new StudentService();
    private DocumentService documentService = new DocumentService();
    
    public bool TakeDocumentMethod(Student student, Document? document)
    {
        List<Student>? sList = sProvider.ReadDB(1);
        List<Document?>? dList = dProvider.ReadDB(2);
        if (libraryService.TakeDocument(student, document))
        {
            int sIndex = slService.GetIndex(sList, student);
            slService.ChangeByIndex(sList, student, sIndex);
            int dIndex = dlService.GetIndex(dList, document);
            dlService.ChangeByIndex(dList, document, dIndex);
            sProvider.WriteDB(sList, 1);
            dProvider.WriteDB(dList, 2);
            return true;
        }
        return false;
    }
    public bool ReturnDocumentMethod(Student student, Document? document)
    {
        List<Student>? sList = sProvider.ReadDB(1);
        List<Document?>? dList = dProvider.ReadDB(2);
        if (libraryService.ReturnDocument(student, document))
        {
            int sIndex = slService.GetIndex(sList, student);
            slService.ChangeByIndex(sList, student, sIndex);
            int dIndex = dlService.GetIndex(dList, document);
            dlService.ChangeByIndex(dList, document, dIndex);
            sProvider.WriteDB(sList, 1);
            dProvider.WriteDB(dList, 2);
            return true;
        }
        return false;
    }
    public void SortStudentListMethod(int input)
    {
        List<Student>? list = sProvider.ReadDB(1);
        List<Student>? newList = new List<Student>();
        newList = studentService.SortStudentList(list, input);
        sProvider.WriteDB(newList, 1);
    }
    public void SortDocumentListMethod(int input)
    {
        List<Document?>? list = dProvider.ReadDB(2);
        List<Document?>? newList = new List<Document?>();
        newList = documentService.SortDocumentList(list, input);
        dProvider.WriteDB(newList, 2);
    }

    public void CreateStudent(string fName, string lName, string sCard, string group)
    {
        Student student = new Student(fName, lName, sCard, group);
        List<Student>? list = new List<Student>();
        if (sProvider.ReadDB(1) != null)
        {
            list = sProvider.ReadDB(1);
        }
        list = slService.Add(list, student);
        sProvider.WriteDB(list, 1);
    }
    public void CreateDocument(string name, string author)
    {
        Document? document = new Document(name, author);
        List<Document?>? list = new List<Document?>();
        if (sProvider.ReadDB(2) != null)
        {
            list = dProvider.ReadDB(2);
        }
        list = dlService.Add(list, document);
        dProvider.WriteDB(list, 2);
    }
}