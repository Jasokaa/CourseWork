using DAL;
namespace BLL;
public class DBService<T>
{
    private const string FileStudents = "fileStudents.json";
    private const string FileDocuments = "fileDocuments.json";

    public void WriteDB(List<T>? list, int input)
    {
        try
        {
            switch (input)
            {
                case 1:
                {
                    JSONProvider<T> provider = new JSONProvider<T>();
                    provider.Write(list, FileStudents);
                    break;
                }
                case 2:
                {
                    JSONProvider<T> provider = new JSONProvider<T>();
                    provider.Write(list, FileDocuments);
                    break;
                }
            }
        }
        catch (Exception) { /*ignored*/ }
    }
    public List<T>? ReadDB(int input)
    {
        List<T>? list = null;
        try
        {
            switch (input)
            {
                case 1:
                {
                    JSONProvider<T> provider = new JSONProvider<T>();
                    list = provider.Read(FileStudents);
                    return list;
                }
                case 2:
                {
                    JSONProvider<T> provider = new JSONProvider<T>();
                    list = provider.Read(FileDocuments);
                    return list;
                }
            }
        }
        catch (Exception) { /*ignored*/ }
        return list;
    }
    public void DeleteAllFromFile(int input)
    {
        try
        {
            switch (input)
            {
                case 1:
                {
                    JSONProvider<T> provider = new JSONProvider<T>();
                    provider.DeleteAllFromFile(FileStudents);
                    break;
                }
                case 2:
                {
                    JSONProvider<T> provider = new JSONProvider<T>();
                    provider.DeleteAllFromFile(FileDocuments);
                    break;
                }
            }
        }
        catch (Exception) { /*ignored*/ }
    }
}