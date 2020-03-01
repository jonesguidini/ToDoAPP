using APP.Domain.DTOs;
using FluentValidation;

namespace APP.Domain.Entities.FluentValidation
{
    public class UserValidation : FluentValidation<UserForRegisterDTO>
    {
        public UserValidation()
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
                .Length(2, 100).WithMessage("O campo 'usuário' precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.Email)
                .EmailAddress().WithMessage("Informe um 'email' válido.")
                .Length(2, 100).WithMessage("O campo ''email' precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.Password)
                .NotEmpty().WithMessage("O campo 'senha' deve ser informado.")
                .Length(6, 20).WithMessage("O campo 'senha' precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(f => f.PasswordConfirmation)
                .NotEmpty().WithMessage("A confirmação da senhadeve ser informado.")
                .Length(2, 100).WithMessage("A confirmação da senha precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(x => x.PasswordConfirmation)
                .Equal(x => x.Password)
                .WithMessage("A Confirmação de Senha está diferente da Senha.");
        }
    }
}
