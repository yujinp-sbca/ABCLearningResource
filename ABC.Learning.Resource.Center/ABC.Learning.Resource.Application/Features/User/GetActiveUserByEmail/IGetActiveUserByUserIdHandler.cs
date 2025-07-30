

namespace ABC.Learning.Resource.Application.Features.User
{
    public interface IGetActiveUserByUserIdHandler
    {
        public Task<GetActiveUserByEmailResponseDTO> Handle(Guid userId);
    }
}
