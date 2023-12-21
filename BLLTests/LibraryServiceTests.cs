namespace BLLTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using DAL;
[TestClass]
public class LibraryServiceTests
{
    private LibraryService libraryService;
    private Student student;
    private Document? document;

    [TestInitialize]
    public void TestInitialize()
    {
        libraryService = new LibraryService();
        student = new Student("John", "Doe", "12345", "Group1");
        document = new Document("Book1", "Author1");
    }

    [TestMethod]
    public void TakeDocument_ValidInput_ReturnsTrue()
    {
        // Arrange
        // Act
        bool result = libraryService.TakeDocument(student, document);
        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(student, document.Owner);
        Assert.IsTrue(student.Documents.Contains(document));
    }

    [TestMethod]
    public void TakeDocument_DocumentAlreadyTaken_ReturnsFalse()
    {
        // Arrange
        student.AddDocument(document);  // Simulate that the student already has the document
        document.Owner = student;
        // Act
        bool result = libraryService.TakeDocument(student, document);
        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void TakeDocument_DocumentIsNull_ReturnsFalse()
    {
        // Arrange
        // Act
        bool result = libraryService.TakeDocument(student, null);
        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ReturnDocument_ValidInput_ReturnsTrue()
    {
        // Arrange
        libraryService.TakeDocument(student, document);  // Simulate that the student has taken the document
        // Act
        bool result = libraryService.ReturnDocument(student, document);
        // Assert
        Assert.IsTrue(result);
        Assert.IsNull(document.Owner);
        Assert.IsFalse(student.Documents.Contains(document));
    }

    [TestMethod]
    public void ReturnDocument_InvalidStudent_ReturnsFalse()
    {
        // Arrange
        // Act
        bool result = libraryService.ReturnDocument(null, document);
        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ReturnDocument_InvalidDocument_ReturnsFalse()
    {
        // Arrange
        // Act
        bool result = libraryService.ReturnDocument(student, null);
        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ReturnDocument_DifferentOwner_ReturnsFalse()
    {
        // Arrange
        Student otherStudent = new Student("Jane", "Doe", "67890", "Group2");
        libraryService.TakeDocument(otherStudent, document);  // Simulate that a different student has taken the document
        // Act
        bool result = libraryService.ReturnDocument(student, document);
        // Assert
        Assert.IsFalse(result);
    }
}
