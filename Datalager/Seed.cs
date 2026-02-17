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

                Namn = "Sara",

                Roll = "Administratör",

                Lösenord = "123"

            });

            samverketContext.SaveChanges();

            samverketContext.Personal.Add(new Personal()
            {

                Namn = "Amanda",

                Roll = "Receptionist",

                Lösenord = "456"

            });

            samverketContext.SaveChanges();

            samverketContext.Personal.Add(new Personal()
            {

                Namn = "Gabbe",

                Roll = "Reparatör",

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
                Medlemsnivå = "flex",
                Betalstatus = "betald",
                SenastUppdaterad = DateTime.Now,
            });

            samverketContext.SaveChanges();

            samverketContext.Medlem.Add(new Medlem()
            {

                Namn = "amanda",
                Epost = "amanda@gmail.com",
                Telefonnummer = "076 111 11 11",
                Medlemsnivå = "företag",
                Betalstatus = "obetald",
                SenastUppdaterad = DateTime.Now,
            });

            samverketContext.SaveChanges();

            samverketContext.Medlem.Add(new Medlem()
            {

                Namn = "gabbe",
                Epost = "gabbe@gmail.com",
                Telefonnummer = "076 222 22 22",
                Medlemsnivå = "fast",
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

                Namn = "Katten",
                Typ = "Konferensrum",
                Kapacitet = 10,
                Status = "Ledig",
                SenastUppdaterad = DateTime.Now,
            });

            samverketContext.SaveChanges();

            samverketContext.Resurs.Add(new Resurs()
            {

                Namn = "Hunden",
                Typ = "Mötesrum",
                Kapacitet = 5,
                Status = "Bokad",
                SenastUppdaterad = DateTime.Now,
            });

            samverketContext.SaveChanges();

            samverketContext.Resurs.Add(new Resurs()
            {

                Namn = "Hästen",
                Typ = "Kontorsrum",
                Kapacitet = 2,
                Status = "Ledig",
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

                Namn = "Whiteboard",

                Kategori = "Whiteboard",

                Skick = "Bra",

                ResursID = 1

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

                ResursID = 2

            });

            samverketContext.SaveChanges();

            samverketContext.Utrustning.Add(new Utrustning()
            {

                Namn = "TV",

                Kategori = "Skärm",

                Skick = "Bra",

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
