using ExtendedDatabase;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    public class ExtendedDatabaseTests
    {
        private Database database;

        [SetUp]
        public void Setup()
        {
            Person[] people = new Person[]
            {
                new Person(1, "Gosho"),
                new Person(2, "Pesho"),
                new Person(3, "Denis"),
                new Person(4, "Kircho")
            };

            this.database = new Database(people);
        }

        [Test]
        public void ConstructorShouldInitializeCollection()
        {
            Person[] people = new Person[]
            {
                new Person(1, "Gosho"),
                new Person(2, "Pesho"),
                new Person(3, "Denis"),
                new Person(4, "Kircho")
            };

            this.database = new Database(people);

            var expectedResult = 4;
            var databasePersons = new List<Person>();

            for (int i = 1; i <= 4; i++)
            {
                databasePersons.Add(database.FindById(i));
            }

            CollectionAssert.AreEqual(people, databasePersons.ToArray());

            Assert.AreEqual(expectedResult, this.database.Count);
        }

        [Test]
        public void ConstructorShouldThrowExceptionIfGetsMoreThan16Element()
        {
            List<Person> people = new List<Person>();

            for (int i = 0; i <= 16; i++)
            {
                people.Add(new Person(i, $"Kircho{i}"));
            }

            Assert.Throws<ArgumentException>(() => new Database(people.ToArray()));
        }

        [Test]
        public void AddShouldAddPerson()
        {
            Person toAdd = new Person(5, "Dragoicho");
            database.Add(toAdd);

            Assert.AreEqual(toAdd, database.FindById(toAdd.Id));
            Assert.AreEqual(5, database.Count);
        }

        [Test]
        public void AddShouldThrowAnExceptionIfTryToAddMoreThan16People()
        {
            this.database = new Database();

            for (int i = 0; i <= 15; i++)
            {
                database.Add(new Person(i, $"Gosho{i}"));
            }

            Assert.Throws<InvalidOperationException>(() => this.database.Add(new Person(17, "Kircho")));
        }

        [Test]
        public void AddShouldThrowAnExceptionIfAddPersonWithSameId()
        {
            Assert.Throws<InvalidOperationException>(() => this.database.Add(new Person(4, "Kircho4")));
        }

        [Test]
        public void AddShouldThrowAnExceptionIfAddPersonWithSameUsername()
        {
            Assert.Throws<InvalidOperationException>(() => this.database.Add(new Person(5, "Kircho")));
        }

        [Test]
        public void RemoveShouldRemoveTheLastElement()
        {
            database.Remove();

            Assert.That(() => database.FindById(4), Throws.InvalidOperationException);

            Assert.AreEqual(3, database.Count);
        }

        [Test]
        public void RemoveOperationShouldThrowExceptionIfDatabaseIsEmpty()
        {
            Assert.Throws<InvalidOperationException>(() => new Database().Remove());
        }

        [Test]
        public void FindByUsernameShouldWorkiCorrectly()
        {
            Person person = new Person(5, "Pavkata");
            database.Add(person);

            Assert.AreEqual(person, database.FindByUsername(person.UserName));
        }

        [Test]
        public void FindByUsernameShoulThrowAnExceptionIfPersonIsMissing()
        {
            Assert.Throws<InvalidOperationException>(() => database.FindByUsername("alabala"));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void FindByUsernameShouldThrowAnExceptionIfUsernameIsNullOrEmpty(string username)
        {
            Assert.Throws<ArgumentNullException>(() => database.FindByUsername(username));
        }

        [Test]
        public void ArgumentsAreCaseSensitive()
        {
            Assert.Throws<InvalidOperationException>(() => database.FindByUsername("kircho"));
        }

        [Test]
        public void FindByIdShouldWorkCorrectly()
        {
            Person person = new Person(5, "Pavkata");
            database.Add(person);

            Assert.AreEqual(person, database.FindById(person.Id));
        }

        public void FindByIdShoulThrowAnExceptionIfPersonIsMissing()
        {
            Assert.Throws<InvalidOperationException>(() => database.FindById(17));
        }

        public void FindByIdShoulThrowAnExceptionIfIdIsLessThanZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(-1));
        }
    }
}