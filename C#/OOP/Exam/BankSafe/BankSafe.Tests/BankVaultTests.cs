using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BankSafe.Tests
{
    public class BankVaultTests
    {
        private Item item;
        private BankVault vault;

        [SetUp]
        public void Setup()
        {
            this.item = new Item("testOwner", "123");
            this.vault = new BankVault();
        }

        [Test]
        public void ItemConstructorShouldWorkProperly()
        {
            Assert.AreEqual("testOwner", item.Owner);
            Assert.AreEqual("123", item.ItemId);
        }

        [Test]
        public void BankVaultConstructorShouldWorkCorrectly()
        {
            Assert.IsNotNull(vault.VaultCells);
        }

        [Test]
        public void VaultCellsShouldReturnTheVaultCells()
        {

            var testvaultCells = new Dictionary<string, Item>
            {
                {"A1", null},
                {"A2", null},
                {"A3", null},
                {"A4", null},
                {"B1", null},
                {"B2", null},
                {"B3", null},
                {"B4", null},
                {"C1", null},
                {"C2", null},
                {"C3", null},
                {"C4", null},
            };

            CollectionAssert.AreEqual(testvaultCells, this.vault.VaultCells);
        }

        [Test]
        public void AddItemShouldAddItemToACell()
        {
            this.vault.AddItem("A1", item);

            Assert.AreEqual(item.ItemId, this.vault.VaultCells["A1"].ItemId);
            Assert.AreEqual(item.Owner, this.vault.VaultCells["A1"].Owner);
        }

        [Test]
        public void AddItemShouldReturnMessage()
        {
            Assert.AreEqual($"Item:{item.ItemId} saved successfully!", this.vault.AddItem("A1", item));
        }

        [Test]
        public void AddItemShouldThrowExceptionIfGivenCellIsInvalid()
        {
            Assert.Throws<ArgumentException>(() => this.vault.AddItem("InvalidCell", item));
        }

        [Test]
        public void AddItemShouldThrowExceptionIfGivenCellIsAlreadyTaken()
        {
            var testItem = new Item("Owner2", "testID");

            this.vault.AddItem("A1", item);

            Assert.Throws<ArgumentException>(() => this.vault.AddItem("A1", testItem));
        }

        [Test]
        public void AddItemShouldThrowExceptionIfItemIsAlreadyInTheVault()
        {
            this.vault.AddItem("A1", item);

            Assert.Throws<InvalidOperationException>(() => this.vault.AddItem("A2", item));
        }

        [Test]
        public void RemoveItemShouldRemoveItemFormACell()
        {
            this.vault.AddItem("A1", item);

            this.vault.RemoveItem("A1", item);

            Assert.IsNull(this.vault.VaultCells["A1"]);
        }

        [Test]
        public void RemoveItemShouldReturnString()
        {
            this.vault.AddItem("A1", item);

            Assert.AreEqual($"Remove item:{item.ItemId} successfully!", this.vault.RemoveItem("A1", item));
        }

        [Test]
        public void RemoveItemShouldThrowExceptionIfGivenCellIsInvalid()
        {
            Assert.Throws<ArgumentException>(() => this.vault.RemoveItem("InvalidCell", item));
        }

        [Test]
        public void RemoveItemShouldThrowExceptionIfThereISNoSuchItemInTheGivenCell()
        {
            Assert.Throws<ArgumentException>(() => this.vault.RemoveItem("A1", item));
        }
    }
}