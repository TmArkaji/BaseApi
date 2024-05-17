using AutoMapper;
using BaseApi.Data;
using BaseApi.DTOs;
using BaseApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatusesController : GenericController<int, Status, StatusDto>
    {
        public StatusesController(IStatusesRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
