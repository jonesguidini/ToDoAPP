using FluentValidation;
using APP.Domain.DTOs;

namespace APP.Domain.Entities.FluentValidation
{
    public class AuthValidation : FluentValidation<UserForLoginDTO>
    {
        public AuthValidation()
        {
            SetValidation();
        }

        /// <summary>
        /// Valida as properties da Entidade
        /// </summary>
        public override void SetValidation()
        {
            RuleFor(f => f.Username)
                .NotEmpty().WithMessage("O campo 'usuário' deve ser informado.")
                .Length(5, 100)
                .WithMessage("O campo 'usuário' precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.Password)
                .NotEmpty().WithMessage("O campo 'senha' deve ser informado.")
                .Length(6, 20)
                .WithMessage("O campo 'senha' precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
