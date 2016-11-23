using System.Linq;
using UniversityIot.VitocontrolApi.Enums;
using UniversityIot.VitocontrolApi.Exceptions;
using UniversityIot.VitocontrolApi.Models;

namespace UniversityIot.VitocontrolApi.Services
{
    public class GatewayService
    {
        private IDataService _dataService;

        public GatewayService(IDataService dataService)
        {
            _dataService = dataService;
        }
        public Gateway RegisterGateway(string serialNumber)
        {
            if (serialNumber.Length != 16 || !serialNumber.All(char.IsDigit))
            {
                throw new IllegallSerialNumberException();
            }
            if (_dataService.Gateways.FirstOrDefault(x => x.SerialNumber == serialNumber) != null)
            {
                throw new GatewayExistsException();
            }
            var gateway = new Gateway() {SerialNumber = serialNumber, User = _dataService.Users.FirstOrDefault(), Status = GatewayStatus.Registered};
            _dataService.Gateways.Add(gateway);
            return gateway;
        }

        public Gateway UnregisterGateway(string serialNumber)
        {
            var gateway = _dataService.Gateways.FirstOrDefault(x => x.SerialNumber == serialNumber);
            if (gateway == null)
            {
                throw new GatewayNotFoundException();
            }
            if (gateway.Status == GatewayStatus.Unregistered)
            {
                throw new AlreadyUnregisteredGatewayException();
            }
            gateway.User = null;
            gateway.Status = GatewayStatus.Unregistered;
            return gateway;
        }
    }
}