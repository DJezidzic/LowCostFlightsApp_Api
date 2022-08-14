using Microsoft.EntityFrameworkCore;

namespace LowCostFlightsAppApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    }
}
