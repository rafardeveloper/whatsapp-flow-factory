using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using whatsapp_flow_factory.Facades;
using whatsapp_flow_factory.Facades.Interfaces;
using whatsapp_flow_factory.Models.Extensions;
using whatsapp_flow_factory.Models.Requests;
using whatsapp_flow_factory.Services;

namespace whatsapp_flow_factory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowController : ControllerBase
    {
        private readonly FlowsSettings flowsSettings;
        private readonly IWhatsappFlowFacade _whatsappFlowFacade;

        public FlowController(IOptions<FlowsSettings> settings, IWhatsappFlowFacade whatsappFlowFacade)
        {
            flowsSettings = settings.Value;
            _whatsappFlowFacade = whatsappFlowFacade;
        }

        [HttpPost("/")]
        public async Task<IActionResult> PostEndpointData([FromBody] EncryptedRequest body)
        {
            try
            {
                var encryptedResponse = await _whatsappFlowFacade.DataExchangeAsync(body);

                return Ok(encryptedResponse);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an error response
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
