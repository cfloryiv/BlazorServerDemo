using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using BlazorServerDemoApp.Pages.Order;
using BlazorServerDemoApp.ViewModels;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlazorServerDemoApp.Utilities
{
    public class DoctorUI
    {
        private readonly IDoctorData _doctorData;
        private readonly IMapper _mapper;

        public DoctorUI(IDoctorData doctorData, IMapper mapper)
        {
            _doctorData = doctorData;
            _mapper = mapper;
        }
        public List<UILine> Fields = new List<UILine>()
        {
            new UILine()
            {
                ID = "Name", LabelText = "Name: ", FieldType = "InputText", FieldName = "ViewModel.Name", ReportName = "record.Name"
            },
            new UILine()
            {
                ID = "Hospital", LabelText = "Hospital: ", FieldType = "InputText", FieldName = "ViewModel.Hospital", ReportName = "record.Hospital"
            },
            new UILine()
            {
                ID = "TelNo", LabelText = "Telephone#: ", FieldType = "InputText", FieldName = "ViewModel.TelNo", ReportName = "record.TelNo"
            },
            new UILine()
            {
                ID = "Speciality", LabelText = "Speciality: ", FieldType = "InputText", FieldName = "ViewModel.Speciality", ReportName = "record.Speciality"
            }
        };

        public string Path { get; } =
            @"C:\Users\curts new dell\source\repos\BlazorServerDemo\BlazorServerDemoApp\Pages\Doctor\";

        public void CreateTemplates()
        {
            var template = new List<string>();
            var template2 = new List<string>();
            var template3 = new List<string>();

            foreach (var uiline in Fields)
            {
                template.Add("<div class=\"form-group\">");
                template.Add($"<label class=\"control-label\">{uiline.LabelText}</label>");
                template.Add($"<{uiline.FieldType} @bind-Value=\"{uiline.FieldName}\" class=\"form-control\" />");
                template.Add($"<ValidationMessage For=\"() => {uiline.FieldName}\" />");
                template.Add($"</div>");

                template2.Add($"<th>{uiline.LabelText}</th>");

                template3.Add($"<td>@{uiline.ReportName}</td>");
            }

            var output = new List<string>();
            using (StreamReader sr = new StreamReader(Path + "UIList.razorx"))
            {
                
                while (sr.Peek() > 0)
                {
                    string line = sr.ReadLine();
                    string process=new string(line);

                    var str =process.Trim().Split(" ");
                    if (str[0] == "@*#")
                    {
                        var temp = new List<string>();
                        switch (str[1])
                        {
                            case "1": 
                                temp = template;
                                break;
                            case "2":
                                temp = template2;
                                break;
                            case "3":
                                temp = template3;
                                break;
                            default:
                                temp=new List<string>();
                                break;
                        }
                        foreach (var tline in temp)
                        {
                            output.Add(tline);
                        }
                    }
                    else
                    {
                        output.Add(line);
                    }
                    //Console.WriteLine(line);
                }
            }
            using (var sw = new StreamWriter(Path + "UIList.Razor"))
            {
                foreach (var line in output)
                {
                    sw.WriteLine(line);
                }

            }
        }

        public async Task<DoctorViewModel> Read(int ID)
        {
            DoctorModel Doctor = await _doctorData.GetDoctorById(ID);
            return _mapper.Map<DoctorViewModel>(Doctor);
        }
        public async Task Create(DoctorViewModel ViewModel, int ID)
        {
            DoctorModel Doctor = _mapper.Map<DoctorModel>(ViewModel);
            Doctor.ID = ID;
            if (ID == 0)
            {
                int id = await _doctorData.CreateDoctor(Doctor);
            }
            else
            {
                int id = await _doctorData.UpdateDoctor(Doctor);
            }
        }

        public void Delete(int ID)
        {
            _doctorData.DeleteDoctor(ID);
        }

        public async Task<List<DoctorModel>> GetList()
        {
            return await _doctorData.GetDoctors();
        }

        public DoctorViewModel Clear()
        {
            return new DoctorViewModel();
        }
    }

    public class UILine
    {
        public string ID { get; set; }
        public string LabelText { get; set; }
        public string FieldType { get; set; }
        public string FieldName { get; set; }
        public string ReportName { get; set; }
    }
}
