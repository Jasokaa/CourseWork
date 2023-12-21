using System.Text.Json.Serialization;

namespace DAL;
public class Document: IComparable<Document>
{
    private string author;
    private string name;
    private Student? owner;
    
    public string Author
    {
        get => author;
        set => author = value ?? throw new ArgumentNullException(nameof(value));
    }
    public string Name
    {
        get => name;
        set => name = value ?? throw new ArgumentNullException(nameof(value));
    }
    public Student? Owner
    {
        get => owner;
        set => owner = value;
    }
    [JsonConstructor]
    public Document(string name, string author)
    {
        this.author = author;
        this.name = name;
    }
    
    public Document(string name, string author, Student? owner)
    {
        this.author = author;
        this.name = name;
        this.owner = owner;
    }
    public void AddOwner(Student student)
    {
        if (owner != null)
        {
            throw new Exception(this.ToStringShortly() + "is not in library.");
        }
        else
        {
            owner = student;
        }
    }
    public void DeleteOwner()
    {
        owner = null;
    }
    public bool HasOwner()
    {
        if (owner == null)
        {
            return false;
        }
        return true;
    }
    public int CompareTo(Document? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        return string.Compare(name, other.name, StringComparison.Ordinal);
    }
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Document otherDocument = (Document)obj;
        return string.Equals(name, otherDocument.name, StringComparison.Ordinal) &&
               string.Equals(author, otherDocument.author, StringComparison.Ordinal);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + (name != null ? StringComparer.Ordinal.GetHashCode(name) : 0);
            hash = hash * 23 + (author != null ? StringComparer.Ordinal.GetHashCode(author) : 0);
            hash = hash * 23 + (owner != null ? owner.GetHashCode() : 0);
            return hash;
        }
    }
    public string ToStringShortly()
    {
        return author + " \"" + name + "\"";
    }
    public override string ToString()
    {
        string str = author + " \"" + name + "\"";
        if (owner != null)
        {
            str = str + " - is given to " + owner.FirstName + " " + owner.LastName;
        }
        else
        {
            str += " - in library";
        }
        return str;
    }
}