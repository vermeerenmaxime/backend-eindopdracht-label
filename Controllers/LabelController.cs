using System;
using Microsoft.Extensions.Logging;

namespace Label.API.Controllers
{
    public class LabelController
    {

        private readonly ILogger<LabelController> _logger;
        public LabelController(ILogger<LabelController> logger)
        {
            _logger = logger;
        }
    }
}
