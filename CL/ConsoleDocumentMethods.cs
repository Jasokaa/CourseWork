using BLL;
using DAL;

namespace CL;

public abstract class ConsoleDocumentMethods
{
    private static readonly MethodsBLL methodsBll = new MethodsBLL();
    private static readonly RegexService regexService = new RegexService();
    private static readonly DBService <Document> dProvider = new DBService<Document>();
    private static readonly DBService <Student> sProvider = new DBService<Student>();
    private static readonly DocumentService documentService = new DocumentService();
    private static readonly ListService <Document> dListService = new ListService<Document>();
    public static void AddDocument()
    {
        Console.Clear();
        List<Document> list = dProvider.ReadDB(2);
        Console.WriteLine("Enter name:");
        string? data = Console.ReadLine();
        while (regexService.InputDocumentName(data) == false || documentService.CheckName(list, data))
        {
            Console.WriteLine("ERROR! Try again to write:");
            data = Console.ReadLine();
        }
        string name = data;
        
        Console.WriteLine("Enter author:");
        data = Console.ReadLine();
        while (regexService.InputAuthor(data) == false)
        {
            Console.WriteLine("ERROR! Try again to write:");
            data = Console.ReadLine();
        }
        string author = data;
        methodsBll.CreateDocument(name, author);
    }
    public static void ViewDocument()
    {
        Console.Clear();
        List<Document> list = dProvider.ReadDB(2);
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
        Console.WriteLine("Enter index of document to view full information:");
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
    public static void ChangeDocument()
    {
        Console.Clear();
        List<Document> list = dProvider.ReadDB(2);
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
        Console.WriteLine("Enter index of document to change:");
        string? index = Console.ReadLine();

        while (true)
        {
            if (regexService.InputIndex(index))
            {
                int intIndex = int.Parse(index);
                if (intIndex >= 0 && intIndex < list.Count)
                {
                    Document changedDocument = ChangeDocumentInfo(list[intIndex]);
                    Console.Clear();
                    Console.WriteLine("Document with changed information:\n" + changedDocument);
                    dListService.ChangeByIndex(list, changedDocument, intIndex);
                    dProvider.WriteDB(list, 2);
                    break;
                }
            }
            Console.WriteLine("ERROR! Try again to write:");
            index = Console.ReadLine();
        }
        Console.ReadLine();
    }
    private static Document ChangeDocumentInfo(Document document)
    {
        Console.Clear();
        List<Document> list = dProvider.ReadDB(2);
        Console.WriteLine("Choose what to change:\n" +
                          "1 - name\n" +
                          "2 - author\n");
        string input = Console.ReadLine();
        if (input == "1")
        {
            Console.WriteLine("Enter new name:");
            string? data = Console.ReadLine();
            while (regexService.InputDocumentName(data) == false || documentService.CheckName(list, data))
            {
                Console.WriteLine("ERROR! Try again to write:");
                data = Console.ReadLine();
            }
            return documentService.ChangeInfo(document, data, 1);
        }
        if (input == "2")
        {
            Console.WriteLine("Enter new author:");
            string? data = Console.ReadLine();
            while (regexService.InputAuthor(data) == false)
            {
                Console.WriteLine("ERROR! Try again to write:");
                data = Console.ReadLine();
            }
            return documentService.ChangeInfo(document, data, 2);
        }
        return ChangeDocumentInfo(document);
    }
    public static void DeleteDocument()
    {
        Console.Clear();
        List<Document> list = dProvider.ReadDB(2);
        List<Student> sList = sProvider.ReadDB(1);
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
        Console.WriteLine("Enter index of document to delete:");
        string? index = Console.ReadLine();

        while (true)
        {
            if (regexService.InputIndex(index))
            {
                int intIndex = int.Parse(index);
                if (intIndex >= 0 && intIndex < list.Count)
                {
                    Document document = list[intIndex];
                    Student student = sList.Find(s => document.Owner != null && s.Equals(document.Owner));
                    int indexOfDoc = student.IndexOf(document);
                    if (student != null && document != null)
                    {
                        if (indexOfDoc != -1)
                        {
                            student.Documents[indexOfDoc] = null;
                        }
                    }
                    sProvider.WriteDB(sList, 1);
                    dProvider.WriteDB(dListService.DeleteByIndex(list, intIndex), 2);
                    Console.WriteLine("Document deleted.");
                    break;
                }
            }
            Console.WriteLine("ERROR! Try again to write:");
            index = Console.ReadLine();
        }
        Console.ReadLine();
    }
    public static void SortDocuments(int input)
    {
        methodsBll.SortDocumentListMethod(input);
        ConsoleMethods.ShowAllDocuments();
    }
    public static void SearchDocumentByName()
    {
        Console.Clear();
        List<Document> list = dProvider.ReadDB(2);
        if (list == null)
        {
            Console.WriteLine("List is empty.");
            Console.ReadLine();
            return;
        }
        Console.WriteLine("Enter document name:");
        string data = Console.ReadLine();
        while (regexService.InputDocumentName(data) == false)
        {
            Console.WriteLine("ERROR! Try again to write:");
            data = Console.ReadLine();
        }
        Document foundDocument = list.Find(document => document.Name == data);

        if (foundDocument != null)
        {
            Console.WriteLine($"Document found:\n{foundDocument}");
        }
        else
        {
            Console.WriteLine("Document not found.");
        }
        Console.ReadLine();
    }
}