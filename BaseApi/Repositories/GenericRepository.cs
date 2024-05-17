using BaseApi.Data;
using BaseApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;
using BaseApi.Configurations;

namespace BaseApi.Repositories
{
    public class GenericRepository<TKey, TModel> : IGenericRepository<TKey, TModel>
        where TModel : BaseEntity<TKey>
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<GenericRepository<TKey, TModel>> _logger;
        public readonly string _userId;

        public GenericRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ILogger<GenericRepository<TKey, TModel>> logger)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _userId = (_httpContextAccessor.HttpContext == null) ? "null" : _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value ?? ApiConstants.ANONYMOUSE_USER_ID;
        }

        public virtual async Task<List<TModel>> GetAllAsync()
        {
            return await _context.Set<TModel>().Where(e => !e.deleted).ToListAsync();
        }

        public virtual async Task<TModel> GetByIdAsync(TKey id)
        {
            return await _context.Set<TModel>().FindAsync(id);
        }

        public virtual async Task<TModel> CreateAsync(TModel entity)
        {
            try
            {
                entity = AddCreateData(entity);
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the entity.");
                throw;
            }
        }

        public virtual async Task<TModel> UpdateAsync(TModel entity)
        {
            try
            {
                entity = AddUpdateData(entity);
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the entity.");
                throw;
            }
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            try
            {
                var entity = await _context.Set<TModel>().FindAsync(id);
                if (entity != null)
                {
                    entity = AddUpdateData(entity);
                    entity.deleted = true;
                    _context.Entry(entity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the entity.");
                throw;
            }
        }

        public virtual async Task<bool> Exist(TKey id)
        {
            var entity = await GetByIdAsync(id);
            return entity != null;
        }

        public virtual DateTime GetDateTime()
        {
            return DateTime.UtcNow;
        }

        public virtual TModel AddCreateData(TModel entity)
        {
            entity.createDate = GetDateTime();
            entity.updateDate = GetDateTime();
            entity.createUserId = _userId;
            entity.updateUserId = _userId;
            return entity;
        }

        public virtual TModel AddUpdateData(TModel entity)
        {
            entity.updateDate = GetDateTime();
            entity.updateUserId = _userId;

            var updateEntity = _context.Set<TModel>().AsNoTracking().FirstOrDefault(e => e.ID.Equals(entity.ID));
            if (updateEntity != null)
            {
                entity.createDate = updateEntity.createDate;
                entity.createUserId = updateEntity.createUserId;
            }
            return entity;
        }

        public virtual async Task<List<TModel>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.Set<TModel>()
                .Where(e => !e.deleted)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public virtual async Task<TModel> GetByIdAsync(Expression<Func<TModel, bool>> predicate)
        {
            return await _context.Set<TModel>().FirstOrDefaultAsync(predicate);
        }
    }
}
