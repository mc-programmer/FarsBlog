using FarsBlog.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FarsBlog.Web.Results
{
    public class JsonResponse : JsonResult
    {
        private JsonResponse(bool isSuccess,string message) : base(new { isSuccess = isSuccess, message = message }) { }

        public static JsonResponse Success() => new(true,SuccessMessages.SuccessfullyDone);
        public static JsonResponse Success(string? message) => new (true , message ?? SuccessMessages.SuccessfullyDone);

        public static JsonResponse Failure() => new(false, ErrorMessages.OperationFailedError);
        public static JsonResponse Failure(string? message) => new (false,message ?? ErrorMessages.OperationFailedError);
    }
}