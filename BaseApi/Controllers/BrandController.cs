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
    public class BrandController : GenericController<int, Brand, BrandDto>
    {
        public BrandController(IBrandRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            return StatusCode(405, $"Deleting {nameof(Brand)} is not allowed.");
        }
    }
}
