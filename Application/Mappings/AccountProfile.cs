


using AutoMapper;
using Presentation.Dto.Profile;

namespace Application.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<CreateAccountDto, Account>();
            CreateMap<UpdateAccountDto, Account>();
            CreateMap<Account, GetAccountDto>();
        }
    }
}