using System;
using AutoMapper;
using UniversityIot.Enums;

namespace UniversityIot.UsersService.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using UniversityIot.UsersDataAccess.Models;
    using UniversityIot.UsersDataService;
    using UniversityIot.UsersService.Helpers;
    using UniversityIot.UsersService.Models;

    [RoutePrefix("users")]
    public class UsersController : ApiController
    {
        private readonly IUsersDataService usersDataService;
     

        public UsersController(IUsersDataService usersDataService)
        {
            this.usersDataService = usersDataService;
          
        }
       
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var user = await usersDataService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<UserViewModel>(user));
        }

        [Route("")]
        public async Task<IHttpActionResult> Post(AddUserViewModel userVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addedUser = await usersDataService.AddUserAsync(new User()
            {
                Name = userVM.Name,
                CustomerNumber = userVM.CustomerNumber,
                Password = userVM.Password
            });
            var userWhichWasAdded = await usersDataService.GetUserAsync(addedUser.Id);
            return Ok(Mapper.Map<UserViewModel>(userWhichWasAdded));
        }

        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var user = await usersDataService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                await usersDataService.DeleteUserAsync(id);
                return Ok();
            }
        }

        [Route("{id}")]
        public async Task<IHttpActionResult> Put(int id, EditUserViewModel userVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userToEdit = await usersDataService.GetUserAsync(id);
            if (userToEdit == null)
            {
                return NotFound();
            }
            userToEdit.CustomerNumber = userVM.CustomerNumber;
            var userUpdated = await usersDataService.UpdateUserAsync(userToEdit);
            return Ok(Mapper.Map<UserViewModel>(userUpdated));
        }


        private static UserViewModel MapUser(User user)
        {
            var userVM = new UserViewModel()
            {
                CustomerNumber = user.CustomerNumber,
                Id = user.Id,
                Name = user.Name,
                Password = user.Password
            };

            foreach (var userGateway in user.UserGateways)
            {
                userVM.UserGateways.Add(new UserGatewayViewModel()
                {
                    GatewaySerial = userGateway.GatewaySerial,
                    Id = userGateway.Id,
                    AccessType = userGateway.AccessType.ToString()
                });
            }

            return userVM;
        }
    }
}