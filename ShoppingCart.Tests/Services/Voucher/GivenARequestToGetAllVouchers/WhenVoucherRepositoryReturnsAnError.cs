﻿using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ShoppingCart.Core.Communication;
using ShoppingCart.Core.Communication.ErrorCodes;
using ShoppingCart.Data.Delivery;
using ShoppingCart.Data.Size;
using ShoppingCart.Data.Voucher;
using ShoppingCart.Services.Voucher;
using GetAllVouchersResponse = ShoppingCart.Services.Voucher.GetAllVouchersResponse;

namespace ShoppingCart.Tests.Services.Voucher.GivenARequestToGetAllVouchers
{
    [TestFixture]
    public class WhenVoucherRepositoryReturnsAnError
    {
        private Mock<IVoucherRepository> _voucherRepository;
        private GetAllVouchersResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            _voucherRepository = new Mock<IVoucherRepository>();
            _voucherRepository.Setup(x => x.GetAllVouchers()).Returns(() => new Data.Voucher.GetAllVouchersResponse
            {
                HasError = true,
                Error = new Error
                {
                    Code = ErrorCodes.DatabaseError
                }
            });

            var subject = new VoucherService(_voucherRepository.Object);
            _result = subject.GetAll();
        }

        [Test]
        public void ThenVoucherRepositoryIsCalledCorrectly()
        {
            _voucherRepository.Verify(x => x.GetAllVouchers(), Times.Once);
        }

        [Test]
        public void ThenResponseHasAnError()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenTheCorrectErrorCodeIsReturned()
        {
            Assert.That(_result.Error.Code, Is.EqualTo(ErrorCodes.DatabaseError));
        }
    }
}