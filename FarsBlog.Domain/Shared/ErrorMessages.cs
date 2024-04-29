namespace FarsBlog.Domain.Shared;

public static class ErrorMessages
{
    public const string RequiredError = "لطفا {0} را وارد کنید";
    public const string MaxLengthError = "تعداد کاراکتر مجاز {1} می باشد";
    public const string NullValue = "مقادیر وارد شده معتبر نمی باشند";
    public const string NotFoundErorr = "موردی یافت نشد";
    public const string SlugExistError = "عنوان در url از قبل موجود است";
    public const string TitleExistError = "عنوان از قبل موجود است";
    public const string OperationFailedError ="عملیات شکست خورد";
}
