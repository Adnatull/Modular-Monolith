using AutoMapper;
using Module.Identity.Core.DataTransferObjects;
using Module.Identity.Core.Entities;

namespace Module.Identity.Core.AutomapperProfiles {
    public class AccountServiceProfile : Profile {
        public AccountServiceProfile() {
            CreateMap<RegisterUserDto, ApplicationUser>();
        }
    }
}
