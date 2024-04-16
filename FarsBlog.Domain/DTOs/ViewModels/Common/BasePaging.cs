using FarsBlog.Domain.DTOs.ViewModels.Common.Filter;

namespace FarsBlog.Domain.DTOs.ViewModels.Common;

public class BasePaging<T>
{
    public int Page { get; set; }

    public int PageCount { get; set; }

    public int AllEntitiesCount { get; set; }

    public int StartPage { get; set; }

    public int EndPage { get; set; }

    public int TakeEntity { get; set; }

    public int SkipEntity { get; set; }

    public int HowManyShowPageAfterAndBefore { get; set; }

    public List<T> Entities { get; set; }

    public int Counter { get; set; }

    public BasePaging()
    {
        Page = 1;
        TakeEntity = 10;
        HowManyShowPageAfterAndBefore = 5;
        Entities = new List<T>();
    }

    public PagingViewModel GetCurrentPaging()
    {
        return new PagingViewModel
        {
            EndPage = EndPage,
            Page = Page,
            StartPage = StartPage,
            PageCount = PageCount
        };
    }

    public async Task<BasePaging<T>> Paging(IQueryable<T> queryable)
    {
        IQueryable<T> queryable2 = queryable;
        TakeEntity = TakeEntity;
        int allEntitiesCount = queryable2.Count();
        int pageCount = 0;
        try
        {
            pageCount = Convert.ToInt32(Math.Ceiling((double)allEntitiesCount / (double)TakeEntity));
        }
        catch (Exception)
        {
        }

        Page = ((Page > pageCount) ? pageCount : Page);
        if (Page <= 0)
        {
            Page = 1;
        }

        AllEntitiesCount = allEntitiesCount;
        HowManyShowPageAfterAndBefore = HowManyShowPageAfterAndBefore;
        SkipEntity = (Page - 1) * TakeEntity;
        StartPage = ((Page - HowManyShowPageAfterAndBefore <= 0) ? 1 : (Page - HowManyShowPageAfterAndBefore));
        EndPage = ((Page + HowManyShowPageAfterAndBefore > pageCount) ? pageCount : (Page + HowManyShowPageAfterAndBefore));
        PageCount = pageCount;
        Entities = await Task.Run(() => queryable2.Skip(SkipEntity).Take(TakeEntity).ToList());
        Counter = (Page - 1) * TakeEntity + 1;
        return this;
    }
}
