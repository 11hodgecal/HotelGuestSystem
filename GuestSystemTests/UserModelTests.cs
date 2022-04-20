using HotelGuestSystem.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestSystemTests
{
    public class UserModelTests
    {
        [Test]
        public async Task FullnameStringReturnsCorrectly()
        {
            //Arrange
            UserModel test = new UserModel();
            test.Fname = "test";
            test.Sname = "test";
            var expected = "test test";
            //Act
            var actual = test.Fullname();
            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
