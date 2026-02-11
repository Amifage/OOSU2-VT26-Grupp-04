namespace Datalager
{
    public class UnitOfWork
    {
        private SamverketContext samverketContext;


        public UnitOfWork()
        {

            samverketContext = new SamverketContext();

            //samverketContext.Database.EnsureDeleted(); Vi ska inte ha denna då vi ej har behörighet att ta bort databasen
            samverketContext.Database.EnsureCreated();

            Seed.PopulatePersonal(samverketContext);
            Seed.PopulateMedlem(samverketContext);
            Seed.PopulateResurs(samverketContext);  
            Seed.PopulateUtrustning(samverketContext);
            Seed.PopulateBokning(samverketContext);



        }
        
    }
}
