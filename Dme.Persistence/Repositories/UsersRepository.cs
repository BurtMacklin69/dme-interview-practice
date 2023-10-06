using AutoMapper;
using Dme.Interaction.Models.UsersExtraction;
using Dme.Interaction.RepositoryRequests;
using Dme.Persistence.Models.Models;
using Dme.Persistence.Pattern;
using Microsoft.EntityFrameworkCore;

namespace Dme.Persistence.Repositories
{
    internal class UsersRepository : Repository<UserEntity>, ICreateUsersRequest
    {
        private readonly IMapper _mapper;

        public UsersRepository(DbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        void ICreateUsersRequest.Create(IReadOnlyCollection<User> users)
        {
            var userEntities = users.Select(_mapper.Map<User, UserEntity>).ToList();
            Set.AttachRange(userEntities);
        }
    }
}