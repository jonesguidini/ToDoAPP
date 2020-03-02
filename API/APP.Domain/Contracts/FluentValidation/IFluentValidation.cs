using System.ComponentModel.DataAnnotations;

namespace APP.Domain.Contracts.FluentValidation
{
    public interface IFluentValidation<TEntity> where TEntity : class
    {
        void SetValidation();

        ValidationResult GetValidations();
    }
}
