namespace Pigga.Mvc.Exam.Areas.Manage.ViewModels
{
    public class PaginationVm<T> where T : class,new()
    {
        public ICollection<T> Items { get; set; }
        public int Limit { get; set; }
        public int CurrentPage { get; set; }
        public decimal TotalPage { get; set; }
    }
}
