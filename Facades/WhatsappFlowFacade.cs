using System.Text.Json;
using whatsapp_flow_factory.Facades.Interfaces;
using whatsapp_flow_factory.Models.Extensions;
using whatsapp_flow_factory.Models.Requests;
using whatsapp_flow_factory.Models.Response;
using whatsapp_flow_factory.Services;
using whatsapp_flow_factory.Services.Interfaces;

namespace whatsapp_flow_factory.Facades
{
    public class WhatsappFlowFacade: IWhatsappFlowFacade
    {
        private readonly FlowsSettings _flowsSettings;

        public WhatsappFlowFacade(FlowsSettings flowsSettings)
        {
            _flowsSettings = flowsSettings;
        }

        public async Task<string> DataExchangeAsync(EncryptedRequest encryptedRequestBody)
        {
            var decrypted = EncryptionService.DecryptRequest(encryptedRequestBody.encrypted_aes_key, encryptedRequestBody.encrypted_flow_data, encryptedRequestBody.initial_vector, _flowsSettings.PRIVATE_KEY, _flowsSettings.PASSPHRASE);

            // Example to read decrypted fields
            var action = decrypted.decryptedBody.GetProperty("action").GetString();
            dynamic response;
            switch (action)
            {
                case "ping":
                    response = new PingResponse();
                    break;

                case "data_exchange":
                    string data = decrypted.decryptedBody.GetProperty("data").GetString();
                    if (!string.IsNullOrEmpty(data))
                    {
                        throw new ArgumentNullException("Data is null or empty");
                    }
                    string screenName = decrypted.decryptedBody.GetProperty("data").GetProperty("screen").GetString();

                    IScreen screen = ScreenFactory.CreateScreen(screenName);
                    response = await screen.DataExchangeAsync(data);
                    break;

                default:
                    throw new Exception("Unknown action");
            }

            var serializedResponse = JsonSerializer.Serialize(response);
            var encryptedResponse = EncryptionService.EncryptResponse(serializedResponse, decrypted.aesKeyBytes, decrypted.initialVectorBytes);

            return encryptedResponse;
        }



    }
}
