using DAL;

namespace BLL;

public class StudentService
{
    private static readonly RegexService regexService = new RegexService();
    private static readonly DBService<Document> dProvider = new DBService<Document>();
    
    public List<Student>? SortStudentList(List<Student> list, int input)
    {
        List<Student>? newList = null;
        try
        {
            switch (input)
            {
                case 1:
                {
                    newList = list.OrderBy(s => s.FirstName).ToList();
                    return newList;
                }
                case 2:
                {
                    newList = list.OrderBy(s => s.LastName).ToList();
                    return newList;
                }
                case 3:
                {
                    newList = list.OrderBy(s => s.Group).ToList();
                    return newList;
                }
            }
        }
        catch (Exception) { /*ignored*/ }
        return newList;
    }

    public Student? SearchStudent(List<Student>? list, string inputCard)
    {
        if (list == null)
        {
            throw new Exception("List is empty.");
        }
        else
        {
            Student? foundStudent = list.FirstOrDefault(student => student.StudentCard == inputCard);
            return foundStudent;
        }
    }
    public bool CheckStudentCard(List<Student>? list, string inputCard)
    {
        try
        {
            bool cardExists = list.Any(student => student.StudentCard == inputCard);
            return cardExists;
        }
        catch (Exception) { /*ignored*/ }
        return false;
    }

    public Student ChangeInfo(Student student, string info, int input)
    {
        List<Document> documents = dProvider.ReadDB(2);
        Student newStudent = student;
        if (input == 1 && regexService.InputName(info))
        {
            newStudent.FirstName = info;
        }
        if (input == 2 && regexService.InputName(info))
        {
            newStudent.LastName = info;
        }
        if (input == 3 && regexService.InputStudentCard(info))
        {
            newStudent.StudentCard = info;
        }
        if (input == 4 && regexService.InputGroup(info))
        {
            newStudent.Group = info;
        }

        foreach (Document document in documents)
        {
            if (document != null && student.Equals(document.Owner))
            {
                document.Owner = newStudent;
            }
        }
        dProvider.WriteDB(documents, 2);
        return newStudent;
    }
}