namespace whatsapp_flow_factory.Services.Interfaces
{
    public interface IScreen
    {
        public Task<string> DataExchangeAsync(string flowData);
    }
}
