using System.ComponentModel.DataAnnotations;

namespace FarsBlog.Domain.Enums.Common;

public enum DeletedStatus
{
    [Display(Name = "حذف نشده")] NotDeleted,
    [Display(Name = "همه")] All,
    [Display(Name = "حذف شده")] Deleted
}