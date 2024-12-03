using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusServ.DTOs;
using lab7.Entities;

namespace BusServ.Mappings
{
    public class BusProfile : Profile
    {
        public BusProfile()
        {
            CreateMap<Bus, BusDto>().ReverseMap();
        }
    }
}