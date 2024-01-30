using AutoMapper;
using FinancialManagementDataLayer.Repositories;
using FinancialManagementServices.Models;
using Microsoft.Extensions.Logging;

namespace FinancialManagementServices.UserBeneficialServices
{
    public class TopUpOptionsManagementService : ITopUpOptionsManagementService
    {
        private readonly ITopUpOptionsRepository _topUpOptionsRepository;
        private readonly ILogger<TopUpOptionsManagementService> _logger;
        private readonly IMapper _mapper;
        CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public TopUpOptionsManagementService(ITopUpOptionsRepository topUpOptionsRepository, ILogger<TopUpOptionsManagementService> logger, IMapper mapper)
        {
            _topUpOptionsRepository = topUpOptionsRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<List<TopUpOptions>> GetTopUpOptionsList()
        {
            try
            {

                var topupOptions = await _topUpOptionsRepository.GetTopUpOptions(_cancellationTokenSource.Token);
                return _mapper.Map<List<TopUpOptions>>(topupOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while fetching user details");
                throw;
            }
        }
    }
}
