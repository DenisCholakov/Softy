using NUnit.Framework;
using System;
using System.Linq;

namespace Tests
{
    public class DatabaseTests
    {
        private Database.Database database;

        [SetUp]
        public void Setup()
        {
            this.database = new Database.Database(Enumerable.Range(1, 10).ToArray());
        }

        [Test]
        public void ConstructorShouldBeInitializedWith16Elements()
        {
            var expectedResult = 10;

            Assert.AreEqual(expectedResult, this.database.Count);
        }

        [Test]
        public void AddShouldAddElementAtNextFreeCell()
        {
            int numToAdd = 5;
            database.Add(numToAdd);

            Assert.AreEqual(numToAdd, database.Fetch()[database.Count - 1]);
            Assert.AreEqual(11, database.Count);
        }

        [Test]
        public void AddShouldThrowAnExceptionIfTryToAddMoreThan16Elements()
        {
            this.database = new Database.Database(Enumerable.Range(1, 16).ToArray());

            Assert.Throws<InvalidOperationException>(() => this.database.Add(3));
        }

        [Test]
        public void RemoveShouldRemoveTheLastElement()
        {
            database.Remove();

            var expectedResult = 9;

            Assert.AreEqual(expectedResult, database.Fetch()[database.Count - 1]);
            Assert.AreEqual(9, database.Count);
        }

        [Test]
        public void RemoveOperationShouldThrowExceptionIdDatabaseIsEmpty()
        {
            Assert.Throws<InvalidOperationException>(() => new Database.Database().Remove());
        }

        [Test]
        public void FetchShouldReturnElementsAsArray()
        {
            var expectedResult = Enumerable.Range(1, 10).ToArray();

            CollectionAssert.AreEqual(expectedResult, database.Fetch());
        }


    }
}