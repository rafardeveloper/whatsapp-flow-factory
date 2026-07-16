using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using whatsapp_flow_factory.Models.Extensions;
using whatsapp_flow_factory.Models.Request;
using whatsapp_flow_factory.Services;

namespace whatsapp_flow_factory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowController : ControllerBase
    {
        private readonly FlowsSettings flowsSettings;


        public FlowController(IOptions<FlowsSettings> settings)
        {
            flowsSettings = settings.Value;
        }

        [HttpPost("/")]
        public IActionResult PostEndpointData([FromBody] EncryptedRequest body)
        {
            try
            {
                var decrypted = EncryptionService.DecryptRequest(body.encrypted_aes_key, body.encrypted_flow_data, body.initial_vector, flowsSettings.PRIVATE_KEY, flowsSettings.PASSPHRASE);

                // Example to read decrypted fields
                var action = decrypted.decryptedBody.GetProperty("action").GetString();

                // Return the next screen & data to client
                var response = new { screen = "SCREEN_NAME", data = new { some_key = "some_value" } };
                var encryptedResponse = EncryptionService.EncryptResponse(response, decrypted.aesKeyBytes, decrypted.initialVectorBytes);

                // Return the response as plaintext
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
