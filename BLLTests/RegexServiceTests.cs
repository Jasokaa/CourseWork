namespace BLLTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
[TestClass]
    public class RegexServiceTests
    {
        private RegexService regexService;

        [TestInitialize]
        public void TestInitialize()
        {
            regexService = new RegexService();
        }

        [TestMethod]
        public void InputName_ValidFirstName_ReturnsTrue()
        {
            // Arrange
            string validFirstName = "John";
            // Act
            bool result = regexService.InputName(validFirstName);
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InputName_InvalidFirstName_ReturnsFalse()
        {
            // Arrange
            string invalidFirstName = "john123";
            // Act
            bool result = regexService.InputName(invalidFirstName);
            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InputStudentCard_ValidStudentCard_ReturnsTrue()
        {
            // Arrange
            string validStudentCard = "KB12345678";
            // Act
            bool result = regexService.InputStudentCard(validStudentCard);
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InputStudentCard_InvalidStudentCard_ReturnsFalse()
        {
            // Arrange
            string invalidStudentCard = "ABCD12345678";
            // Act
            bool result = regexService.InputStudentCard(invalidStudentCard);
            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InputGroup_ValidGroup_ReturnsTrue()
        {
            // Arrange
            string validGroup = "AB-123";
            // Act
            bool result = regexService.InputGroup(validGroup);
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InputGroup_InvalidGroup_ReturnsFalse()
        {
            // Arrange
            string invalidGroup = "abc123";
            // Act
            bool result = regexService.InputGroup(invalidGroup);
            // Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void InputDocumentName_ValidDocumentName_ReturnsTrue()
        {
            // Arrange
            string validDocumentName = "Document123";
            // Act
            bool result = regexService.InputDocumentName(validDocumentName);
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InputDocumentName_ValidDocumentNameWithSpaces_ReturnsTrue()
        {
            // Arrange
            string validDocumentNameWithSpaces = "Document 123";
            // Act
            bool result = regexService.InputDocumentName(validDocumentNameWithSpaces);
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InputDocumentName_InvalidDocumentNameWithSpecialCharacters_ReturnsFalse()
        {
            // Arrange
            string invalidDocumentName = "Document@123";
            // Act
            bool result = regexService.InputDocumentName(invalidDocumentName);
            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InputDocumentName_InvalidDocumentNameStartsWithLowercaseLetter_ReturnsFalse()
        {
            // Arrange
            string invalidDocumentName = "document123";
            // Act
            bool result = regexService.InputDocumentName(invalidDocumentName);
            // Assert
            Assert.IsFalse(result);
        }
        [TestMethod]
        public void InputAuthor_ValidAuthor_ReturnsTrue()
        {
            // Arrange
            string validAuthor = "John Doe";
            // Act
            bool result = regexService.InputAuthor(validAuthor);
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InputAuthor_InvalidAuthor_ReturnsFalse()
        {
            // Arrange
            string invalidAuthor = "123 Author";
            // Act
            bool result = regexService.InputAuthor(invalidAuthor);
            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InputIndex_ValidIndex_ReturnsTrue()
        {
            // Arrange
            string validIndex = "123";
            // Act
            bool result = regexService.InputIndex(validIndex);
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InputIndex_InvalidIndex_ReturnsFalse()
        {
            // Arrange
            string invalidIndex = "abc";
            // Act
            bool result = regexService.InputIndex(invalidIndex);
            // Assert
            Assert.IsFalse(result);
        }
    }