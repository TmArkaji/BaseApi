using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BaseApi.Data;
using BaseApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenericController<TKey, TModel, TDto> : ControllerBase
        where TModel : BaseEntity<TKey>
        where TDto : class
    {
        private readonly IGenericRepository<TKey, TModel> _repository;
        private readonly IMapper _mapper;

        public GenericController(IGenericRepository<TKey, TModel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TDto>>> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            var dtos = _mapper.Map<List<TDto>>(entities);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TDto>> Get(TKey id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<TDto>(entity);
            return Ok(dto);
        }

        [HttpPost]
        public virtual async Task<ActionResult<TDto>> Create(TDto dto)
        {
            var entity = _mapper.Map<TModel>(dto);
            entity = await _repository.CreateAsync(entity);

            var createdDto = _mapper.Map<TDto>(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.ID }, createdDto);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(TKey id, TDto dto)
        {
            var entity = _mapper.Map<TModel>(dto);
            entity.ID = id;
            await _repository.UpdateAsync(entity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(TKey id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
