using AutoMapper;
using EmployeeManagement.Models;
using EmployeeManagement.ModelViews;
namespace EmployeeManagement.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<City, CityModel>().ReverseMap();

            CreateMap<District, DistrictModel>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.DistrictName, opt => opt.MapFrom(srs => srs.Name));

            CreateMap<DistrictModel, District>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(srs => srs.DistrictName));

            CreateMap<Ward, WardModel>()
                .ForMember(dest => dest.WardName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DistrictName, opt => opt.MapFrom(src => src.District.Name))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.District.City.Name));

            CreateMap<WardModel, Ward>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.WardName));

            CreateMap<Employee, EmployeeModel>()
                .ForMember(dest => dest.Diplomas, opt => opt.MapFrom(src => src.Diplomas))
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job.Title))
                .ForMember(dest => dest.EthicName, opt => opt.MapFrom(src => src.Ethic.Name))
                .ForMember(dest => dest.WardName, opt => opt.MapFrom(src => src.Ward.Name))
                .ForMember(dest => dest.DistrictName, opt => opt.MapFrom(src => src.District.Name))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name));

            CreateMap<EmployeeModel, Employee>();
            CreateMap<Diploma, DiplomaModel>().ReverseMap();

            CreateMap<Ethic, Ethic>().ReverseMap();

            CreateMap<Job, JobModel>().ReverseMap();
        }
    }
}

