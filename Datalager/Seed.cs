using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitetslager;

namespace Datalager
{
    public class Seed
    {

        SamverketContext samverketContext = new SamverketContext();
        
        Utrustning utrustning = new Utrustning();

        public static void Populate(SamverketContext samverketContext)
        {

            samverketContext.Utrustning.Add(new Utrustning()
            {

                Namn = "Whiteboard",

                Kategori = "Whiteboard",

                Skick = "Bra",

                ResursID = null

            });

            samverketContext.SaveChanges();


            samverketContext.Utrustning.Add(new Utrustning()
            {

                Namn = "Projektor",

                Kategori = "Projektor",

                Skick = "Trasig",

                ResursID = null

            });

            samverketContext.SaveChanges();

            samverketContext.Utrustning.Add(new Utrustning()
            {

                Namn = "Dator",

                Kategori = "Skärm",

                Skick = "Sliten",

                ResursID = null

            });

            samverketContext.SaveChanges();

            samverketContext.Utrustning.Add(new Utrustning()
            {

                Namn = "TV",

                Kategori = "Skärm",

                Skick = "Bra",

                ResursID = null

            });

            samverketContext.SaveChanges();

        }


        
    }
}
