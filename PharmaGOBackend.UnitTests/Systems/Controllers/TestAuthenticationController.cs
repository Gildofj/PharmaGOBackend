using Microsoft.AspNetCore.Mvc;
using PharmaGOBackend.Api.Controllers;
using PharmaGOBackend.Application.Services.Authentication;
using PharmaGOBackend.Contract.Authentication;
using PharmaGOBackend.Domain.Entities;

namespace PharmaGOBackend.UnitTests.Systems.Controllers;

public class TestAuthenticationController
{
    #region Register

    [Fact]
    public void Register_OnSuccess_ReturnsStatusCode200()
    {
        var registerRequest = new RegisterRequest("teste@pharmago.com", "Teste", "Unitario", "123");

        var mockAuthenticationService = new Mock<IAuthenticationService>();

        mockAuthenticationService
            .Setup(service => service.Register(registerRequest.FirstName, registerRequest.LastName, registerRequest.Email, registerRequest.Password))
            .Returns(new AuthenticationResult(new Client(), string.Empty));

        var sut = new AuthenticationController(mockAuthenticationService.Object);

        var result = (OkObjectResult)sut.Register(registerRequest);

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void Register_OnSuccess_InvokesAuthenticationService()
    {
        var registerRequest = new RegisterRequest("teste@pharmago.com", "Teste", "Unitario", "123");

        var mockAuthenticationService = new Mock<IAuthenticationService>();

        mockAuthenticationService
            .Setup(service => service.Register(registerRequest.FirstName, registerRequest.LastName, registerRequest.Email, registerRequest.Password))
            .Returns(new AuthenticationResult(new Client(), string.Empty));

        var sut = new AuthenticationController(mockAuthenticationService.Object);

        var result = (OkObjectResult)sut.Register(registerRequest);

        mockAuthenticationService.Verify(
            service => service.Register(registerRequest.FirstName, registerRequest.LastName, registerRequest.Email, registerRequest.Password),
            Times.Once()
            );
    }

    [Fact]
    public void Register_OnSuccess_ReturnsAuthenticationResponse()
    {
        var registerRequest = new RegisterRequest("teste@pharmago.com", "Teste", "Unitario", "123");

        var mockAuthenticationService = new Mock<IAuthenticationService>();

        mockAuthenticationService
            .Setup(service => service.Register(registerRequest.FirstName, registerRequest.LastName, registerRequest.Email, registerRequest.Password))
            .Returns(new AuthenticationResult(new Client(), string.Empty));

        var sut = new AuthenticationController(mockAuthenticationService.Object);

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

        var mockAuthenticationService = new Mock<IAuthenticationService>();

        mockAuthenticationService
            .Setup(service => service.Login(loginRequest.Email, loginRequest.Password))
            .Returns(new AuthenticationResult(new Client(), string.Empty));

        var sut = new AuthenticationController(mockAuthenticationService.Object);

        var result = (OkObjectResult)sut.Login(loginRequest);

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void Login_OnSuccess_InvokesAuthenticationService()
    {
        var loginRequest = new LoginRequest("teste@pharmago.com", "123");

        var mockAuthenticationService = new Mock<IAuthenticationService>();

        mockAuthenticationService
            .Setup(service => service.Login(loginRequest.Email, loginRequest.Password))
            .Returns(new AuthenticationResult(new Client(), string.Empty));

        var sut = new AuthenticationController(mockAuthenticationService.Object);

        var result = (OkObjectResult)sut.Login(loginRequest);

        mockAuthenticationService.Verify(
            service => service.Login(loginRequest.Email, loginRequest.Password),
            Times.Once()
            );
    }

    [Fact]
    public void Login_OnSuccess_ReturnsAuthenticationResponse()
    {
        var loginRequest = new LoginRequest("teste@pharmago.com", "123");

        var mockAuthenticationService = new Mock<IAuthenticationService>();

        mockAuthenticationService
            .Setup(service => service.Login(loginRequest.Email, loginRequest.Password))
            .Returns(new AuthenticationResult(new Client(), string.Empty));

        var sut = new AuthenticationController(mockAuthenticationService.Object);

        var result = sut.Login(loginRequest);

        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        objectResult.Value.Should().BeOfType<AuthenticationResponse>();
    }

    #endregion
}