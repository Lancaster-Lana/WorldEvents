﻿using System;

namespace WorldEvents.DBModel
{
    /// <summary>
    ///  Data DB inializer (if not EntityCore)
    /// </summary>
    public static class SatteliteDBInitializer
    {
        public static void Initialize(SatteliteDbContext context)
        {
            context.Database.EnsureCreated();

            Console.WriteLine("Seeding...");

            //Seed data to database
            SeederDataDB.Seed(context);

            Console.WriteLine("Seeding done");
        }
    }

    //public class SatteliteDBSInitializer : IDatabaseInitializer<SatteliteDbContext>
    //{
    //    public void InitializeDatabase(SatteliteDbContext context)
    //    {
    //        //Seeder.Seed(context);
    //    }
    //}

    /// <summary>
    /// Data DB inializer (if not EntityCore)
    /// </summary>
    /*public class SatteliteDBInitializer : CreateDatabaseIfNotExists<SatteliteDbContext>
    {
        protected override void Seed(SatteliteDbContext context)
        {
            base.Seed(context);

            Console.WriteLine("Seeding...");

            //Seed data to database
            SeederDataDB.Seed(context);

            Console.WriteLine("Seeding done");
        }
    }*/
}
