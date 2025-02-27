using System.Text.Json.Serialization;

namespace DrawingsGPTBackend.API;
public partial class MainWindow
{
    public class LoginRequest
    {
        [JsonPropertyName("username")]
        public string Email { get; set; } = "";
        [JsonPropertyName("password")]
        public string Password { get; set; } = "";
    }
}