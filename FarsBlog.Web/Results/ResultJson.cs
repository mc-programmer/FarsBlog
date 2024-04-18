using Microsoft.AspNetCore.Mvc;

namespace FarsBlog.Web.Results
{
    public class ModalJsonResult : JsonResult
    {
        public ModalJsonResult(string? message = "مشکلی پیش آمده", bool isSuccess = false) 
            : base(new {isSuccess=isSuccess, message = message })
        {
        }
    }
}