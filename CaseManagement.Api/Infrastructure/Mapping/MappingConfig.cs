using CaseManagement.Api.Domain.DTOs.Clients;
using CaseManagement.Api.Domain.Entities;
using Mapster;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // config.NewConfig<User, UserDto>()
        //     .Map(dest => dest.FullName,
        //          src => $"{src.FirstName} {src.LastName}");

        config.NewConfig<Client, ClientDto>();
        config.NewConfig<ClientDto, Client>();
    }
}
