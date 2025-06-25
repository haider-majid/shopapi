using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMapper Mapper;

        protected BaseController(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}