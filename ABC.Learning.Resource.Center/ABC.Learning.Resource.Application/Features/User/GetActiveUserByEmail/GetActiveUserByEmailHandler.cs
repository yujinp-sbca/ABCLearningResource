using ABC.Learning.Resource.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;

namespace ABC.Learning.Resource.Application.Features.User
{
    public class GetActiveUserByEmailHandler : IGetActiveUserByEmailHandler
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly ILogger<IGetActiveUserByEmailHandler> _logger;

        public GetActiveUserByEmailHandler(IUserAccountRepository userAccountRepository, ILogger<IGetActiveUserByEmailHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userAccountRepository = userAccountRepository ?? throw new ArgumentNullException(nameof(userAccountRepository));
        }

        public async Task<GetActiveUserByEmailResponse> Handle(string email)
        {
            var userResponse = new GetActiveUserByEmailResponse();

            if (string.IsNullOrEmpty(email))
            {
                _logger.LogError("Email cannot be null or empty.");
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            }

            _logger.LogInformation("Fetching active user by email: {Email}", email);
            var user = await _userAccountRepository.GetActiveUserByEmail(email);

            if (user != null) {
                userResponse.UserId = user.UserId;
                userResponse.IsAdmin = user.IsAdmin;
            }

            return userResponse;
        }
    }
}
