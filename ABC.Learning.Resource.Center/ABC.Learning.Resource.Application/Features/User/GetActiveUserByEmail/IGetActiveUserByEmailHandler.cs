

namespace ABC.Learning.Resource.Application.Features.User
{
    public interface IGetActiveUserByEmailHandler
    {
        public Task<GetActiveUserByEmailResponse> Handle(string email);
    }
}
