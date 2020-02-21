using System.ComponentModel.DataAnnotations;
using APP.Domain.Entities;

namespace APP.Domain.Contracts.FluentValidation
{
    public interface IFluentValidation<TEntity> where TEntity : class 
    {
        void SetValidation();

        ValidationResult GetValidations();
    }
}
