namespace BLLTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using DAL;
[TestClass]
    public class MethodsBLLTests
    {
        private MethodsBLL methodsBLL;
        private DBService<Student> sProvider;
        private DBService<Document?> dProvider;
        private List<Student>? students;
        private List<Document?>? documents;

        [TestInitialize]
        public void TestInitialize()
        {
            methodsBLL = new MethodsBLL();
            sProvider = new DBService<Student>();
            dProvider = new DBService<Document?>();
            students = new List<Student>
            {
                new Student("John", "Doe", "12345", "Group1"),
                new Student("Jane", "Doe", "67890", "Group2"),
                new Student("Alice", "Smith", "54321", "Group1")
            };
            documents = new List<Document>
            {
                new Document("Document1", "Author1"),
                new Document("Document3", "Author3"),
                new Document("Document2", "Author2")
            };
        }

        [TestMethod]
        public void TakeDocumentMethod_SuccessfulTake_ReturnsTrue()
        {
            // Arrange
            sProvider.DeleteAllFromFile(1);
            dProvider.DeleteAllFromFile(2);
            sProvider.WriteDB(students, 1);
            dProvider.WriteDB(documents,2);
            

            // Act
            bool result = methodsBLL.TakeDocumentMethod(students[1], documents[1]);

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TakeDocumentMethod_SuccessfulTake_ReturnsFalse()
        {
            // Arrange
            sProvider.DeleteAllFromFile(1);
            dProvider.DeleteAllFromFile(2);
            sProvider.WriteDB(students, 1);
            dProvider.WriteDB(documents,2);
            documents[1].Owner = students[0];
            // Act
            bool result = methodsBLL.TakeDocumentMethod(students[1], documents[1]);
            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ReturnDocumentMethod_SuccessfulReturn_ReturnsTrue()
        {
            // Arrange
            sProvider.DeleteAllFromFile(1);
            dProvider.DeleteAllFromFile(2);
            sProvider.WriteDB(students, 1);
            dProvider.WriteDB(documents,2);
            students[1].AddDocument(documents[1]);
            documents[1].Owner = students[1];
            // Act
            bool result = methodsBLL.ReturnDocumentMethod(students[1], documents[1]);
            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void ReturnDocumentMethod_SuccessfulReturn_ReturnsFalse()
        {
            // Arrange
            sProvider.DeleteAllFromFile(1);
            dProvider.DeleteAllFromFile(2);
            sProvider.WriteDB(students, 1);
            dProvider.WriteDB(documents,2);
            students[1].AddDocument(documents[1]);
            documents[1].Owner = null;
            // Act
            bool result = methodsBLL.ReturnDocumentMethod(students[1], documents[1]);
            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SortStudentListMethod_ValidInput_SortsList()
        {
            // Arrange
            int input = 1;
            sProvider.WriteDB(students, 1);
            // Act
            methodsBLL.SortStudentListMethod(input);

            // Assert
            List<Student>? sortedList = sProvider.ReadDB(1);
            CollectionAssert.AreEqual(sortedList, sortedList.OrderBy(s => s.FirstName).ToList());
        }

        [TestMethod]
        public void SortDocumentListMethod_ValidInput_SortsList()
        {
            // Arrange
            int input = 1;
            sProvider.WriteDB(students, 2);
            // Act
            methodsBLL.SortDocumentListMethod(input);

            // Assert
            List<Document?>? sortedList = dProvider.ReadDB(2);
            CollectionAssert.AreEqual(sortedList, sortedList.OrderBy(d => d?.Name).ToList());
        }

        [TestMethod]
        public void CreateStudent_SuccessfulCreation_UpdatesList()
        {
            // Arrange
            string fName = "John";
            string lName = "Doe";
            string sCard = "12345";
            string group = "Group1";

            // Act
            methodsBLL.CreateStudent(fName, lName, sCard, group);

            // Assert
            List<Student>? createdList = sProvider.ReadDB(1);
            Assert.IsNotNull(createdList);
            Assert.IsTrue(createdList.Any(s => s.FirstName == fName && s.LastName == lName && s.StudentCard == sCard && s.Group == group));
        }

        [TestMethod]
        public void CreateDocument_SuccessfulCreation_UpdatesList()
        {
            // Arrange
            string name = "Book1";
            string author = "Author1";

            // Act
            methodsBLL.CreateDocument(name, author);

            // Assert
            List<Document?>? createdList = dProvider.ReadDB(2);
            Assert.IsNotNull(createdList);
            Assert.IsTrue(createdList.Any(d => d?.Name == name && d?.Author == author));
        }
    }