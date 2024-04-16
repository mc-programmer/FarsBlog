namespace FarsBlog.Domain.ViewModels.Common;

public class ModalBaseViewModel
{
    public string? ModalTitle { get; set; } = "";

    public string? Url { get; set; } = "";

    public string Method { get; set; } = "get";

    public string? SubmitButtonText { get; set; } = "ذخیره تغییرات";
}