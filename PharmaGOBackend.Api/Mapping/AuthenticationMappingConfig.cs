using Mapster;
using PharmaGOBackend.Application.Authentication.Common;
using PharmaGOBackend.Contract.Authentication;

namespace PharmaGOBackend.Api.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest, src => src.Client);
    }
}