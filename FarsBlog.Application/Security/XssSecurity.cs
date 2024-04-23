﻿using Ganss.Xss;

namespace FarsBlog.Application.Security;

public static class XssSecurity
{
    public static string SanitizeText(this string text)
    {
        var htmlSanitizer = new HtmlSanitizer
        {
            KeepChildNodes = true,
            AllowDataAttributes = true
        };

        return htmlSanitizer.Sanitize(text);
    }

    public static string SanitizeTextAndTrim(this string text)
    {
        return text.Trim().SanitizeText();
    }
}