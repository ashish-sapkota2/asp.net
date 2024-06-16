using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalk.Data;
using NZWalk.Models;

namespace NZWalk.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;

        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
           var existingwalk=await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingwalk == null)
            {
                return null;
            }
            dbContext.Walks.Remove(existingwalk);
            await dbContext.SaveChangesAsync();

            return existingwalk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingwalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingwalk== null) { 
                return null;
            } 

            existingwalk.Name= walk.Name;
            existingwalk.Description= walk.Description; 
            existingwalk.LengthInKm = walk.LengthInKm;
            existingwalk.WalkImageUrl = walk.WalkImageUrl;  
            existingwalk.DifficultyId = walk.DifficultyId;  
            existingwalk.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();

            return existingwalk;

        }
    }
}
