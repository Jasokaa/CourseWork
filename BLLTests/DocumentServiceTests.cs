namespace BLLTests;
using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
[TestClass]
    public class DocumentServiceTests
    {
        private DocumentService? documentService;
        private List<Document?>? documents;
        private DBService<Student> sProvider;
        private DBService<Document> dProvider;

        [TestInitialize]
        public void TestInitialize()
        {
            documentService = new DocumentService();
            documents = new List<Document?>
            {
                new Document("Document1", "Author1"),
                new Document("Document3", "Author3"),
                new Document("Document2", "Author2")
            };
            sProvider = new DBService<Student>();
            dProvider = new DBService<Document>();
        }

        [TestMethod]
        public void SortDocumentList_SortByName_ReturnsSortedByName()
        {
            // Arrange
            // Act
            List<Document?>? sortedDocuments = documentService.SortDocumentList(documents, 1);

            // Assert
            CollectionAssert.AreEqual(
                documents.OrderBy(doc => doc.Name).ToList(),
                sortedDocuments
            );
        }

        [TestMethod]
        public void SortDocumentList_SortByAuthor_ReturnsSortedByAuthor()
        {
            // Arrange
            // Act
            List<Document?>? sortedDocuments = documentService.SortDocumentList(documents, 2);

            // Assert
            CollectionAssert.AreEqual(
                documents.OrderBy(doc => doc.Author).ToList(),
                sortedDocuments
            );
        }


        [TestMethod]
        public void SearchDocuments_ListIsNull_ThrowsException()
        {
            // Arrange
            List<Document>? documentsNull = null;
            string inputName = "Document1";

            // Act & Assert
            Assert.ThrowsException<Exception>(() => documentService.SearchDocument(documentsNull, inputName));
        }
        [TestMethod]
        public void CheckName_CaseSensitiveComparison_ReturnsTrue()
        {
            // Arrange
            string inputName1 = "Document1";
            string inputName2 = "doc";
            // Act & Assert
            Assert.IsTrue(documentService.CheckName(documents, inputName1));
            Assert.IsFalse(documentService.CheckName(documents, inputName2));
        }
        [TestMethod]
        public void SearchDocuments_ReturnsMatchingDocuments()
        {
            // Arrange
            string inputName1 = "Document1";
            string inputName2 = "Doc";
            // Act
            Document result1 = documentService.SearchDocument(documents, inputName1);
            Document result2 = documentService.SearchDocument(documents, inputName2);
            // Assert
            Assert.IsNotNull(result1);
            Assert.AreEqual(inputName1, result1?.Name);
            Assert.IsNull(result2);
        }
        [TestMethod]
        public void SortDocumentList_InvalidSortOption_ReturnsOriginalList()
        {
            // Arrange
            // Act
            List<Document?>? sortedDocuments = documentService.SortDocumentList(documents, 3);

            // Assert
            CollectionAssert.AreEqual(null, sortedDocuments);
        }
        [TestMethod]
        public void CheckName_NameExists_ReturnsTrue()
        {
            // Arrange
            string inputName = "Document1";
            // Act
            bool result = documentService.CheckName(documents, inputName);
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckName_NameDoesNotExist_ReturnsFalse()
        {
            // Arrange
            string inputName = "NonExistingDocument";
            // Act
            bool result = documentService.CheckName(documents, inputName);
            // Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void ChangeInfo_ChangeDocumentName_SuccessfullyChanged()
        {
            // Arrange
            Student student = new Student("John", "Doe", "12345", "Group1");
            Document document = new Document("OldName", "OldAuthor", student);
            student.AddDocument(document);
            List<Student> sList = new List<Student>();
            sList.Add(student);
            List<Document> dList = new List<Document>();
            sProvider.WriteDB(sList, 1);
            dProvider.WriteDB(dList, 2);
            string newName = "NewName";
            int input = 1;
            // Act
            Document result = documentService.ChangeInfo(document, newName, input);
            // Assert
            Assert.AreEqual(newName, result.Name);
        }

        [TestMethod]
        public void ChangeInfo_ChangeDocumentAuthor_SuccessfullyChanged()
        {
            // Arrange
            Student student = new Student("John", "Doe", "12345", "Group1");
            Document document = new Document("OldName", "OldAuthor", student);
            student.AddDocument(document);
            List<Student> sList = new List<Student>();
            sList.Add(student);
            List<Document> dList = new List<Document>();
            sProvider.WriteDB(sList, 1);
            dProvider.WriteDB(dList, 2);
            string newAuthor = "New Author";
            int input = 2;

            // Act
            Document result = documentService.ChangeInfo(document, newAuthor, input);

            // Assert
            Assert.AreEqual(newAuthor, result.Author);
        }

        [TestMethod]
        public void ChangeInfo_InvalidInput_NoChange()
        {
            // Arrange
            Student student = new Student("John", "Doe", "12345", "Group1");
            Document document = new Document("OldName", "OldAuthor", student);
            student.AddDocument(document);
            List<Student> sList = new List<Student>();
            sList.Add(student);
            List<Document> dList = new List<Document>();
            sProvider.WriteDB(sList, 1);
            dProvider.WriteDB(dList, 2);
            string invalidInfo = "InvalidInfo";
            int input = 3; // Assuming 3 is an invalid input

            // Act
            var result = documentService.ChangeInfo(document, invalidInfo, input);

            // Assert
            Assert.AreEqual(document.Name, result.Name);
            Assert.AreEqual(document.Author, result.Author);
        }
    }