using BLL;
namespace CL;

public abstract class ConsoleMenu
{
    public static string Commands()
    {
        Console.Clear();
        const string commands = "MAIN MENU:\n" +
                                   "1 - student action (add, view, change, delete, sort)\n" +
                                   "2 - document action (add, view, change, delete, sort)\n" +
                                   "3 - take document\n" +
                                   "4 - return document\n" +
                                   "5 - show all students\n" + 
                                   "6 - show all documents\n" +
                                   "7 - delete all information\n" +
                                   //"8 - add information for testing\n" +
                                   "EXIT - stop program\n";
        Console.Write(commands);
        return $"{Console.ReadLine()}";
    }
    private static string StudentCommands()
    {
        Console.Clear();
        const string commands = "STUDENT MENU:\n" +
                                   "1 - add student\n" +
                                   "2 - view full information about student\n" +
                                   "3 - change information about student\n" +
                                   "4 - delete student\n" +
                                   "5 - sort students\n" + 
                                   "6 - search student by card\n" +
                                   "MENU - return to main menu\n";
        Console.Write(commands);
        return $"{Console.ReadLine()}";
    }
    private static string SortStudentsCommands()
    {
        Console.Clear();
        string commands = "SORT STUDENTS MENU:\n" +
                             "1 - sort by first name\n" +
                             "2 - sort by last name\n" +
                             "3 - sort by academic group\n" +
                             "MENU - return to student menu\n";
        Console.Write(commands);
        return $"{Console.ReadLine()}";
    }
    private static string DocumentCommands()
    {
        Console.Clear();
        string commands = "DOCUMENT MENU:\n" +
                             "1 - add document\n" +
                             "2 - view full information about document\n" +
                             "3 - change information about document\n" +
                             "4 - delete document\n" +
                             "5 - sort documents\n" + 
                             "6 - search document by name\n" +
                             "MENU - return to main menu\n";
        Console.Write(commands);
        return $"{Console.ReadLine()}";
    }
    private static string SortDocumentsCommands()
    {
        Console.Clear();
        string commands = "SORT DOCUMENTS MENU:\n" +
                          "1 - sort by name\n" +
                          "2 - sort by author\n" +
                          "MENU - return to document menu\n";
        Console.Write(commands);
        return $"{Console.ReadLine()}";
    }
    public static int UserInputCommands(string input)
    {
        try
        {
            switch (input)
            {
                case "1":
                {
                    //student action (add, view, change, delete, sort)
                    string studentInput = StudentCommands();
                    string returnFromUserInput = UserInputStudentCommands(studentInput);
                    while (returnFromUserInput != "MENU")
                    {
                        studentInput = StudentCommands();
                        returnFromUserInput = UserInputStudentCommands(studentInput);
                    }
                    return 0;
                }
                case "2":
                {
                    //document action (add, view, change, delete, sort)
                    string documentInput = DocumentCommands();
                    string returnFromUserInput = UserInputDocumentCommands(documentInput);
                    while (returnFromUserInput != "MENU")
                    {
                        documentInput = DocumentCommands();
                        returnFromUserInput = UserInputDocumentCommands(documentInput);
                    }
                    return 0;
                }
                case "3":
                {
                    //take document
                    ConsoleMethods.TakeDocument();
                    return 0;
                }
                case "4":
                {
                    //return document
                    ConsoleMethods.ReturnDocument();
                    return 0;
                }
                case "5":
                {
                    //show all students
                    ConsoleMethods.ShowAllStudents();
                    return 0;
                }
                case "6":
                {
                    //show all documents
                    ConsoleMethods.ShowAllDocuments();
                    return 0;
                }
                case "7":
                {
                    //delete all information
                    ConsoleMethods.DeleteAllInformation();
                    return 0;
                }
                /*case "8":
                {
                    //add information for testing
                    ConsoleMethods.AddInformationForTesting();
                    return 0;
                }*/
                case "EXIT":
                {
                    return 1;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return 0;
    }
    private static string UserInputStudentCommands(string input)
    {
        try
        {
            switch (input)
            {
                case "1":
                {
                    //add student
                    ConsoleStudentMethods.AddStudent();
                    return "";
                }
                case "2":
                {
                    //view all information about student
                    ConsoleStudentMethods.ViewStudent();
                    return "";
                }
                case "3":
                {
                    //change information about student
                    ConsoleStudentMethods.ChangeStudent();
                    return "";
                }
                case "4":
                {
                    //delete student
                    ConsoleStudentMethods.DeleteStudent();
                    return "";
                }
                case "5":
                {
                    //sort students
                    string studentInput = SortStudentsCommands();
                    string returnFromUserInput = UserInputSortStudentsCommands(studentInput);
                    while (returnFromUserInput != "MENU")
                    {
                        studentInput = SortStudentsCommands();
                        returnFromUserInput = UserInputSortStudentsCommands(studentInput);
                    }
                    return "";
                }
                case "6":
                {
                    //search student by card
                    ConsoleStudentMethods.SearchStudentByCard();
                    return "";
                }
                case "MENU":
                {
                    //return to main menu
                    return "MENU";
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return "";
    }
    private static string UserInputDocumentCommands(string input)
    {
        try
        {
            switch (input)
            {
                case "1":
                {
                    //add document
                    ConsoleDocumentMethods.AddDocument();
                    return "";
                }
                case "2":
                {
                    //view all information about document
                    ConsoleDocumentMethods.ViewDocument();
                    return "";
                }
                case "3":
                {
                    //change information about document
                    ConsoleDocumentMethods.ChangeDocument();
                    return "";
                }
                case "4":
                {
                    //delete document
                    ConsoleDocumentMethods.DeleteDocument();
                    return "";
                }
                case "5":
                {
                    //sort documents
                    string documentInput = SortDocumentsCommands();
                    string returnFromUserInput = UserInputSortDocumentsCommands(documentInput);
                    while (returnFromUserInput != "MENU")
                    {
                        documentInput = SortDocumentsCommands();
                        returnFromUserInput = UserInputSortDocumentsCommands(documentInput);
                    }
                    return "";
                }
                case "6":
                {
                    //search document by name
                    ConsoleDocumentMethods.SearchDocumentByName();
                    return "";
                }
                case "MENU":
                {
                    //return to main menu
                    return "MENU";
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return "";
    }
    private static string UserInputSortStudentsCommands(string input)
    {
        try
        {
            switch (input)
            {
                case "1":
                {
                    //sort by first name
                    ConsoleStudentMethods.SortStudents(1);
                    return "";
                }
                case "2":
                {
                    //sort by last name
                    ConsoleStudentMethods.SortStudents(2);
                    return "";
                }
                case "3":
                {
                    //sort by academic group
                    ConsoleStudentMethods.SortStudents(3);
                    return "";
                }
                case "MENU":
                {
                    //return to main menu
                    return "MENU";
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return "";
    }
    private static string UserInputSortDocumentsCommands(string input)
    {
        try
        {
            switch (input)
            {
                case "1":
                {
                    //sort by name
                    ConsoleDocumentMethods.SortDocuments(1);
                    return "";
                }
                case "2":
                {
                    //sort by author
                    ConsoleDocumentMethods.SortDocuments(2);
                    return "";
                }
                case "MENU":
                {
                    //return to main menu
                    return "MENU";
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        return "";
    }
}