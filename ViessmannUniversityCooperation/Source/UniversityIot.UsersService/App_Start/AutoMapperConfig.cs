using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using AutoMapper;
using UniversityIot.UsersDataAccess.Migrations;
using UniversityIot.UsersDataAccess.Models;
using UniversityIot.UsersService.Models;

namespace UniversityIot.UsersService.App_Start
{
    public static class AutoMapperConfig
    {

        public static void RegisterMappings()
        {

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserGateway, UserGatewayViewModel>();
                cfg.CreateMap<User, UserViewModel>()
                    .ForMember(u => u.Password, opt => opt.Ignore())
                    .ForMember(u => u.UserGateways, opt => opt.MapFrom(
                        src => Mapper.Map<ICollection<UserGateway>, ICollection<UserGatewayViewModel>>(src.UserGateways)
                        ));
            });
        }
    }
}