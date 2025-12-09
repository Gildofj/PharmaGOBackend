using Mapster;
using PharmaGO.Application.Clients.Common;
using PharmaGO.Contract.Authentication;

namespace PharmaGO.Api.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ClientAuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest, src => src.Client);
    }
}