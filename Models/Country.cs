using System.Text.Json.Serialization;

namespace DOTNET_RPG.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Country
    {
        Brazil,
        Russia,
        Spain,
        Japan,
        India

    }
}