using Microsoft.AspNetCore.Mvc;

namespace Labs.Feedback.API.UnitTests;

public static class ObjectExtensions
{
    public static TipoRetorno GetData<TipoRetorno>(this ObjectResult response)
    {
        var postResponse = response.Value;
        var propertyInfo = postResponse.GetType().GetProperty("data");
        var data = propertyInfo.GetValue(postResponse);

        if (data is TipoRetorno)
            return (TipoRetorno)data;

        return default(TipoRetorno);
    }
}
