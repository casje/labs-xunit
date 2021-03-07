using System;
using Labs.Feedback.API.Model;

namespace Labs.Feedback.API.Extensions
{
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
    }
}