using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using PatientService.Api;
using PatientService.Domain.ApiModels;
using PatientService.Domain.Entities;
using PatientService.Domain.Manager;
using PatientService.Domain.Repository;
using System.Collections.Generic;
using Xunit;

namespace PatientService.UnitTest.Manager
{
    public class ManagerUnitTest
    {
        [Fact]
        public void GetAllPatients_ShouldReturnAllPatients_Valid()
        {
            var mock = new Mock<IPatientRepository>();
            var mockMapper = new Mock<IMapper>();
            var mockCache = new Mock<IMemoryCache>();

            mock.Setup(x => x.GetPatients()).ReturnsAsync(GetPatients());

            IManager manager = new ManagerImpl(mock.Object, mockMapper.Object, mockCache.Object);

            var result = manager.GetPatients();
            var patients = result.Result;

            Assert.Equal(patients.Count, GetPatients().Count);
        }

        public List<Patient> GetPatients()
        {
            return new List<Patient>()
            {
                new Patient {Id=1, Name="Roy", MobileNumber = "12345", BloodGroup = "O+", DateOfBirth = new System.DateTime(2020,1,1), Email = "roy@world.com", MedicalIssues = null},
                new Patient {Id=2, Name="Vince", MobileNumber = "67980", BloodGroup = "O-", DateOfBirth = new System.DateTime(2019,1,1), Email = "vince@world.com", MedicalIssues = null},
                new Patient {Id=3, Name="Root", MobileNumber = "24680", BloodGroup = "AB+", DateOfBirth = new System.DateTime(2018,1,1), Email = "root@world.com", MedicalIssues = null}
            };
        }
    }
}
