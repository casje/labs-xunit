using System;
using FluentValidation;

namespace Labs.Feedback.API.Model.Validadores
{
    public class MensagemValidador : AbstractValidator<Mensagem>
    {
        public MensagemValidador()
        {
            RuleFor(m => m.Descricao)
                   .NotEmpty().WithMessage("A descrição da mensagem deve ser informada")
                   .MaximumLength(100).WithMessage("A descrição da mensagem deve conter no máximo 100 dígitos");

            RuleFor(m => m.Categoria)
                   .NotEqual(Categoria.NENHUMA).WithMessage("A Categoria não é válida ou não foi informada");

        }
    }
}