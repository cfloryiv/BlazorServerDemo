using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLibrary.Data;

namespace BlazorServerDemoApp.Utilities
{
    public class CreateUIFiles
    {
        public void CreateTemplates(IDoctorData doctorData, IMapper mapper)
        {
            var ui = new DoctorUI(doctorData, mapper);
            ui.CreateTemplates();
        }
    }
}
