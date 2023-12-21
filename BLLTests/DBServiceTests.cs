namespace BLLTests;
using BLL;
using DAL;
    [TestClass]
    public class DBServiceTests
    {
        private DBService<Student> sProvider;
        private DBService<Document> dProvider;
        private List<Student>? students;

        [TestInitialize]
        public void TestInitialize()
        {
            sProvider = new DBService<Student>();
            dProvider = new DBService<Document>();
            students = new List<Student>
            {
                new Student("John", "Doe", "12345", "Group1"),
                new Student("Jane", "Doe", "67890", "Group2")
            };
        }
        
        [TestMethod]
        public void WriteDB_Students_SuccessfulWrite()
        {
            // Arrange
            // Act
            sProvider.WriteDB(students, 1);
            List<Student> readStudents = sProvider.ReadDB(1);
            // Assert
            CollectionAssert.AreEqual(students, readStudents);
        }

        [TestMethod]
        public void ReadDB_Students_SuccessfulRead()
        {
            // Arrange
            sProvider.WriteDB(students, 1);

            // Act
            List<Student> readStudents = sProvider.ReadDB(1);

            // Assert
            CollectionAssert.AreEqual(students, readStudents);
        }

        [TestMethod]
        public void DeleteAllFromFile_Students_SuccessfulDelete()
        {
            // Arrange
            sProvider.WriteDB(students, 1);
            // Act
            sProvider.DeleteAllFromFile(1);
            List<Student> readStudents = sProvider.ReadDB(1);

            // Assert
            Assert.IsNull(readStudents);
        }
        
        [TestMethod]
        public void ReadDB_Documents_ReturnsNull()
        {
            // Arrange
            dProvider.DeleteAllFromFile(2);
            dProvider.WriteDB(null, 2);
            // Act
            List<Document>? readDocuments = dProvider.ReadDB(2);
            // Assert
            Assert.IsNull(readDocuments);
        }
    }