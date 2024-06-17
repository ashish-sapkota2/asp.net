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

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy=null, bool isAscending=true, int pageNumber = 1, int pageSize = 1080)
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
            if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
               
            }
            //sorting

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks= isAscending? walks.OrderBy(x => x.Name): walks.OrderByDescending(x=>x.Name);
                }
                else if(sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            //Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();

            //return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();

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
