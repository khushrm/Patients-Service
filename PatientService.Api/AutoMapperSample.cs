using AutoMapper;
using PatientService.Domain.ApiModels;
using PatientService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientService.Api
{
    public class AutoMapperSample : Profile
    {
        public AutoMapperSample()
        {
            CreateMap<Patient, PatientApiModel>();
            CreateMap<PatientApiModel, Patient>();
            CreateMap<MedicalIssuesApiModel, MedicalIssue>();
            CreateMap<MedicalIssue, MedicalIssuesApiModel>();
        }
    }
}
