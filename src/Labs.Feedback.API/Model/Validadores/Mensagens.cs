namespace Labs.Feedback.API.Model;

public static class Mensagens
{
    public static string DESCRICAO_VAZIA { get; } = "A descrição da mensagem deve ser informada";
    public static string DESCRICAO_TAMANHO_MAXIMO { get; } = "A descrição da mensagem deve conter no máximo 100 dígitos";

    public static string CATEGORIA_INVALIDA { get; } = "A Categoria não é válida ou não foi informada";
}
