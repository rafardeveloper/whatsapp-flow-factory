using whatsapp_flow_factory.Models.Requests;

namespace whatsapp_flow_factory.Facades.Interfaces
{
    public interface IWhatsappFlowFacade
    {
        public Task<string> DataExchangeAsync(EncryptedRequest encryptedRequestBody);
    }
}
