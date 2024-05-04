using FarsBlog.Domain.DTOs.ViewModels.Article;
using FarsBlog.Domain.Shared;

namespace FarsBlog.Application.Services.Interfaces.Article;

public interface IArticleService
{
	#region Admin

	Task<FilterArticleViewModel> AdminFilterAsync(FilterArticleViewModel filter);
	Task<Result<AdminSideUpdateArticleViewModel>> GetArticleByIdForEditByAdminAsync(int id);
	Task<Result> CreateAsync(AdminSideCreateArticleViewModel model);
	Task<Result> UpdateAsync(AdminSideUpdateArticleViewModel model);
	Task<Result<bool>> DeleteAsync(int id);
	Task<Result<bool>> RecoverAsync(int id);

	#endregion
}
