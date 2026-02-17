using Entitetslager;
using Microsoft.EntityFrameworkCore;
using System;

namespace Datalager
{
    public class UnitOfWork : IDisposable
    {
        private readonly SamverketContext _samverketContext;

        public GenericRepository<Bokning> BokningRepository { private set; get; }        
        public GenericRepository<Medlem> MedlemRepository { get; private set; }
        public GenericRepository<Personal> PersonalRepository { private set; get; }
        public GenericRepository<Resurs> ResursRepository { private set; get; }
        public GenericRepository<Utrustning> UtrustningRepository { private set; get; }

        public Validering validering {  private set; get; } 
        public Bokningar bokningar { private set; get; }

        public UnitOfWork(SamverketContext samverketContext)
        {
            _samverketContext = samverketContext ?? throw new ArgumentNullException(nameof(samverketContext));

           
            MedlemRepository = new GenericRepository<Medlem>(samverketContext.Medlem); 
            PersonalRepository = new GenericRepository<Personal>(samverketContext.Personal);
            ResursRepository = new GenericRepository<Resurs> (samverketContext.Resurs);
            UtrustningRepository = new GenericRepository<Utrustning>(samverketContext.Utrustning);
            BokningRepository = new GenericRepository<Bokning>(samverketContext.Bokning);

            validering = new Validering(samverketContext);
            bokningar = new Bokningar(samverketContext);

            Seed.PopulateMedlem(samverketContext);
            Seed.PopulateResurs(samverketContext);
            Seed.PopulatePersonal(samverketContext);
            Seed.PopulateBokning(samverketContext);
            Seed.PopulateUtrustning(samverketContext);
        }
        
        public int Save()
        {
            return _samverketContext.SaveChanges();
        }
        public void Dispose()
        {
            _samverketContext?.Dispose();
        }           

    }
}
