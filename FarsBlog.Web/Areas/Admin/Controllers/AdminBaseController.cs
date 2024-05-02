using Microsoft.AspNetCore.Mvc;

namespace FarsBlog.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminBaseController : Controller
{
    #region Messages

    public static string SuccessMessage = "SuccessMessage";
    public static string ErrorMessage = "ErrorMessage";
    public static string InfoMessage = "InfoMessage";
    public static string WarningMessage = "WarningMessage";

    public readonly string ToastrErrorMessage = "ToastrErrorMessage";
    public readonly string ToastrSuccessMessage = "ToastrSuccessMessage";

    #endregion
}