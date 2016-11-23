using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityIot.VitocontrolApi.Exceptions;
using UniversityIot.VitocontrolApi.Models;

namespace UniversityIot.VitocontrolApi.Services
{
    public class UserService
    {
        private readonly IDataService _dataService;

        public UserService(IDataService dataService)
        {
            _dataService = dataService;
        }
        public User GetUser(string name)
        {
            var user = _dataService.Users.FirstOrDefault(x => x.Name == name);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            return user;
        }

        public User CreateUser(string name)
        {
            if (_dataService.Users.FirstOrDefault(x => x.Name == name) != null)
            {
                throw new DuplicateNameOfUserException();
            }
            var user = new User() {Name = name};
            _dataService.Users.Add(user);
            return user;
        }

        public User DeleteUser(string name)
        {
            var user = _dataService.Users.FirstOrDefault(x => x.Name == name);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            _dataService.Users.Remove(user);
            return user;
        }
    }

}