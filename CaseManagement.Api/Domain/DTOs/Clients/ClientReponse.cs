using CaseManagement.Api.Domain.DTOs.Clients;

namespace CaseManagement.Api.DTOs.Clients
{
    /// <summary>
    /// Response dto containing queried Client dtos
    /// </summary>
    [Serializable]
    public record ClientResponse
    {
        public IEnumerable<ClientDto> clientDetails = [];
    }
}