using Krylov_KT_42_22.Filters.LoadFilter;
using Krylov_KT_42_22.Interfaces.LoadInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Krylov_KT_42_22.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoadController : ControllerBase
    {
        private readonly ILogger<LoadController> _logger;
        private readonly ILoadService _loadService;

        public LoadController(ILogger<LoadController> logger, ILoadService loadService)
        {
            _logger = logger;
            _loadService = loadService;
        }

        [HttpPost(Name = "GetLoads")]
        public async Task<IActionResult> GetLoads(LoadTeacherTimeFilter filter, CancellationToken cancellationToken = default)
        {
            var loads = await _loadService.GetLoads(filter, cancellationToken);
            return Ok(loads);
        }
    }
}
