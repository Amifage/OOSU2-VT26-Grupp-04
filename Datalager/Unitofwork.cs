namespace Datalager
{
    public class UnitOfWork
    {
        private SamverketContext samverketContext;


        public UnitOfWork()
        {
            SamverketContext samverketContext = new SamverketContext();

            samverketContext.Database.EnsureDeleted();
            samverketContext.Database.EnsureCreated();
        }
        
    }
}
