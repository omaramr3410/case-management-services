using CaseManagement.Api.Domain.Entities;
using CaseManagement.Api.DTOs;

namespace CaseManagement.Api.Application.Orchestrators
{
    public interface IAuthOrchestrator
    {
        /// <summary>
        /// Method to attempt login of user
        /// </summary>
        /// <param name="request">Request body</param>
        /// <returns>Retrieves User if authenticated successfully </returns>
        User? Authenticate(LoginRequest request);
    }
}
