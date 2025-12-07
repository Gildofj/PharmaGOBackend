using Mapster;
using PharmaGO.Application.Authentication.Common;
using PharmaGO.Contract.Authentication;

namespace PharmaGO.Api.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest, src => src.Client);
    }
}