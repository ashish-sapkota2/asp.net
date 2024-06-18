using Microsoft.EntityFrameworkCore;
using NZWalk.Models;

namespace NZWalk.Data
{
    public class NZWalksDbContext :DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions): base(dbContextOptions) 
        { 


        }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region>Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed data for Difficulties
            //Easy,Medium, Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id=Guid.Parse("a5152b2a-65d0-4ce4-aba5-a5965795f7ac"),
                    Name="Easy",
                },
                new Difficulty()
                {
                    Id= Guid.Parse("a2d9cff7-c4ff-4078-9a32-23c4d2091b80"),
                    Name="Medium",
                },
                new Difficulty()
                {
                    Id=Guid.Parse("405662c8-fb7a-4ea2-82af-9dbb5b50c05b") ,
                    Name="Hard",
                },
            };
            //seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);


            //seed data for Regions
            var regions = new List<Region>
            {
                new Region() {
                    Id=Guid.Parse("62ce941a-f207-4222-a6cd-b7ce288c4caa"),
                    Name="Ackland",
                    Code= "Al",
                    RegionImageUrl= "Null"
                },
                 new Region() {
                    Id=Guid.Parse("9d53067e-b2e6-4ee1-b3a8-7c8188d49ac8"),
                    Name="Shuttherland",
                    Code= "STL",
                    RegionImageUrl= "Null"
                },
                  new Region() {
                    Id=Guid.Parse("2547fc32-2817-489b-8907-ae45ee2b2913"),
                    Name="Californea",
                    Code= "Cal",
                    RegionImageUrl= "Null"
                },
                   new Region() {
                    Id=Guid.Parse("64a5db8a-dab6-464e-bea7-edb822730619"),
                    Name="Japan",
                    Code= "JAP",
                    RegionImageUrl= "Null"
                },
                    new Region() {
                    Id=Guid.Parse("07a6055c-9414-4ea1-aaa3-6f2b49eb085f"),
                    Name="Thiland",
                    Code= "THI",
                    RegionImageUrl= "Null"
                },
                     new Region() {
                    Id=Guid.Parse("00a5f071-1269-4287-9705-dbf8d6b2a4d1"),
                    Name="Bankok",
                    Code= "BK",
                    RegionImageUrl= "Null"
                },
             };

            modelBuilder.Entity<Region>().HasData(regions);

        }

    }
}
