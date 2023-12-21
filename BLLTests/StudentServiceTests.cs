using BLL;
using DAL;

namespace BLLTests;

[TestClass]
    public class StudentServiceTests
    {
        private StudentService studentService;
        private List<Student> students;
        private DBService<Student> sProvider;
        private DBService<Document> dProvider;
        [TestInitialize]
        public void TestInitialize()
        {
            studentService = new StudentService();
            students = new List<Student>
            {
                new Student("John", "Doe", "12345", "Group1"),
                new Student("Jane", "Doe", "67890", "Group2"),
                new Student("Alice", "Smith", "54321", "Group1")
            };
            sProvider = new DBService<Student>();
            dProvider = new DBService<Document>();
        }
        [TestMethod]
        public void SortStudentList_SortByFirstName_ReturnsSortedByFirstName()
        {
            // Arrange
            // Act
            List<Student>? sortedStudents = studentService.SortStudentList(students, 1);
            // Assert
            CollectionAssert.AreEqual(
                students.OrderBy(student => student.FirstName).ToList(),
                sortedStudents
            );
        }
        [TestMethod]
        public void SortStudentList_SortByLastName_ReturnsSortedByLastName()
        {
            // Arrange
            // Act
            List<Student>? sortedStudents = studentService.SortStudentList(students, 2);
            // Assert
            CollectionAssert.AreEqual(
                students.OrderBy(student => student.LastName).ToList(),
                sortedStudents
            );
        }
        [TestMethod]
        public void SortStudentList_SortByGroup_ReturnsSortedByGroup()
        {
            // Arrange
            // Act
            List<Student>? sortedStudents = studentService.SortStudentList(students, 3);
            // Assert
            CollectionAssert.AreEqual(
                students.OrderBy(student => student.Group).ToList(),
                sortedStudents
            );
        }
        [TestMethod]
        public void SearchStudent_ListIsNull_ThrowsException()
        {
            // Arrange
            List<Student>? students = null;
            string inputCard = "12345";
            // Act & Assert
            Assert.ThrowsException<Exception>(() => studentService.SearchStudent(students, inputCard));
        }
        [TestMethod]
        public void SearchStudent_ExistingCard_ReturnsStudent()
        {
            // Arrange
            string inputCard = "12345";
            // Act
            Student? foundStudent = studentService.SearchStudent(students, inputCard);
            // Assert
            Assert.IsNotNull(foundStudent);
            Assert.AreEqual(inputCard, foundStudent!.StudentCard);
        }
        [TestMethod]
        public void SearchStudent_NonExistingCard_ReturnsNull()
        {
            // Arrange
            string inputCard = "99999";
            // Act
            Student? foundStudent = studentService.SearchStudent(students, inputCard);
            // Assert
            Assert.IsNull(foundStudent);
        }
        [TestMethod]
        public void CheckStudentCard_ExistingCard_ReturnsTrue()
        {
            // Arrange
            string inputCard = "12345";
            // Act
            bool result = studentService.CheckStudentCard(students, inputCard);
            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void CheckStudentCard_NonExistingCard_ReturnsFalse()
        {
            // Arrange
            string inputCard = "99999";
            // Act
            bool result = studentService.CheckStudentCard(students, inputCard);
            // Assert
            Assert.IsFalse(result);
        }
        
        [TestMethod]
        public void ChangeInfo_ChangeStudentFirstName_SuccessfullyChanged()
        {
            // Arrange
            Student student = new Student("John", "Doe", "12345", "Group1");
            Document document = new Document("OldName", "OldAuthor", student);
            student.AddDocument(document);
            List<Student> sList = new List<Student> { student };
            List<Document> dList = new List<Document> { document };
            sProvider.WriteDB(sList, 1);
            dProvider.WriteDB(dList, 2);
            string newFirstName = "Newfirstname";
            int input = 1;
            // Act
            Student result = studentService.ChangeInfo(student, newFirstName, input);
            // Assert
            Assert.AreEqual(newFirstName, result.FirstName);
        }
        [TestMethod]
        public void ChangeInfo_ChangeStudentLastName_SuccessfullyChanged()
        {
            // Arrange
            Student student = new Student("John", "Doe", "12345", "Group1");
            Document document = new Document("OldName", "OldAuthor", student);
            student.AddDocument(document);
            List<Student> sList = new List<Student> { student };
            List<Document> dList = new List<Document> { document };
            sProvider.WriteDB(sList, 1);
            dProvider.WriteDB(dList, 2);
            string newLastName = "Newlastname";
            int input = 2;
            // Act
            Student result = studentService.ChangeInfo(student, newLastName, input);
            // Assert
            Assert.AreEqual(newLastName, result.LastName);
        }

    [TestMethod]
    public void ChangeInfo_ChangeStudentStudentCard_SuccessfullyChanged()
    {
        // Arrange
        Student student = new Student("John", "Doe", "12345", "Group1");
        Document document = new Document("OldName", "OldAuthor", student);
        student.AddDocument(document);
        List<Student> sList = new List<Student> { student };
        List<Document> dList = new List<Document> { document };
        sProvider.WriteDB(sList, 1);
        dProvider.WriteDB(dList, 2);
        string newStudentCard = "KB12345678";
        int input = 3;
        // Act
        Student result = studentService.ChangeInfo(student, newStudentCard, input);
        // Assert
        Assert.AreEqual(newStudentCard, result.StudentCard);
    }
    [TestMethod]
    public void ChangeInfo_ChangeStudentGroup_SuccessfullyChanged()
    {
        // Arrange
        Student student = new Student("John", "Doe", "12345", "Group1");
        Document document = new Document("OldName", "OldAuthor", student);
        student.AddDocument(document);
        List<Student> sList = new List<Student> { student };
        List<Document> dList = new List<Document> { document };
        sProvider.WriteDB(sList, 1);
        dProvider.WriteDB(dList, 2);
        string newGroup = "SE-224";
        int input = 4;
        // Act
        Student result = studentService.ChangeInfo(student, newGroup, input);
        // Assert
        Assert.AreEqual(newGroup, result.Group);
    }

    [TestMethod]
    public void ChangeInfo_InvalidInput_NoChange()
    {
        // Arrange
        Student student = new Student("John", "Doe", "12345", "Group1");
        Document document = new Document("OldName", "OldAuthor", student);
        student.AddDocument(document);
        List<Student> sList = new List<Student> { student };
        List<Document> dList = new List<Document> { document };
        sProvider.WriteDB(sList, 1);
        dProvider.WriteDB(dList, 2);
        string invalidInfo = "Invalidinfo";
        int input = 5; // Assuming 5 is an invalid input

        // Act
        Student result = studentService.ChangeInfo(student, invalidInfo, input);

        // Assert
        Assert.AreEqual(student.FirstName, result.FirstName);
        Assert.AreEqual(student.StudentCard, result.StudentCard);
    }
    }