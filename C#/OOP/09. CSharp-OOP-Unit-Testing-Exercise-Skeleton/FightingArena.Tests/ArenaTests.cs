using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class ArenaTests
    {
        private Arena arena;

        [SetUp]
        public void Setup()
        {
            this.arena = new Arena();
        }

        [Test]
        public void ConstructorShouldInitializeDependantValues()
        {
            Assert.IsNotNull(arena.Warriors);
        }

        [Test]
        public void EnrollShouldAddWarrior()
        {
            Warrior toEnroll = new Warrior("Malinka", 40, 160);

            this.arena.Enroll(toEnroll);

            Assert.AreEqual(1, arena.Count);
            Assert.That(arena.Warriors.Contains(toEnroll));
        }

        [Test]
        public void EnrollShouldThrowExceptionIfWarriorExists()
        {
            Warrior toEnroll = new Warrior("Malinka", 40, 160);

            this.arena.Enroll(toEnroll);

            Assert.Throws<InvalidOperationException>(() => arena.Enroll(toEnroll));
        }

        [Test]
        public void FightShouldThrowExceptionIfTheAttackerOrDeffenderIsNullOrBoth()
        {
            Warrior attacker = new Warrior("Denis", 100, 200);
            Warrior deffender = new Warrior("Pavel", 70, 140);
            arena.Enroll(attacker);
            arena.Enroll(deffender);

            Assert.Throws<InvalidOperationException>(() => arena.Fight("Denis", null));
            Assert.Throws<InvalidOperationException>(() => arena.Fight(null, "Pavel"));
            Assert.Throws<InvalidOperationException>(() => arena.Fight(null, null));
        }

        [Test]
        public void FightShouldThrowExceptionIfTheAttackerOrDeffenderIsNotEnrolledOrBoth()
        {
            Warrior attacker = new Warrior("Denis", 100, 200);
            Warrior deffender = new Warrior("Pavel", 70, 140);
            arena.Enroll(attacker);
            arena.Enroll(deffender);

            Assert.Throws<InvalidOperationException>(() => arena.Fight("Denis", "Kircho"));
            Assert.Throws<InvalidOperationException>(() => arena.Fight("NqkoiSi", "Pavel"));
        }

        [Test]
        public void FightShouldWorkAsExpected()
        {
            Warrior attacker = new Warrior("Denis", 100, 200);
            Warrior deffender = new Warrior("Pavel", 70, 140);
            arena.Enroll(attacker);
            arena.Enroll(deffender);

            arena.Fight("Denis", "Pavel");

            Assert.AreEqual(130, attacker.HP);
            Assert.AreEqual(40, deffender.HP);
        }
    }
}
