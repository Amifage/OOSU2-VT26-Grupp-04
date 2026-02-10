namespace Datalager
{
    public class UnitOfWork
    {
        private SamverketContext samverketContext;


        public UnitOfWork()
        {

            samverketContext = new SamverketContext();

            //samverketContext.Database.EnsureDeleted(); Ska vi ha denna????
            samverketContext.Database.EnsureCreated();

            
        
        }
        
    }
}
