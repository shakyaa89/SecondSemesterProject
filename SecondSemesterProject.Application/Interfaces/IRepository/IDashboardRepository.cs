namespace SecondSemesterProject.Application.Interfaces.IRepository
{
    public interface IDashboardRepository
    {
        Task<object> GetSummary();
    }
}
