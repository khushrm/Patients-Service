using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using PatientService.Domain.ApiModels;
using PatientService.Domain.Entities;
using PatientService.Domain.Manager;
using PatientService.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PatientService.UnitTest.Manager
{
    public class ManagerUnitTest
    {
        
        public Mock<IPatientRepository> mock { get; set; } = new Mock<IPatientRepository>();
        public Mock<IMapper> mockMapper { get; set; } = new Mock<IMapper>();
        public Mock<IMemoryCache> mockCache { set; get; } = new Mock<IMemoryCache>();
        
        [Fact]
        public void GetAllPatients_ShouldReturnAllPatients_Valid()
        {
            mock.Setup(x => x.GetPatients()).ReturnsAsync(GetPatients());

            IManager manager = new ManagerImpl(mock.Object, mockMapper.Object, mockCache.Object);

            var result = manager.GetPatients();
            var patients = result.Result;

            Assert.Equal(patients.Count, GetPatients().Count);
        }
        [Fact]
        public void GetPatient_ShouldReturnPatientId_Valid()
        {
            var mockCache1 = new Mock<IMemoryCache>();
            var pat = GetPatients().Where(x => x.Id == 1).FirstOrDefault();
            var patApi = GetPatientsApiModel().Where(x => x.Id == 1).FirstOrDefault();
            mock.Setup(x => x.GetPatient(1)).ReturnsAsync(pat);
            mockMapper.Setup(x => x.Map<PatientApiModel>(pat)).Returns(patApi);

            IManager manager = new ManagerImpl(mock.Object, mockMapper.Object, mockCache1.Object);

            var result = manager.GetPatient(1);
            var patient = result.Result;

            Assert.Equal(patient.Id, pat.Id);
        }
        [Fact]
        public void GetPatient_ShouldReturnPatientName_Valid()
        {
            var pat = GetPatients().Where(x => x.Id == 1).FirstOrDefault();
            var patApi = GetPatientsApiModel().Where(x => x.Id == 1).FirstOrDefault();
            mock.Setup(x => x.GetPatient(1)).ReturnsAsync(pat);
            mockMapper.Setup(x => x.Map<PatientApiModel>(pat)).Returns(patApi);

            IManager manager = new ManagerImpl(mock.Object, mockMapper.Object, mockCache.Object);

            var result = manager.GetPatient(1);
            var patient = result.Result;

            Assert.Equal(patient.Name, pat.Name);
        }

        [Fact]
        public void GetPatient_ShouldReturnPatientEmail_Valid()
        {
            var mock = new Mock<IPatientRepository>();
            var mockMapper = new Mock<IMapper>();
            var mockCache = new Mock<IMemoryCache>();

                
            var pat = GetPatients().Where(x => x.Id == 1).FirstOrDefault();
            var patApi = GetPatientsApiModel().Where(x => x.Id == 1).FirstOrDefault();

            mock.Setup(x => x.GetPatient(1)).ReturnsAsync(pat);
            mockMapper.Setup(x => x.Map<PatientApiModel>(pat)).Returns(patApi);

            IManager manager = new ManagerImpl(mock.Object, mockMapper.Object, mockCache.Object);

            var result =  manager.GetPatient(1);
            var patient = result.Result;

            Assert.Equal(patient.Email, pat.Email);
        }
        [Fact]
        public void GetPatient_ShouldReturnPatientBloodGroup_Valid()
        {
            var pat = GetPatients().Where(x => x.Id == 1).FirstOrDefault();
            var patApi = GetPatientsApiModel().Where(x => x.Id == 1).FirstOrDefault();
            mock.Setup(x => x.GetPatient(1)).ReturnsAsync(pat);
            mockMapper.Setup(x => x.Map<PatientApiModel>(pat)).Returns(patApi);

            IManager manager = new ManagerImpl(mock.Object, mockMapper.Object, mockCache.Object);

            var result = manager.GetPatient(1);
            var patient = result.Result;

            Assert.Equal(patient.BloodGroup, pat.BloodGroup);
        }
        [Fact]
        public void GetPatient_ShouldReturnPatientDOB_Valid()
        {
            var pat = GetPatients().Where(x => x.Id == 1).FirstOrDefault();
            var patApi = GetPatientsApiModel().Where(x => x.Id == 1).FirstOrDefault();
            mock.Setup(x => x.GetPatient(1)).ReturnsAsync(pat);
            mockMapper.Setup(x => x.Map<PatientApiModel>(pat)).Returns(patApi);

            IManager manager = new ManagerImpl(mock.Object, mockMapper.Object, mockCache.Object);

            var result = manager.GetPatient(1);
            var patient = result.Result;

            Assert.Equal(patient.DateOfBirth, pat.DateOfBirth);
        }
        

        public List<Patient> GetPatients()
        {
            return new List<Patient>()
            {
                new Patient {Id=1, Name="Roy", MobileNumber = "1234567890", BloodGroup = "O+", DateOfBirth = new System.DateTime(2020,2,5), Email = "roy@world.com", MedicalIssues = null},
                new Patient {Id=2, Name="Vince", MobileNumber = "6798067890", BloodGroup = "O-", DateOfBirth = new System.DateTime(2019,1,1), Email = "vince@world.com", MedicalIssues = null},
                new Patient {Id=3, Name="Root", MobileNumber = "2468067890", BloodGroup = "AB+", DateOfBirth = new System.DateTime(2018,1,1), Email = "root@world.com", MedicalIssues = null}
            };
        }
        public List<PatientApiModel> GetPatientsApiModel()
        {
            return new List<PatientApiModel>()
            {
                new PatientApiModel {Id=1, Name="Roy", MobileNumber = "1234567890", BloodGroup = "O+", DateOfBirth = new System.DateTime(2020,2,5), Email = "roy@world.com", MedicalIssues = null},
                new PatientApiModel {Id=2, Name="Vince", MobileNumber = "6798067890", BloodGroup = "O-", DateOfBirth = new System.DateTime(2019,1,1), Email = "vince@world.com", MedicalIssues = null},
                new PatientApiModel {Id=3, Name="Root", MobileNumber = "2468067890", BloodGroup = "AB+", DateOfBirth = new System.DateTime(2018,1,1), Email = "root@world.com", MedicalIssues = null}
            };
        }
    }
}
