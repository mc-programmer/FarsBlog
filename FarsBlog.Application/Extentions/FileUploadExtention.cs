using FarsBlog.Application.Convertors;
using FarsBlog.Application.Security;
using FarsBlog.Domain.Shared;
using Microsoft.AspNetCore.Http;

namespace FarsBlog.Application.Extentions;

public static class FileUploadExtention
{
    public static Result AddImageToServer(this IFormFile image, string fileName, string originalPath, int? width,
        int? height, string? thumbPath = null, string? deleteFileName = null)
    {
        if (image == null || !image.IsImage()) Result.Failure(ErrorMessages.NullValue);

        originalPath = Directory.GetCurrentDirectory() + "/wwwroot" + originalPath;
        thumbPath = Directory.GetCurrentDirectory() + "/wwwroot" + thumbPath;

        if (!Directory.Exists(originalPath))
            Directory.CreateDirectory(originalPath);

        if (!string.IsNullOrEmpty(deleteFileName))
        {
            if (File.Exists(originalPath + deleteFileName))
                File.Delete(originalPath + deleteFileName);

            if (!string.IsNullOrEmpty(thumbPath))
            {
                if (File.Exists(thumbPath + deleteFileName))
                    File.Delete(thumbPath + deleteFileName);
            }
        }

        string finalPath = originalPath + fileName;

        using (var stream = new FileStream(finalPath, FileMode.Create))
        {
            if (!Directory.Exists(finalPath)) image!.CopyTo(stream);
        }


        if (!string.IsNullOrEmpty(thumbPath))
        {
            if (!Directory.Exists(thumbPath))
                Directory.CreateDirectory(thumbPath);

            ImageOptimizer resizer = new ImageOptimizer();

            if (width != null && height != null)
                resizer.ImageResizer(originalPath + fileName, thumbPath + fileName, width, height);
        }

        return Result.Success();
    }

    public static void DeleteImage(this string? imageName, string OriginPath, string? ThumbPath)
    {
        if (string.IsNullOrEmpty(imageName)) return;

        OriginPath = Directory.GetCurrentDirectory() + "/wwwroot" + OriginPath;
        ThumbPath = Directory.GetCurrentDirectory() + "/wwwroot" + ThumbPath;

        if (File.Exists(OriginPath + imageName))
            File.Delete(OriginPath + imageName);

        if (!string.IsNullOrEmpty(ThumbPath))
        {
            if (File.Exists(ThumbPath + imageName))
                File.Delete(ThumbPath + imageName);
        }
    }
}
