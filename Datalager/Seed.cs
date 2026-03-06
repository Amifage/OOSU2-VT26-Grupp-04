using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitetslager;

namespace Datalager
{
    public class Seed //Fyller databsen första gången koden kompileras
    {

        SamverketContext samverketContext = new SamverketContext();

        Personal personal = new Personal();
        Medlem medlem = new Medlem();
        Resurs resurs = new Resurs();
        Utrustning utrustning = new Utrustning();
        Bokning bokning = new Bokning();    

        #region Populate Personal
        public static void PopulatePersonal(SamverketContext samverketContext)
        {
            if (samverketContext.Personal.Any())
                return;

            samverketContext.Personal.Add(new Personal()
            {

                Namn = "sara",

                Roll = "administratör",

                Lösenord = "123"

            });

            samverketContext.SaveChanges();

            samverketContext.Personal.Add(new Personal()
            {

                Namn = "amanda",

                Roll = "receptionist",

                Lösenord = "456"

            });

            samverketContext.SaveChanges();

            samverketContext.Personal.Add(new Personal()
            {

                Namn = "gabbe",

                Roll = "reparatör",

                Lösenord = "789"

            });

            samverketContext.SaveChanges();
        }
        #endregion

        #region Populate Medlem 
        public static void PopulateMedlem(SamverketContext samverketContext)
        {
            if (samverketContext.Medlem.Any())
                return;

            samverketContext.Medlem.Add(new Medlem()
            {

                Namn = "sara",
                Epost = "sara@gmail.com",
                Telefonnummer = "076 000 00 00",
                Lösenord = "123",
                Medlemsnivå = "flex",
                Poäng = 100,
                Betalstatus = "betald",
                  SenastUppdaterad = DateTime.Now,

            });

            samverketContext.SaveChanges();

            samverketContext.Medlem.Add(new Medlem()
            {

                Namn = "amanda",
                Epost = "amanda@gmail.com",
                Telefonnummer = "076 111 11 11",
                Lösenord = "456",
                Medlemsnivå = "företag",
                Poäng = 100,
                Betalstatus = "obetald",
                SenastUppdaterad = DateTime.Now,
            });

            samverketContext.SaveChanges();

            samverketContext.Medlem.Add(new Medlem()
            {

                Namn = "gabbe",
                Epost = "gabbe@gmail.com",
                Telefonnummer = "076 222 22 22",
                Lösenord = "789",
                Medlemsnivå = "fast",
                Poäng = 100,
                Betalstatus = "betald",
                SenastUppdaterad = DateTime.Now,
            });

            samverketContext.SaveChanges();
        }
        #endregion 

        #region Populate Resurs
        public static void PopulateResurs(SamverketContext samverketContext)
        {
            if (samverketContext.Resurs.Any())
                return;

            samverketContext.Resurs.Add(new Resurs()
            {

                Namn = "katten",
                Typ = "konferensrum",
                Kapacitet = 10,
                Status = "ledig",
                SenastUppdaterad = DateTime.Now,
            });

            samverketContext.SaveChanges();

            samverketContext.Resurs.Add(new Resurs()
            {

                Namn = "hunden",
                Typ = "mötesrum",
                Kapacitet = 5,
                Status = "bokad",
                SenastUppdaterad = DateTime.Now,
            });

            samverketContext.SaveChanges();

            samverketContext.Resurs.Add(new Resurs()
            {

                Namn = "hästen",
                Typ = "kontorsrum",
                Kapacitet = 2,
                Status = "ledig",
                SenastUppdaterad = DateTime.Now,
            });

            samverketContext.SaveChanges();


        }
        #endregion

        #region Populate Utrustning
        public static void PopulateUtrustning(SamverketContext samverketContext)
        {
            if (samverketContext.Utrustning.Any())
                return;

            samverketContext.Utrustning.Add(new Utrustning()
            {

                Namn = "whiteboard",

                Kategori = "whiteboard",

                Skick = "bra",

                ResursID = 1

            });

            samverketContext.SaveChanges();


            samverketContext.Utrustning.Add(new Utrustning()
            {

                Namn = "projektor",

                Kategori = "projektor",

                Skick = "trasig",

                ResursID = null

            });

            samverketContext.SaveChanges();

            samverketContext.Utrustning.Add(new Utrustning()
            {

                Namn = "dator",

                Kategori = "skärm",

                Skick = "sliten",

                ResursID = 2

            });

            samverketContext.SaveChanges();

            samverketContext.Utrustning.Add(new Utrustning()
            {

                Namn = "tv",

                Kategori = "skärm",

                Skick = "bra",

                ResursID = 1

            });

            samverketContext.SaveChanges();

        }

        #endregion

        #region Populate Bokning
        public static void PopulateBokning(SamverketContext samverketContext)
        {
            if (samverketContext.Bokning.Any())
                return;

            samverketContext.Bokning.Add(new Bokning()
            {

                MedlemID = 1,
                ResursID = 1,
                Starttid = DateTime.Parse("2026-02-15 10:00"),
                Sluttid = DateTime.Parse("2026-02-15 11:00"),
                SenastUppdaterad = DateTime.Now,
            });

            samverketContext.SaveChanges();

            samverketContext.Bokning.Add(new Bokning()
            {

                MedlemID = 2,
                ResursID = 2,
                Starttid = DateTime.Parse("2026-02-17 13:00"),
                Sluttid = DateTime.Parse("2026-02-17 14:30"),
                SenastUppdaterad = DateTime.Now,
            });

            samverketContext.SaveChanges();

            samverketContext.Bokning.Add(new Bokning()
            {

                MedlemID = 3,
                ResursID = 3,
                Starttid = DateTime.Parse("2026-02-20 12:00"),
                Sluttid = DateTime.Parse("2026-02-20 14:00"),
                SenastUppdaterad = DateTime.Now,
            });

            samverketContext.SaveChanges();
        }
#endregion
    }
}
