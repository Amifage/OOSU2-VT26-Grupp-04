using Microsoft.EntityFrameworkCore;
using System;

namespace Datalager
{
    public class UnitOfWork : IDisposable
    {
        private readonly SamverketContext samverketContext;

        public MedlemRepository MedlemRepository { get; }
        public BokningRepository BokningRepository { get; }
        public ResursRepository ResursRepository { get; }
        public StatistikRepository StatistikRepository { get; }


        public UnitOfWork(SamverketContext context)
        {
            samverketContext = context ?? throw new ArgumentNullException(nameof(context));

            samverketContext.Database.EnsureCreated();

            MedlemRepository = new MedlemRepository(samverketContext);
            //BokningRepository = new BokningRepository(samverketContext);
            ResursRepository = new ResursRepository(samverketContext);
            //StatistikRepository = new StatistikRepository(samverketContext);

            Seed.PopulatePersonal(samverketContext);
            Seed.PopulateMedlem(samverketContext);
            Seed.PopulateResurs(samverketContext);
            Seed.PopulateUtrustning(samverketContext);
            Seed.PopulateBokning(samverketContext);

        }
        
        public int Save()
        {
            return samverketContext.SaveChanges();
        }
        public void Dispose()
        {
            samverketContext.Dispose();
        }           

    }
}
