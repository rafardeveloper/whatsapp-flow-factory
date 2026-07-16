namespace whatsapp_flow_factory.Services.Interfaces
{
    public interface IScreenFactory
    {
        public abstract static IScreen CreateScreen(string screenName);
    }
}
