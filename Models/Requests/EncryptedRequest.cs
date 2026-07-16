namespace whatsapp_flow_factory.Models.Requests
{
    public record EncryptedRequest
    (
        string encrypted_aes_key,
        string encrypted_flow_data,
        string initial_vector
    );
    
}
