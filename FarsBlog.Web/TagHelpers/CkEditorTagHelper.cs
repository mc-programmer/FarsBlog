using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FarsBlog.Web.TagHelpers;

[HtmlTargetElement("ckeditor")]
public class CKEditorTagHelper : TagHelper
{
    [HtmlAttributeName("asp-for")]
    public ModelExpression? For { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "textarea";
        output.Attributes.SetAttribute("class", "ckeditor form-control");
        output.Attributes.SetAttribute("ckeditor", 1);

        if (For is not null)
            output.Content.SetHtmlContent(For.Model?.ToString());
    }
}