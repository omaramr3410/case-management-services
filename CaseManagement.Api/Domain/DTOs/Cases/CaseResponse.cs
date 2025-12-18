using System.Collections;
using CaseManagement.Api.Domain.Entities;

namespace CaseManagement.Api.DTOs.Cases
{
    /// <summary>
    /// Response dto containing queried Case dtos
    /// </summary>
    [Serializable]
    public record CaseResponse
    {
        public IEnumerable<Case> cases = [];
    }
}