namespace PharmaGOBackend.UnitTests.Systems.Authentication.Controllers;

public class TestAuthenticationController
{
    /*#region Register

    [Fact]
    public void Register_OnSuccess_ReturnsStatusCode200()
    {
        var registerRequest = new RegisterRequest(
            "teste@pharmago.com", 
            "Teste", 
            "Unitario", 
            "123"
            );

        var mockMediatR = new Mock<ISender>();

        mockMediatR
            .Setup(service => 
                service.Send(
                new   
            )
            .Returns(new AuthenticationResult(new Client(), string.Empty));

        var sut = new AuthenticationController(mockMediatR.Object);

        var result = (OkObjectResult)sut.Register(registerRequest);

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void Register_OnSuccess_InvokesAuthenticationCommandService()
    {
        var registerRequest = new RegisterRequest(
            "teste@pharmago.com", 
            "Teste", 
            "Unitario", 
            "123"
            );

        var mockAuthenticationQueryService = new Mock<IAuthenticationQueryService>();
        var mockAuthenticationCommandService = new Mock<IAuthenticationCommandService>();

        mockAuthenticationCommandService
            .Setup(service => 
                service.
                    Register(new ClientDto(
                        registerRequest.FirstName, 
                        registerRequest.LastName, 
                        registerRequest.Email, 
                        registerRequest.Password)
                        )
                )
            .Returns(new AuthenticationResult(new Client(), string.Empty));

        var sut = new AuthenticationController(
            mockAuthenticationCommandService.Object, 
            mockAuthenticationQueryService.Object
            );

        mockAuthenticationCommandService.Verify(
            service => 
                service.Register(
                    new ClientDto(
                        registerRequest.FirstName, 
                        registerRequest.LastName, 
                        registerRequest.Email, 
                        registerRequest.Password
                        )
                    ),
            Times.Once()
            );
    }

    [Fact]
    public void Register_OnSuccess_ReturnsAuthenticationResponse()
    {
        var registerRequest = new RegisterRequest(
            "teste@pharmago.com", 
            "Teste", 
            "Unitario", 
            "123"
            );

        var mockAuthenticationQueryService = new Mock<IAuthenticationQueryService>();
        var mockAuthenticationCommandService = new Mock<IAuthenticationCommandService>();

        mockAuthenticationCommandService
            .Setup(service => 
                service.Register(
                    new ClientDto(
                    registerRequest.FirstName, 
                    registerRequest.LastName, 
                    registerRequest.Email, 
                    registerRequest.Password)
                        )
                )
            .Returns(new AuthenticationResult(new Client(), string.Empty));

        var sut = new AuthenticationController(
            mockAuthenticationCommandService.Object,
            mockAuthenticationQueryService.Object
            );

        var result = sut.Register(registerRequest);

        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        objectResult.Value.Should().BeOfType<AuthenticationResponse>();
    }

    #endregion

    #region Login

    [Fact]
    public void Login_OnSuccess_ReturnsStatusCode200()
    {
        var loginRequest = new LoginRequest("teste@pharmago.com", "123");

        var mockAuthenticationQueryService = new Mock<IAuthenticationQueryService>();
        var mockAuthenticationCommandService = new Mock<IAuthenticationCommandService>();

        mockAuthenticationQueryService
            .Setup(service => service.Login(loginRequest.Email, loginRequest.Password))
            .Returns(new AuthenticationResult(new Client(), string.Empty));

        var sut = new AuthenticationController(
            mockAuthenticationCommandService.Object,
            mockAuthenticationQueryService.Object
        );

        var result = (OkObjectResult)sut.Login(loginRequest);

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void Login_OnSuccess_InvokesAuthenticationQueryService()
    {
        var loginRequest = new LoginRequest("teste@pharmago.com", "123");

        var mockAuthenticationQueryService = new Mock<IAuthenticationQueryService>();
        var mockAuthenticationCommandService = new Mock<IAuthenticationCommandService>();

        mockAuthenticationQueryService
            .Setup(service => service.Login(loginRequest.Email, loginRequest.Password))
            .Returns(new AuthenticationResult(new Client(), string.Empty));

        var sut = new AuthenticationController(
            mockAuthenticationCommandService.Object,
            mockAuthenticationQueryService.Object
        );

        var result = (OkObjectResult)sut.Login(loginRequest);

        mockAuthenticationQueryService.Verify(
            service => service.Login(loginRequest.Email, loginRequest.Password),
            Times.Once()
            );
    }

    [Fact]
    public void Login_OnSuccess_ReturnsAuthenticationResponse()
    {
        var loginRequest = new LoginRequest("teste@pharmago.com", "123");

        var mockAuthenticationQueryService = new Mock<IAuthenticationQueryService>();
        var mockAuthenticationCommandService = new Mock<IAuthenticationCommandService>();

        mockAuthenticationQueryService
            .Setup(service => service.Login(loginRequest.Email, loginRequest.Password))
            .Returns(new AuthenticationResult(new Client(), string.Empty));

        var sut = new AuthenticationController(
            mockAuthenticationCommandService.Object,
            mockAuthenticationQueryService.Object
        );

        var result = sut.Login(loginRequest);

        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        objectResult.Value.Should().BeOfType<AuthenticationResponse>();
    }

    #endregion*/
}