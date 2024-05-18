using BaseApi.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;

namespace BaseApi.Interfaces
{
    public interface IGenericRepository<TKey, TModel> where TModel : BaseEntity<TKey>
    {
        Task<List<TModel>> GetAllAsync();
        Task<TModel> GetByIdAsync(TKey id);
        Task<TModel> CreateAsync(TModel entity);
        Task<TModel> UpdateAsync(TModel entity);
        Task DeleteAsync(TKey id);
        Task<bool> Exist(TKey id);
        public DateTime GetDateTime();
        public string GetPropertyName<T>(Expression<Func<T>> propertyExpression);
    }
}
