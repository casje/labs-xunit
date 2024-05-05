using FluentValidation;

namespace Labs.Feedback.API.Model.Validadores;

public class MensagemValidador : AbstractValidator<Mensagem>
{
    public MensagemValidador()
    {
        RuleFor(m => m.Descricao)
               .NotEmpty().WithMessage(Mensagens.DESCRICAO_VAZIA)
               .MaximumLength(100).WithMessage(Mensagens.DESCRICAO_TAMANHO_MAXIMO);

        RuleFor(m => m.Categoria)
               .NotEqual(Categoria.NENHUMA).WithMessage(Mensagens.CATEGORIA_INVALIDA);

    }
}