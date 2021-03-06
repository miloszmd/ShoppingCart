﻿using System;
using Moq;
using NUnit.Framework;
using ShoppingCart.Data.Database;
using ShoppingCart.Data.Size;

namespace ShoppingCart.Data.Tests.Size.GivenAGetSizeRepository
{
    [TestFixture]
    public class WhenTheDatabaseFailsToRetrieveTheSizeRecords
    {
        private GetSizesResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.Query<SizeRecord>()).Throws<Exception>();

            var subject = new SizeRepository(database.Object);
            _result = subject.GetAll();
        }

        [Test]
        public void ThenAnEmptyListOfPizzaRecordsIsReturned()
        {
            Assert.That(_result.Sizes.Count, Is.Zero);
        }

        [Test]
        public void ThenAnErrorIsReturned()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenAnErrorMessageIsReturned()
        {
            Assert.That(_result.Error.UserMessage,
                Is.EqualTo("Something went wrong when retrieving SizeRecords from database."));
        }
    }
}