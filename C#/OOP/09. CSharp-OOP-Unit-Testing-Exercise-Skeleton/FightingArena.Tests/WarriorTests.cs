using NUnit.Framework;
using System;

namespace Tests
{
    public class WarriorTests
    {
        private Warrior warrior;

        [SetUp]
        public void Setup()
        {
            this.warrior = new Warrior("name", 100, 150);
        }

        [Test]
        public void WarriorConstructorShouldWorkCorrectly()
        {
            Assert.AreEqual("name", this.warrior.Name);
            Assert.AreEqual(100, this.warrior.Damage);
            Assert.AreEqual(150, this.warrior.HP);
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void ConstructorShouldThrowExceptionIfNameIsNullEmptyOrWhiteSpace(string name)
        {
            Assert.Throws<ArgumentException>(() => new Warrior(name, 100, 50));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void ConstructoeShouldThrowExceptionIfDamageIsZeroOrNegative(int damage)
        {
            Assert.Throws<ArgumentException>(() => new Warrior("name", damage, 50));
        }

        [Test]
        public void ConstructorShouldThrowExceptionIfHPIsNegative()
        {
            Assert.Throws<ArgumentException>(() => new Warrior("name", 40, -1));
        }

        [Test]
        public void AttackWorksCorrectly()
        {
            Warrior deffender = new Warrior("deffender", 100, 120);

            warrior.Attack(deffender);

            Assert.AreEqual(50, this.warrior.HP);
            Assert.AreEqual(20, deffender.HP);
        }

        [Test]
        public void AttackShouldMakeHealthZeroIfAttackIsGreaterThanHp()
        {
            Warrior deffender = new Warrior("deffender", 100, 80);

            warrior.Attack(deffender);

            Assert.AreEqual(0, deffender.HP);
        }

        [Test]
        public void WarriorsWithLessTHan30HPCannotAttack()
        {
            Warrior attacker = new Warrior("Kircho", 50, 20);

            Assert.Throws<InvalidOperationException>(() => attacker.Attack(this.warrior));
        }

        [Test]
        public void WarriorCannotAttackWarriorWithLessTHan30HP()
        {
            Warrior deffender = new Warrior("Kircho", 50, 20);

            Assert.Throws<InvalidOperationException>(() => warrior.Attack(deffender));
        }

        [Test]
        public void WarriorsCannotAttackStrongerEnemies()
        {
            Warrior deffender = new Warrior("stronger", 500, 150);

            Assert.Throws<InvalidOperationException>(() => warrior.Attack(deffender));
        }
    }
}