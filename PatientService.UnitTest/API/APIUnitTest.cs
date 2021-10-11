
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PatientService.Api.Controllers;
using PatientService.Domain.ApiModels;
using PatientService.Domain.Manager;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PatientService.UnitTest.API
{
    
    public class APIUnitTest
    {
        public Mock<IManager> mock { set; get; } = new Mock<IManager>();
        public Mock<ILogger> mockLogger { set; get; } = new Mock<ILogger>();
        [Fact]
        public void GetAllPatients_ShouldReturnAllPatients_Valid()
        {
            // Arrange
            
            mock.Setup(x => x.GetPatients()).ReturnsAsync(GetPatients());
            PatientsController pc = new PatientsController(mock.Object,mockLogger.Object);

            // Act
            var result = pc.GetAllData();
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result.Result);
            //var patients = Assert.IsType<ICollection<PatientApiModel>>(objectResult.Value);

            var patients = objectResult.Value as ICollection<PatientApiModel>;

            // Assert

            Assert.Equal(patients.Count, GetPatients().Count);
        }

        [Fact]
        public void GetPatientById_ShouldReturnPatient_ValidId()
        {
            mock.Setup(x => x.GetPatient(1)).ReturnsAsync(GetPatients().Where(x => x.Id == 1).FirstOrDefault());

            PatientsController pc = new PatientsController(mock.Object,mockLogger.Object);
            var result = pc.GetPatientById(1);

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result.Result);
            PatientApiModel patient = Assert.IsType<PatientApiModel>(objectResult.Value);

            Assert.Equal(1, patient.Id);
        }
        [Fact]
        public void GetPatientById_ShouldReturnPatient_ValidName()
        {
            mock.Setup(x => x.GetPatient(1)).ReturnsAsync(GetPatients().Where(x => x.Id == 1).FirstOrDefault());

            PatientsController pc = new PatientsController(mock.Object, mockLogger.Object);
            var result = pc.GetPatientById(1);

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result.Result);
            PatientApiModel patient = Assert.IsType<PatientApiModel>(objectResult.Value);

            Assert.Equal("Roy",patient.Name);
        }

        [Fact]
        public void GetPatientById_ShouldReturnPatient_ValidBloodGroup()
        {
            mock.Setup(x => x.GetPatient(1)).ReturnsAsync(GetPatients().Where(x => x.Id == 1).FirstOrDefault());

            PatientsController pc = new PatientsController(mock.Object, mockLogger.Object);
            var result = pc.GetPatientById(1);

            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result.Result);
            PatientApiModel patient = Assert.IsType<PatientApiModel>(objectResult.Value);

            Assert.Equal("O+", patient.BloodGroup);
        }

        [Fact]
        public void UpdatePatient_ShouldUpdatePatient_Valid()
        {
            var expectedPatientDetails = GetPatients().Where(x => x.Id == 1).FirstOrDefault();
            expectedPatientDetails.Name = "John";
            mock.Setup(x => x.EditPatient(1, expectedPatientDetails)).ReturnsAsync(expectedPatientDetails);

            PatientsController pc = new PatientsController(mock.Object, mockLogger.Object);
            var result =  pc.Put(1, expectedPatientDetails);
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(result.Result);
            PatientApiModel patient = Assert.IsType<PatientApiModel>(objectResult.Value);
            Assert.Equal(patient.Name, expectedPatientDetails.Name);
        }

        //doubt : HOW
        [Fact]
        public void AddPatient_ShouldAddPatient_Valid()
        {
            PatientApiModel patient = null;
            mock.Setup(x => x.AddPatient(It.IsAny<PatientApiModel>()))
                .Callback<PatientApiModel>(x => patient = x);

            var pat = new PatientApiModel
            {
                Name = "Test Patient",
                MobileNumber = "1111111111",
                DateOfBirth = new System.DateTime(2020, 1, 1),
                BloodGroup = "AB-",
                Email = "test@ut.com",
                MedicalIssues = null
            };

            PatientsController pc = new PatientsController(mock.Object, mockLogger.Object);
            var result = pc.Post(pat);

            mock.Verify(x => x.AddPatient(It.IsAny<PatientApiModel>()), Times.Once);

            Assert.Equal(patient.Name, pat.Name);

        }

        [Fact]
        public void AddPatient_InvalidPatientData_ShouldGiveBadRequest()
        {
            var pat = new PatientApiModel
            {
                Name = "Test Patient",
                Email = "test@test.com"
            };

            PatientsController pc = new PatientsController(mock.Object, mockLogger.Object);
            pc.ModelState.AddModelError("MobileNumber", "Mobile Number is required");
            var result = pc.Post(pat);

            var content = result.Result;

            mock.Verify(x => x.AddPatient(It.IsAny<PatientApiModel>()), Times.Never);
        }

        [Fact]
        public void EditPatient_InvalidPatientData_ShouldGiveBadRequest()
        {


            var pat = new PatientApiModel
            {
                Name = "Test Patient",
                Email = "test@test.com"
            };

            PatientsController pc = new PatientsController(mock.Object, mockLogger.Object);
            pc.ModelState.AddModelError("MobileNumber", "Mobile Number is required");
            var result = pc.Post(pat);

            var content = result.Result;

            mock.Verify(x => x.EditPatient(1,It.IsAny<PatientApiModel>()), Times.Never);
        }

        public List<PatientApiModel> GetPatients()
        {
            return new List<PatientApiModel>()
            {
                new PatientApiModel {Id=1, Name="Roy", MobileNumber = "12345", BloodGroup = "O+", DateOfBirth = new System.DateTime(2020,1,1), Email = "roy@world.com", MedicalIssues = null},
                new PatientApiModel {Id=2, Name="Vince", MobileNumber = "67980", BloodGroup = "O-", DateOfBirth = new System.DateTime(2019,1,1), Email = "vince@world.com", MedicalIssues = null},
                new PatientApiModel {Id=3, Name="Root", MobileNumber = "24680", BloodGroup = "AB+", DateOfBirth = new System.DateTime(2018,1,1), Email = "root@world.com", MedicalIssues = null}
            };
        }
    }
}
