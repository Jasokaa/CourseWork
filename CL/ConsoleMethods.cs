using BLL;
using DAL;

namespace CL;

public class ConsoleMethods
{
    private static readonly DBService <Student> sProvider = new DBService<Student>();
    private static readonly DBService<Document?> dProvider = new DBService<Document?>();
    private static readonly RegexService regexService = new RegexService();
    private static readonly MethodsBLL methodsBll = new MethodsBLL();
    public static void TakeDocument()
    {
        Console.Clear();
        List<Student>? sList = sProvider.ReadDB(1);
        List<Document?>? dList = dProvider.ReadDB(2);
        if (sList == null)
        {
            Console.WriteLine("List of students is empty.");
            Console.ReadLine();
            return;
        }
        if (dList == null)
        {
            Console.WriteLine("List of documents is empty.");
            Console.ReadLine();
            return;
        }
        for (int i = 0; i < sList.Count; i++)
        {
            Console.WriteLine(i + ":");
            Console.WriteLine(sList[i].ToStringShortly());
        }
        Console.WriteLine("MENU - return to main menu");
        Console.WriteLine("Write index of student that will take document:");
        
        string? sIndex = Console.ReadLine();
        int sIntIndex = 0;
        while (true)
        {
            if (regexService.InputIndex(sIndex))
            {
                sIntIndex = int.Parse(sIndex);
                if (sIntIndex >= 0 && sIntIndex < sList.Count)
                {
                    if (sList[sIntIndex].CanTakeDocument())
                    {
                        Console.WriteLine("Student chosen.");
                        Console.WriteLine(sList[sIntIndex].ToStringShortly());
                        break;
                    }
                    Console.WriteLine("This student can't take more then 5 documents.");
                }
            }
            if (sIndex == "MENU")
            {
                break;
            }
            Console.WriteLine("ERROR! Try again to write:");
            sIndex = Console.ReadLine();
            
        }
        if (sIndex == "MENU")
        {
            return;
        }
        Console.ReadLine();
        Console.Clear();
        List<Document> documentsInLibrary = new List<Document>();
        for (int i = 0; i < dList.Count; i++)
        {
            if (dList[i].HasOwner() == false)
            {
                documentsInLibrary.Add(dList[i]);
            }
        }
        for (int i = 0; i < documentsInLibrary.Count; i++)
        {
            Console.WriteLine(i + ":");
            Console.WriteLine(documentsInLibrary[i].ToString());
        }
        Console.WriteLine("MENU - return to main menu");
        Console.WriteLine("Write index of document to take:");
        string? dIndex = Console.ReadLine();
        int dIntIndex = 0;
        while (true)
        {
            if (regexService.InputIndex(dIndex))
            {
                dIntIndex = int.Parse(dIndex);
                if (dIntIndex >= 0 && dIntIndex < documentsInLibrary.Count)
                {
                    if (dList[dIntIndex].HasOwner() == false)
                    {
                        Console.WriteLine("Document chosen.");
                        Console.WriteLine(documentsInLibrary[dIntIndex].ToStringShortly());
                        break;
                    }
                    Console.WriteLine("This document isn't in library.");
                }
            }
            if (dIndex == "MENU")
            {
                break;
            }
            Console.WriteLine("ERROR! Try again to write:");
            dIndex = Console.ReadLine();
        }
        if (dIndex == "MENU")
        {
            return;
        }
        Console.ReadLine();
        Console.Clear();
        bool taken = methodsBll.TakeDocumentMethod(sList[sIntIndex], documentsInLibrary[dIntIndex]);
        if (taken)
        {
            Console.WriteLine("Student took document.");
            Console.WriteLine(sList[sIntIndex]);
            Console.WriteLine(documentsInLibrary[dIntIndex]);
            Console.ReadLine();
        }
    }
    public static void ReturnDocument()
    {
        Console.Clear();
        List<Student>? sList = sProvider.ReadDB(1);
        List<Document?>? dList = dProvider.ReadDB(2);
        if (sList == null)
        {
            Console.WriteLine("List of students is empty.");
            Console.ReadLine();
            return;
        }
        if (dList == null)
        {
            Console.WriteLine("List of documents is empty.");
            Console.ReadLine();
            return;
        }
        for (int i = 0; i < sList.Count; i++)
        {
            Console.WriteLine(i + ":");
            Console.WriteLine(sList[i].ToStringShortly());
        }
        Console.WriteLine("MENU - return to main menu");
        Console.WriteLine("Write index of student that will return document:");
        string? sIndex = Console.ReadLine();
        int sIntIndex = 0;
        while (true)
        {
            if (regexService.InputIndex(sIndex))
            {
                sIntIndex = int.Parse(sIndex);
                if (sIntIndex >= 0 && sIntIndex < sList.Count)
                {
                    Console.WriteLine("Student chosen.");
                    Console.WriteLine(sList[sIntIndex].ToStringShortly());
                    break;
                }
            }
            if (sIndex == "MENU")
            {
                break;
            }
            Console.WriteLine("ERROR! Try again to write:");
            sIndex = Console.ReadLine();
            
        }
        if (sIndex == "MENU")
        {
            return;
        }
        Console.ReadLine();
        Console.Clear();
        List<Document?> documents = dList.FindAll(doc => doc.Owner != null && doc.Owner.Equals(sList[sIntIndex]));
        for (int i = 0; i < documents.Count; i++)
        {
            Console.WriteLine(i + ":");
            Console.WriteLine(documents[i].ToStringShortly());
        }
        Console.WriteLine("MENU - return to main menu");
        Console.WriteLine("Write index of document to return:");
        string? dIndex = Console.ReadLine();
        int dIntIndex = 0;
        while (true)
        {
            if (regexService.InputIndex(dIndex))
            {
                dIntIndex = int.Parse(dIndex);
                if (dIntIndex >= 0 && dIntIndex < documents.Count)
                {
                    Console.WriteLine("Document chosen.");
                    Console.WriteLine(documents[dIntIndex].ToStringShortly());
                    break;
                }
            }
            if (dIndex == "MENU")
            {
                break;
            }
            Console.WriteLine("ERROR! Try again to write:");
            dIndex = Console.ReadLine();
        }
        if (dIndex == "MENU")
        {
            return;
        }
        Console.ReadLine();
        Console.Clear();
        bool returned = methodsBll.ReturnDocumentMethod(sList[sIntIndex], documents[dIntIndex]);
        if (returned)
        {
            Console.WriteLine("Student return document.");
            Console.WriteLine(sList[sIntIndex]);
            Console.WriteLine(documents[dIntIndex]);
            Console.ReadLine();
        }
    }
    public static void ShowAllStudents()
    {
        Console.Clear();
        List<Student>? sList = sProvider.ReadDB(1);
        if (sList == null)
        {
            Console.WriteLine("List is empty.");
        }
        else
        {
            foreach (var s in sList)
            {
                Console.WriteLine(s.ToStringShortly());
            }
        }
        Console.ReadLine();
    }
    public static void ShowAllDocuments()
    {
        Console.Clear();
        List<Document?>? dList = dProvider.ReadDB(2);
        if (dList == null)
        {
            Console.WriteLine("List is empty.");
        }
        else
        {
            foreach (var d in dList)
            {
                Console.WriteLine(d);
            }
        }
        Console.ReadLine();
    }
    public static void DeleteAllInformation()
    {
        Console.Clear();
        sProvider.DeleteAllFromFile(1);
        dProvider.DeleteAllFromFile(2);
        Console.WriteLine("All Information deleted.");
        Console.ReadLine();
    }
    public static void AddInformationForTesting()
    {
        
    }
}