namespace BLLTests;
using BLL;
using System;
using System.Collections.Generic;
[TestClass]
    public class ListServiceTests
    {
        private ListService<int> intListService;
        private List<int>? intList;

        [TestInitialize]
        public void TestInitialize()
        {
            intListService = new ListService<int>();
            intList = new List<int> { 1, 2, 3, 4, 5 };
        }

        [TestMethod]
        public void Add_ValidInput_ReturnsListWithAddedElement()
        {
            // Arrange
            int elementToAdd = 6;
            // Act
            List<int>? result = intListService.Add(intList, elementToAdd);
            // Assert
            CollectionAssert.Contains(result, elementToAdd);
        }

        [TestMethod]
        public void GetIndex_ElementExists_ReturnsIndex()
        {
            // Arrange
            int elementToFind = 3;
            // Act
            int index = intListService.GetIndex(intList, elementToFind);
            // Assert
            Assert.AreEqual(2, index);
        }
        [TestMethod]
        public void GetIndex_NullList_ThrowsArgumentNullException()
        {
            // Arrange
            List<int>? nullList = null;
            int elementToFind = 3;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => intListService.GetIndex(nullList, elementToFind));
        }

        [TestMethod]
        public void GetIndex_ElementDoesNotExist_ReturnsMinusOne()
        {
            // Arrange
            int elementToFind = 6;
            // Act
            int index = intListService.GetIndex(intList, elementToFind);
            // Assert
            Assert.AreEqual(-1, index);
        }

        [TestMethod]
        public void DeleteByIndex_ValidIndex_ReturnsListWithElementRemoved()
        {
            // Arrange
            int indexToDelete = 2;
            // Act
            List<int> result = intListService.DeleteByIndex(intList, indexToDelete);
            // Assert
            CollectionAssert.DoesNotContain(result, 3);
        }

        [TestMethod]
        public void DeleteByIndex_IndexOutOfRange_ThrowsException()
        {
            // Arrange
            int indexToDelete = 10;
            // Act & Assert
            Assert.ThrowsException<Exception>(() => intListService.DeleteByIndex(intList, indexToDelete));
        }
        [TestMethod]
        public void DeleteByIndex_NullList_ThrowsException()
        {
            // Arrange
            List<int>? nullList = null;
            int indexToDelete = 2;

            // Act & Assert
            Assert.ThrowsException<Exception>(() => intListService.DeleteByIndex(nullList, indexToDelete));
        }

        [TestMethod]
        public void ChangeByIndex_ValidIndex_ReturnsListWithElementChanged()
        {
            // Arrange
            int indexToChange = 2;
            int newData = 99;
            // Act
            List<int>? result = intListService.ChangeByIndex(intList, newData, indexToChange);
            // Assert
            Assert.AreEqual(newData, result[indexToChange]);
        }

        [TestMethod]
        public void ChangeByIndex_IndexOutOfRange_ThrowsException()
        {
            // Arrange
            int indexToChange = 10;
            int newData = 99;
            // Act & Assert
            Assert.ThrowsException<Exception>(() => intListService.ChangeByIndex(intList, newData, indexToChange));
        }

        [TestMethod]
        public void GetByIndex_ValidIndex_ReturnsElement()
        {
            // Arrange
            int indexToGet = 3;
            // Act
            int result = intListService.GetByIndex(intList, indexToGet);
            // Assert
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void GetByIndex_IndexOutOfRange_ThrowsException()
        {
            // Arrange
            int indexToGet = 10;
            // Act & Assert
            Assert.ThrowsException<Exception>(() => intListService.GetByIndex(intList, indexToGet));
        }
    }