using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using UniversityIot.VitocontrolApi.Enums;
using UniversityIot.VitocontrolApi.Exceptions;
using UniversityIot.VitocontrolApi.Models;
using UniversityIot.VitocontrolApi.Services;

namespace UniversityIot.VitocontrolApi.Tests
{
    [TestFixture]
    public class GatewayServiceTests
    {
        [Test]
        public void RegisterGateway_Should_Create_Gateway_With_New_Serial_Number()
        {
            //arrange
            var serialNumber = "1234567891234567";
            var dataService = new Mock<IDataService>();
            dataService.Setup(m => m.Gateways).Returns(new List<Gateway>());
            dataService.Setup(m => m.Users).Returns(new List<User>() { new User() });
            var gatewayService = new GatewayService(dataService.Object);
            //act
            var current = gatewayService.RegisterGateway(serialNumber);
            //assert
            Assert.IsNotNull(current);
        }
        [Test]
        public void RegisterGateway_Should_Connect_User_To_Gateway()
        {
            //arrange
            var serialNumber = "1234567891234567";
            var dataService = new Mock<IDataService>();
            dataService.Setup(m => m.Gateways).Returns(new List<Gateway>());
            dataService.Setup(m => m.Users).Returns(new List<User>() {new User()});
            var gatewayService = new GatewayService(dataService.Object);
            //act
            var current = gatewayService.RegisterGateway(serialNumber);
            //assert
            Assert.IsNotNull(current.User);
        }
        [Test]
        public void RegisterGateway_Should_Set_Status_To_Registered()
        {
            //arrange
            var serialNumber = "1234567891234567";
            var expected = GatewayStatus.Registered;
            var dataService = new Mock<IDataService>();
            dataService.Setup(m => m.Gateways).Returns(new List<Gateway>());
            dataService.Setup(m => m.Users).Returns(new List<User>() { new User() });
            var gatewayService = new GatewayService(dataService.Object);
            //act
            var current = gatewayService.RegisterGateway(serialNumber);
            //assert
            Assert.AreEqual(current.Status,expected);
        }

        [Test]
        public void RegisterGateway_Should_Not_Allow_To_Register_The_Gateway_Twice()
        {
            //arrange
            var serialNumber = "1234567891234567";
            var dataService = new Mock<IDataService>();
            dataService.Setup(m => m.Gateways).Returns(new List<Gateway>() {new Gateway() {SerialNumber = serialNumber}});
            dataService.Setup(m => m.Users).Returns(new List<User>() { new User() });
            var gatewayService = new GatewayService(dataService.Object);
            //assert
            Assert.Throws<GatewayExistsException>(() => gatewayService.RegisterGateway(serialNumber));
        }
        [Test]
        public void RegisterGateway_Should_Only_Allow_To_Register_Only_16_Long_Serial_Number()
        {
            //arrange
            var serialNumber = "123456";
            var dataService = new Mock<IDataService>();
            dataService.Setup(m => m.Gateways).Returns(new List<Gateway>());
            dataService.Setup(m => m.Users).Returns(new List<User>() { new User() });
            var gatewayService = new GatewayService(dataService.Object);
            //assert
            Assert.Throws<IllegallSerialNumberException>(() => gatewayService.RegisterGateway(serialNumber));
        }
        [Test]
        public void RegisterGateway_Should_Only_Allow_To_Register_Only_16_Long_Digits_Serial_Number()
        {
            //arrange
            var serialNumber = "123456789123456w";
            var dataService = new Mock<IDataService>();
            dataService.Setup(m => m.Gateways).Returns(new List<Gateway>());
            dataService.Setup(m => m.Users).Returns(new List<User>() { new User() });
            var gatewayService = new GatewayService(dataService.Object);
            //assert
            Assert.Throws<IllegallSerialNumberException>(() => gatewayService.RegisterGateway(serialNumber));
        }
        [Test]
        public void UnregisterGateway_Should_Disconnect_User_And_Gateway()
        {
            //arrange
            var serialNumber = "1234567891234561";
            var dataService = new Mock<IDataService>();
            dataService.Setup(m => m.Gateways).Returns(new List<Gateway>() {new Gateway() {SerialNumber = serialNumber,User = new User()} });
            var gatewayService = new GatewayService(dataService.Object);
            //act
            var result = gatewayService.UnregisterGateway(serialNumber);
            //assert
            Assert.IsNull(result.User);
        }
        [Test]
        public void UnregisterGateway_Should_Set_Gateway_Status_To_Unregistered()
        {
            //arrange
            var serialNumber = "1234567891234561";
            var dataService = new Mock<IDataService>();
            var expected = GatewayStatus.Unregistered;
            dataService.Setup(m => m.Gateways).Returns(new List<Gateway>() { new Gateway() { SerialNumber = serialNumber, User = new User() } });
            var gatewayService = new GatewayService(dataService.Object);
            //act
            var result = gatewayService.UnregisterGateway(serialNumber);
            //assert
            Assert.AreEqual(result.Status,expected);
        }
        [Test]
        public void UnregisterGateway_Should_Not_Allow_To_Unregister_Not_Existing_Gateway()
        {
            //arrange
            var serialNumber = "1234567891234561";
            var dataService = new Mock<IDataService>();
            dataService.Setup(m => m.Gateways).Returns(new List<Gateway>());
            var gatewayService = new GatewayService(dataService.Object);
            //assert
            Assert.Throws<GatewayNotFoundException>(() => gatewayService.UnregisterGateway(serialNumber));
        }
        [Test]
        public void UnregisterGateway_Should_Not_Allow_To_Unregister_Unregistered_Gateway()
        {
            //arrange
            var serialNumber = "1234567891234561";
            var dataService = new Mock<IDataService>();
            dataService.Setup(m => m.Gateways).Returns(new List<Gateway>() {new Gateway() {SerialNumber = serialNumber,User = new User(),Status = GatewayStatus.Unregistered} });
            var gatewayService = new GatewayService(dataService.Object);
            //assert
            Assert.Throws<AlreadyUnregisteredGatewayException>(() => gatewayService.UnregisterGateway(serialNumber));
        }

    }

}
