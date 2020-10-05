using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLibrary.Models;

namespace BlazorServerDemoApp.ViewModels
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DoctorViewModel, DoctorModel>();
            CreateMap<DoctorModel, DoctorViewModel>();
        }
    }
}
