using AutoMapper;
using Datingapp.API.Interface;

namespace Datingapp.API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly DapperDbContext dapperDbContext;

        public UnitOfWork(DataContext context, IMapper mapper,DapperDbContext dapperDbContext)
        {
            this.context = context;
            this.mapper = mapper;
            this.dapperDbContext = dapperDbContext;
        }
        public IUserRepository UserRepository => new UserRepository(dapperDbContext,context, mapper);

        public IMessageRepository MessageRepository => new MessageRepository(context, mapper);

        public ILikesRepository LikesRepository => new LikesRepository(context);
        public IPhotoRepository PhotoRepository => new PhotoRepository(context);

        public async Task<bool> Complete()
        {
            return await context.SaveChangesAsync()>0;
        }

        public bool hasChanges()
        {
            return context.ChangeTracker.HasChanges();
        }
    }
}
