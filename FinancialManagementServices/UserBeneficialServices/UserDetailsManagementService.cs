using AutoMapper;
using FinancialManagementDataLayer.Repositories;
using FinancialManagementServices.Models;
using Microsoft.Extensions.Logging;

namespace FinancialManagementServices.UserBeneficialServices
{
    public class UserDetailsManagementService : IUserDetailsManagementService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserDetailsManagementService> _logger;
        private readonly IMapper _mapper;
        CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public UserDetailsManagementService(IUserRepository userRepository, ILogger<UserDetailsManagementService> logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public Task<UserDetails> GetUserByIdAsync(int userId)
        {
            return Task.Run(async () =>
            {
                try
                {
                    var user = await _userRepository.GetUserById(userId, _cancellationTokenSource.Token);
                    return _mapper.Map<UserDetails>(user);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occured while fetching user details");
                    throw;
                }
            });
        }
    }
}
