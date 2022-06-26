using HBD.StatusGeneric;

namespace TEMP.AppServices.Share;

public static class SaveChangesExceptionHandler
{
    #region Methods

    public static IStatusGeneric Handler(Exception exception)
    {
        var status = new StatusGenericHandler();

        if (exception != null)
            status.AddError(
                $"{exception.Message} {(exception.InnerException != null ? "\n" + exception.InnerException.Message : "")}");

        return status;
    }

    #endregion Methods
}