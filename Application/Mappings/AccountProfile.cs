using AutoMapper;
using Presentation.Dto.Profile;
using System.Security.Cryptography;
using System.Text;

namespace Application.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<CreateAccountDto, Account>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src =>
                    Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(src.Password)))));
            CreateMap<UpdateAccountDto, Account>();
            CreateMap<Account, GetAccountDto>();
        }
    }
}