using Bogus;
using Labs.Feedback.API.Dto;
using System;
using System.Text;
using System.Text.Json;

namespace Labs.Feedback.API.UtilTest;

public class MensagemDtoBuilder
{
    public const string IDENT_DEFAULT = "5458892f-5283-4148-87c2-f8c3e19a8d0b";
    private readonly Faker _faker;
    private string _ident;
    private string _descricao;
    private string _categoria;

    public MensagemDtoBuilder()
    {
        _faker = new Faker("pt_BR");

        _ident = Guid.NewGuid().ToString();
        _descricao = GerarTexto(20);
        _categoria = "DUVIDA";
    }

    public static MensagemDtoBuilder Criar()
    {
        return new MensagemDtoBuilder();
    }

    public MensagemDtoBuilder SemIdent()
    {
        _ident = null;
        return this;
    }

    public MensagemDtoBuilder ComIdent(string ident)
    {
        _ident = ident;
        return this;
    }

    public MensagemDtoBuilder ComIdentDefault()
    {
        _ident = IDENT_DEFAULT;
        return this;
    }

    public MensagemDtoBuilder ComDescricao(string descricao)
    {
        _descricao = descricao;
        return this;
    }

    public MensagemDtoBuilder ComCategoria(string categoria)
    {
        _categoria = categoria;
        return this;
    }

    public MensagemDto Build()
    {
        var mensagem = new MensagemDto
        {
            Ident = _ident,
            Descricao = _descricao,
            Categoria = _categoria
        };

        return mensagem;
    }

    public static String ToJson(MensagemDto mensagemDto)
    {
        return JsonSerializer.Serialize(mensagemDto);
    }

    public static MensagemDto ToMensagemDto(string mensagemDto)
    {
        return JsonSerializer.Deserialize<MensagemDto>(mensagemDto);
    }

    private string GerarTexto(int tamanhoMinimoTexto)
    {
        StringBuilder strTexto = new StringBuilder();

        while (strTexto.Length < tamanhoMinimoTexto)
            strTexto.Append($"{_faker.Lorem.Word()} ");

        return strTexto.ToString();
    }
}
