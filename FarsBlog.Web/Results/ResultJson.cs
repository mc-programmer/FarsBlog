using Microsoft.AspNetCore.Mvc;

namespace FarsBlog.Web.Results
{
    public class JsonResponse : JsonResult
    {
        public JsonResponse(string? message = "مشکلی پیش آمده", bool isSuccess = false) 
            : base(new {isSuccess=isSuccess, message = message })
        {
        }
    }
}