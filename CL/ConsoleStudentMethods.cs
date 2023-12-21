using BLL;
using DAL;
//перевірити створення і студ картку на повтор
//повтор кода в створенні і зміні
namespace CL;

public abstract class ConsoleStudentMethods
{
    private static readonly MethodsBLL methodsBll = new MethodsBLL();
    private static readonly RegexService regexService = new RegexService();
    private static readonly DBService <Student> sProvider = new DBService<Student>();
    private static readonly DBService <Document> dProvider = new DBService<Document>();
    private static readonly StudentService studentService = new StudentService();
    private static readonly ListService <Student> sListService = new ListService<Student>();
    public static void AddStudent()
    {
        Console.Clear();
        List<Student> list = sProvider.ReadDB(1);
        Console.WriteLine("Enter first name:");
        string? data = Console.ReadLine();
        while (regexService.InputName(data) == false)
        {
            Console.WriteLine("ERROR! Try again to write:");
            data = Console.ReadLine();
        }
        string firstName = data;
        
        Console.WriteLine("Enter last name:");
        data = Console.ReadLine();
        while (regexService.InputName(data) == false)
        {
            Console.WriteLine("ERROR! Try again to write:");
            data = Console.ReadLine();
        }
        string lastName = data;
        
        Console.WriteLine("Enter student card in format (KB12345678):");
        data = Console.ReadLine();
        while (regexService.InputStudentCard(data) == false || studentService.CheckStudentCard(list, data))
        {
            Console.WriteLine("ERROR! Try again to write:");
            data = Console.ReadLine();
        }
        string? sCard = data;
        
        Console.WriteLine("Enter group in format (SE-224):");
        data = Console.ReadLine();
        while (regexService.InputGroup(data) == false)
        {
            Console.WriteLine("ERROR! Try again to write:");
            data = Console.ReadLine();
        }
        string group = data;
        methodsBll.CreateStudent(firstName, lastName, sCard, group);
        
    }
    public static void ViewStudent()
    {
        Console.Clear();
        List<Student> list = sProvider.ReadDB(1);
        if (list == null)
        {
            Console.WriteLine("List is empty.");
            Console.ReadLine();
            return;
        }
        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine(i + ":");
            Console.WriteLine(list[i].ToStringShortly());
        }
        Console.WriteLine("Enter index of student to view full information:");
        string? index = Console.ReadLine();

        while (true)
        {
            if (regexService.InputIndex(index))
            {
                int intIndex = int.Parse(index);
                if (intIndex >= 0 && intIndex < list.Count)
                {
                    Console.WriteLine(list[intIndex]);
                    break;
                }
            }
            Console.WriteLine("ERROR! Try again to write:");
            index = Console.ReadLine();
        }
        Console.ReadLine();
    }
    public static void ChangeStudent()
    {
        Console.Clear();
        List<Student> list = sProvider.ReadDB(1);
        List<Document> dList = dProvider.ReadDB(2);
        if (list == null)
        {
            Console.WriteLine("List is empty.");
            Console.ReadLine();
            return;
        }
        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine(i + ":");
            Console.WriteLine(list[i].ToStringShortly());
        }
        Console.WriteLine("Enter index of student to change:");
        string? index = Console.ReadLine();

        while (true)
        {
            if (regexService.InputIndex(index))
            {
                int intIndex = int.Parse(index);
                if (intIndex >= 0 && intIndex < list.Count)
                {
                    Student changedStudent = ChangeStudentInfo(list[intIndex]);
                    Console.Clear();
                    Console.WriteLine("Student with changed information:\n" + changedStudent);
                    sListService.ChangeByIndex(list, changedStudent, intIndex);
                    sProvider.WriteDB(list, 1);
                    break;
                }
            }
            Console.WriteLine("ERROR! Try again to write:");
            index = Console.ReadLine();
        }
        Console.ReadLine();
    }
    private static Student ChangeStudentInfo(Student student)
    {
        List<Student> list = sProvider.ReadDB(1);
        Console.Clear();
        Console.WriteLine("Choose what to change:\n" +
                          "1 - first name\n" +
                          "2 - last name\n" +
                          "3 - student card\n" + 
                          "4 - group");
        string input = Console.ReadLine();
        if (input == "1")
        {
            Console.WriteLine("Enter new first name:");
            string? data = Console.ReadLine();
            while (regexService.InputName(data) == false)
            {
                Console.WriteLine("ERROR! Try again to write:");
                data = Console.ReadLine();
            }
            return studentService.ChangeInfo(student, data, 1);
        }
        if (input == "2")
        {
            Console.WriteLine("Enter new last name:");
            string? data = Console.ReadLine();
            while (regexService.InputName(data) == false)
            {
                Console.WriteLine("ERROR! Try again to write:");
                data = Console.ReadLine();
            }
            return studentService.ChangeInfo(student, data, 2);
        }

        if (input == "3")
        {
            Console.WriteLine("Enter new student card in format (KB12345678):");
            string? data = Console.ReadLine();
            while (regexService.InputStudentCard(data) == false || studentService.CheckStudentCard(list, data))
            {
                Console.WriteLine("ERROR! Try again to write:");
                data = Console.ReadLine();
            }
            return studentService.ChangeInfo(student, data, 3);
        }
        if (input == "4")
        {
            Console.WriteLine("Enter new group in format (SE-224):");
            string? data = Console.ReadLine();
            while (regexService.InputGroup(data) == false)
            {
                Console.WriteLine("ERROR! Try again to write:");
                data = Console.ReadLine();
            }
            return studentService.ChangeInfo(student, data, 4);
        }
        return ChangeStudentInfo(student);
    }
    public static void DeleteStudent()
    {
        Console.Clear();
        List<Student> list = sProvider.ReadDB(1);
        List<Document> dList = dProvider.ReadDB(2);
        if (list == null)
        {
            Console.WriteLine("List is empty.");
            Console.ReadLine();
            return;
        }
        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine(i + ":");
            Console.WriteLine(list[i].ToStringShortly());
        }
        Console.WriteLine("Enter index of student to delete:");
        string? index = Console.ReadLine();

        while (true)
        {
            if (regexService.InputIndex(index))
            {
                int intIndex = int.Parse(index);
                if (intIndex >= 0 && intIndex < list.Count)
                {
                    Student student = list[intIndex];
                    foreach (Document doc in dList)
                    {
                        if (student.Equals(doc.Owner))
                        {
                            doc.Owner = null;
                        }
                    }
                    dProvider.WriteDB(dList, 2);
                    sProvider.WriteDB(sListService.DeleteByIndex(list, intIndex), 1);
                    Console.WriteLine("Student deleted.");
                    break;
                }
            }
            Console.WriteLine("ERROR! Try again to write:");
            index = Console.ReadLine();
        }
        Console.ReadLine();
    }
    public static void SortStudents(int input)
    {
        methodsBll.SortStudentListMethod(input);
        ConsoleMethods.ShowAllStudents();
    }
    public static void SearchStudentByCard()
    {
        Console.Clear();
        List<Student> list = sProvider.ReadDB(1);
        if (list == null)
        {
            Console.WriteLine("List is empty.");
            Console.ReadLine();
            return;
        }
        Console.WriteLine("Enter student card in format (KB12345678):");
        string data = Console.ReadLine();
        while (regexService.InputStudentCard(data) == false)
        {
            Console.WriteLine("ERROR! Try again to write:");
            data = Console.ReadLine();
        }
        Student foundStudent = list.Find(student => student.StudentCard == data);

        if (foundStudent != null)
        {
            Console.WriteLine($"Student found:\n{foundStudent}");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
        Console.ReadLine();
    }
}