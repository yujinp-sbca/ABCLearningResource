using ABC.Learning.Resource.Application.Contracts.Persistence;
using Microsoft.Extensions.Logging;

namespace ABC.Learning.Resource.Application.Features.User
{
    public class GetActiveUserByUserIdHandler : IGetActiveUserByUserIdHandler
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly ILogger<IGetActiveUserByUserIdHandler> _logger;

        public GetActiveUserByUserIdHandler(IUserAccountRepository userAccountRepository, ILogger<IGetActiveUserByUserIdHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userAccountRepository = userAccountRepository ?? throw new ArgumentNullException(nameof(userAccountRepository));
        }

        public async Task<GetActiveUserByEmailResponseDTO> Handle(Guid userId)
        {
            var userResponse = new GetActiveUserByEmailResponseDTO();

            if (userId == Guid.Empty)
            {
                _logger.LogError("Email cannot be null or empty.");
                throw new ArgumentException("Email cannot be null or empty.", nameof(userId));
            }

            _logger.LogInformation("Fetching active user by email: {Email}", userId);
            var user = await _userAccountRepository.GetActiveUserByEmail(userId);

            if (user != null) {
                userResponse.UserId = user.UserId;
                userResponse.IsAdmin = user.IsAdmin;
            }

            return userResponse;
        }
    }
}
