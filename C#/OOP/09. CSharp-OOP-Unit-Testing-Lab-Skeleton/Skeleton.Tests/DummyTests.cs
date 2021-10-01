using NUnit.Framework;
using System;

[TestFixture]
public class DummyTests
{
    [Test]
    public void DummyLoosesHealthIfAttacked()
    {
        Axe axe = new Axe(10, 10);
        Dummy dummy = new Dummy(100, 10);

        axe.Attack(dummy);

        int expectedHealth = 90;

        Assert.AreEqual(expectedHealth, dummy.Health);
    }

    [Test]
    public void DeadDummyThowsWxceptionIfAttacked()
    {
        Axe axe = new Axe(10, 10);
        Dummy dummy = new Dummy(0, 10);

        Assert.Throws<InvalidOperationException>(() => axe.Attack(dummy));
    }

    [Test]
    public void DeadDummyCanGiveXP()
    {
        Dummy dummy = new Dummy(0, 10);

        int expectedResult = 10;

        Assert.AreEqual(expectedResult, dummy.GiveExperience());
    }

    [Test]
    public void AliveDummyCantGiveXP()
    {
        Dummy dummy = new Dummy(10, 10);

        Assert.Throws<InvalidOperationException>(() => dummy.GiveExperience());
    }
}
