using AutoMapper;
using DataAccessLayer.Models;
using PresentationLayer.ViewModels;

namespace PresentationLayer.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
