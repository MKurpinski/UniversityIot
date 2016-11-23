using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using UniversityIot.VitocontrolApi.Exceptions;
using UniversityIot.VitocontrolApi.Models;
using UniversityIot.VitocontrolApi.Services;

namespace UniversityIot.VitocontrolApi.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public void GetUser__Should_Return_User_When_User_Exists()
        {
            //arrange
            var userName = "Kowalski";
            var dataService = new Mock<IDataService>();
            var actual = new User() { Name = userName };
            dataService.Setup(m => m.Users).Returns(new List<User>() {actual});
            var userService = new UserService(dataService.Object);

            //act
            var current = userService.GetUser(userName);
            
            //assert
            Assert.AreEqual(actual,current);
        }
        [Test]
        public void GetUser_Should_Fail_When_User_Does_Not_Exist()
        {
            //arrange
            var userName = "Kowalski";
            var dataService = new Mock<IDataService>();
            dataService.Setup(m => m.Users).Returns(new List<User>());
            var userService = new UserService(dataService.Object);

            //assert
            Assert.Throws<UserNotFoundException>(()=> userService.GetUser(userName));
        }

        [Test]
        public void CreateUser_Should_Not_Allow_To_Create_User_With_Existing_Name()
        {
            //arrange
            var userName = "Kowalski";
            var actual = new User() { Name = userName };
            var dataService = new Mock<IDataService>();
            dataService.Setup(m => m.Users).Returns(new List<User>() {actual});
            var userService = new UserService(dataService.Object);
            //assert
            Assert.Throws<DuplicateNameOfUserException>(() => userService.CreateUser(userName));
        }

        [Test]
        public void CreateUser_Should_CreateUser()
        {
            //arrange
            var userName = "Kowalski";
            var dataService = new Mock<IDataService>();
            dataService.Setup(m => m.Users).Returns(new List<User>() {});
            var userService = new UserService(dataService.Object);
            //act
            var current = userService.CreateUser(userName);
            //assert
            Assert.IsNotNull(current);
        }
        [Test]
        public void DeleteUser_Should_Delete_User()
        {
            //arrange
            var userName = "Kowalski";
            var actual = new User() {Name = userName};
            var dataService = new Mock<IDataService>();
            dataService.Setup(m => m.Users).Returns(new List<User>() {actual });
            var userService = new UserService(dataService.Object);
            //act
            var current = userService.DeleteUser(userName);
            //assert
            Assert.AreEqual(actual,current);
        }
        [Test]
        public void DeleteUser_Should_Fail_When_User_Does_Not_Exists()
        {
            //arrange
            var userName = "Kowalski";
            var dataService = new Mock<IDataService>();
            dataService.Setup(m => m.Users).Returns(new List<User>());
            var userService = new UserService(dataService.Object);
            //assert
            Assert.Throws<UserNotFoundException>(() => userService.DeleteUser(userName));
        }
    }
}
