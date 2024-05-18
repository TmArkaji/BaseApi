using AutoMapper;
using BaseApi.Configurations;
using BaseApi.Configurations.ExceptionsHandler;
using BaseApi.Data;
using BaseApi.DTOs;
using BaseApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class BrandController : GenericController<int, Brand, BrandDto>
    {
        public BrandController(IBrandRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        [HttpPost]
        public override async Task<ActionResult<BrandDto>> Create(BrandDto dto)
        {
            // Simular algunas validaciones
            var errors = ValidateBrandDto(dto);
            if (errors.Any())
            {
                throw new ValidationException(errors);
            }

            var entity = _mapper.Map<Brand>(dto);
            entity = await _repository.CreateAsync(entity);
            var createdDto = _mapper.Map<BrandDto>(entity);

            return CreatedAtAction(nameof(Get), new { id = entity.ID }, createdDto);
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            return StatusCode(405, $"Deleting {nameof(Brand)} is not allowed.");
        }


        private List<string> ValidateBrandDto(BrandDto dto)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.BrandName))
            {
                errors.Add(string.Format(ApiConstants.IS_REQUIRED, nameof(BrandDto), _repository.GetPropertyName(() => dto.BrandName)));
            }

            return errors;
        }
    }
}
