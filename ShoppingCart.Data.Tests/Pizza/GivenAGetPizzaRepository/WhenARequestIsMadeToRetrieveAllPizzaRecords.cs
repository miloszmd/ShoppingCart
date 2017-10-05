﻿using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Pizza;

namespace ShoppingCart.Data.Tests.Pizza.GivenAGetPizzaRepository
{
    [TestFixture]
    class WhenARequestIsMadeToRetrieveAllPizzaRecords
    {
        private List<PizzaRecord> _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Select<PizzaRecord>("pizzas")).Returns(new List<PizzaRecord>
            {
                new PizzaRecord
                {
                    Identifier = 1,
                    Name = "Original"
                },
                new PizzaRecord
                {
                    Identifier = 2,
                    Name = "Gimme the Meat"
                },
            });

            var subject = new GetGetPizzaRepository(database.Object);
            _result = subject.Get();
        }

        [Test]
        public void ThenAllPizzaRecordsAreReturned()
        {
            Assert.That(_result.Count, Is.EqualTo(2));
        }

        [TestCase(0, 1)]
        [TestCase(1, 2)]
        public void ThenThePizzaRecordIdentifierIsMappedCorrectly(int index, int identifier)
        {
            Assert.That(_result[index].Identifier, Is.EqualTo(identifier));
        }

        [TestCase(0, "Original")]
        [TestCase(1, "Gimme the Meat")]
        public void ThenThePizzaRecordNameIsMappedCorrectly(int index, string name)
        {
            Assert.That(_result[index].Name, Is.EqualTo(name));
        }
    }
}
