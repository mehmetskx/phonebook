using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PhoneBook.Models;
using PhoneBook.Models.Dtos;
using PhoneBook.PersonOperationService.Controllers;
using PhoneBook.Services.PersonService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PhoneBookXUnitTest
{
    public class PersonsControllerTest
    {
        private readonly Mock<IPersonService> _mockPersonService;
        private readonly Mock<ILogger<PersonsController>> _mockLogging;
        private readonly PersonsController _personController;
        private readonly ResponseModel<List<PersonDto>> personLists;
        public PersonsControllerTest()
        {
            _mockPersonService = new Mock<IPersonService>();
            _mockLogging = new Mock<ILogger<PersonsController>>();
            _personController = new PersonsController(_mockLogging.Object, _mockPersonService.Object);

            #region Values  
            personLists = new ResponseModel<List<PersonDto>>()
            {
                Data = new List<PersonDto>()
                {
                    new PersonDto{Id =1,CompanyName="Rise Consulting", FirstName= "Mehmet", LastName= "ŞIK"},
                    new PersonDto{Id =2,CompanyName="Rise Consulting", FirstName= "Ahmet", LastName= "Taşcı"},
                }
            };
            #endregion
        }

        [Fact]
        public async void GetAll_ActionExecutes_ReturnIsSuccessParameterTrueInReponseModel()
        {
            _mockPersonService.Setup(x => x.GetPersons()).ReturnsAsync(personLists);

            var result = await _personController.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnPersons = Assert.IsAssignableFrom<ResponseModel<List<PersonDto>>>(okResult.Value);

            Assert.Equal<int>(2, returnPersons.Data.Count);
        }

        [Theory]
        [InlineData(0)]
        public async void GetById_ActionExecutes_ReturnPersonNotFound(int personId)
        {
            ResponseModel<PersonDto> personResponse = null;

            _mockPersonService.Setup(x => x.GetPersonByIdWithContactInfos(personId)).ReturnsAsync(personResponse);

            var result = await _personController.GetById(personId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void Add_ActionExecutes_ReturnCreated()
        {
            var person = personLists.Data.First();
            var responseModel = new ResponseModel<PersonDto>();
            responseModel.Data = person;           

            _mockPersonService.Setup(x => x.AddPerson(person)).ReturnsAsync(responseModel);

            var result = await _personController.Add(person);

            Assert.IsType<CreatedResult>(result);
        }

        [Theory]
        [InlineData(0)]
        public async void Delete_ActionExecutes_ReturnNotFound(int id)
        {
            var personResponse = new ResponseModel<PersonDto>();
            personResponse.ErrorList = new List<Error>() { new Error { Description = "Notfound  id" } };
            personResponse.IsSuccess = false;

            _mockPersonService.Setup(x => x.DeletePerson(id)).ReturnsAsync(personResponse);

            var result = await _personController.Delete(id);

            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
