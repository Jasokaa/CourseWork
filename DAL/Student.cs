namespace DAL;
public class Student
{
    private string firstName;
    private string lastName;
    private string studentCard;
    private string group;
    private Document?[] documents = new Document?[5];
    public string FirstName
    {
        get => firstName;
        set => firstName = value ?? throw new ArgumentNullException(nameof(value));
    }
    public string LastName
    {
        get => lastName;
        set => lastName = value ?? throw new ArgumentNullException(nameof(value));
    }
    public string StudentCard
    {
        get => studentCard;
        set => studentCard = value ?? throw new ArgumentNullException(nameof(value));
    }
    public string Group
    {
        get => group;
        set => group = value ?? throw new ArgumentNullException(nameof(value));
    }
    public Document?[] Documents
    {
        get => documents;
        set => documents = value;
    }
    public Student(string firstName, string lastName, string studentCard, string group)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.studentCard = studentCard;
        this.group = group;
    }
    public void AddDocument(Document? document)
    {
        SortDocuments();
        bool documentAdded = false;

            for (int i = 0; i < 5; i++)
            {
                if (documents[i] == null)
                {
                    documents[i] = document;
                    documentAdded = true;
                    break;
                }
            }
            if (!documentAdded)
            {
                throw new Exception(firstName + " " + lastName + " has already taken 5 documents");
            }
        SortDocuments();
    }
    /*public void DeleteDocumentByIndex(int index)
    {
        if (index < 0 || index >= 5)
        {
            throw new Exception("Index out of range.");
        }

        for (int i = 0; i < 5; i++)
        {
            if (i == index)
            {
                documents[i] = null;
            }
        }
        SortDocuments();
    }

    public Document? GetDocumentByIndex(int index)
    {
        SortDocuments();
        if (index < 0 || index >= 5)
        {
            throw new Exception("Index out of range.");
        }
        if (documents[index] == null)
        {
            throw new Exception("Invalid index.");
        }
        else
        {
            return documents[index];
        }
    }*/
    public void DeleteDocument(Document? document)
    {
        bool documentDeleted = false;
        for (int i = 0; i < 5; i++)
        {
            if (EqualityComparer<Document?>.Default.Equals(documents[i], document))
            {
                documents[i] = null;
                documentDeleted = true;
                SortDocuments();
                break;
            }
        }

        if (!documentDeleted)
        {
            throw new Exception(firstName + " " + lastName + " doesn't have document: " + document);
        }
    }
    public int IndexOf(Document? document)
    {
        for (int i = 0; i < documents.Length; i++)
        {
            //name is unique for every book
            if (documents[i] != null && document != null && 
                string.Compare(documents[i]?.Name, document?.Name, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return i;
            }
                
        }
        return -1;
    }
    public void SortDocuments()
    {
        for (int i = 0; i < documents.Length - 1; i++)
        {
            for (int j = i + 1; j < documents.Length; j++)
            {
                if (documents[i] != null && documents[j] != null)
                {
                    if (documents[i]!.CompareTo(documents[j]) > 0)
                    {
                        (documents[i], documents[j]) = (documents[j], documents[i]);//Tuple deconstruction
                    }
                }
                else if (documents[i] == null && documents[j] != null)
                {
                    (documents[i], documents[j]) = (documents[j], documents[i]);
                }
            }
        }
    }
    public bool CanTakeDocument()
    {
        SortDocuments();
        for (int i = 0; i < 5; i++)
        {
            if (documents[i] == null)
            {
                return true;
            }
        }
        return false;
    }
    public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
    
            Student otherStudent = (Student)obj;
            return string.Equals(studentCard, otherStudent.studentCard);
        }
    public override int GetHashCode()
        {
            return studentCard.GetHashCode();
        }
    public string ToStringShortly()
    {
        SortDocuments();
        int docs = 0;
        for (int i = 0; i < 5; i++)
        {
            if (documents[i] != null)
            {
                docs++;
            }
        }
        string str = "Student: " + firstName + " " + lastName + "\n" +
                     "StudentCard: " + studentCard + ", " +
                     "Group: " + group + ", " + 
                     "Has documents: "+ docs + "\n";
        return str;
    }
    public override string ToString()
    {
        SortDocuments();
        string str = "Student: " + firstName + " " + lastName + "\n" +
                     "StudentCard: " + studentCard + "\n" +
                     "Group: " + group + "\n" + 
                     "Documents list:\n";
        string list = "";
        for (int i = 0; i < 5; i++)
        {
            if (documents[i] != null)
            {
                list = list + documents[i]!.ToStringShortly() + "\n";
            }
            else
            {
                list += "________________\n";
            }
        }
        return str + list;
    }
    
}