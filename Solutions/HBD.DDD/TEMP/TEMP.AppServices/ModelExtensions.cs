using System.Runtime.CompilerServices;
using HBD.Web.Models;

[assembly: InternalsVisibleTo("TEMP.Infras.Lite")]

namespace TEMP.AppServices;

public static class ModelExtensions
{
    #region Methods

    public static bool IsNew<TModel>(this TModel model) where TModel : IModel
    {
        return model.Id == null;
    }

    #endregion Methods
}