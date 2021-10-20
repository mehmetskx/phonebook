using AutoMapper;
using PhoneBook.Data.Entities;
using PhoneBook.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Services.Mapping
{
    public class PhoneBookModelMappingProfile : Profile
    {
        public PhoneBookModelMappingProfile() : this("PhoneBookModelMappingProfile")
        { }
        public PhoneBookModelMappingProfile(string profileName) : base(profileName)
        {
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<ContactType, ContactTypeDto>().ReverseMap();           
            CreateMap<Report, ReportDto>().ReverseMap();           
        }
    }
}
