using System;

using NUnit.Framework;

[TestFixture]
public class AxeTests
{
    [Test]
    public void AxeLoosesDurabilityAfterAttack()
    {
        // Arrange

        Axe axe = new Axe(10, 10);
        Dummy dummy = new Dummy(10, 10);

        // Act

        axe.Attack(dummy);

        // Assert

        int expectedPoints = 9;
        Assert.AreEqual(expectedPoints, axe.DurabilityPoints, "Axe Durabillity Points does not change after attack");
    }

    [Test]
    public void AttackingWithBrokenWeaponShouldThrowException()
    {
        Axe axe = new Axe(10, 0);
        Dummy dummy = new Dummy(10, 10);

        Assert.Throws<InvalidOperationException>(() => axe.Attack(dummy));
    }
}