using Labs.Feedback.API.Model;
using System;

namespace Labs.Feedback.API.Extensions;

public static class StringExtensions
{
    public static Categoria ToCategoria(this string text)
    {
        Categoria categoria = Categoria.NENHUMA;

        if (!String.IsNullOrEmpty(text))
        {
            text = text.Trim().ToUpper();
            Enum.TryParse<Categoria>(text, out categoria);
        }

        return categoria;
    }

    public static Guid ToGuid(this string text)
    {
        Guid result = Guid.Empty;

        if (!String.IsNullOrEmpty(text))
        {
            text = text.Trim();
            Guid.TryParse(text, out result);
        }

        return result;
    }
}