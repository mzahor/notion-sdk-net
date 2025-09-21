﻿using System.Threading.Tasks;
using Notion.Client;
using Xunit;

namespace Notion.IntegrationTests;

public class AuthenticationClientTests : IntegrationTestBase
{
    private readonly string _clientId = GetEnvironmentVariableRequired("NOTION_CLIENT_ID");
    private readonly string _clientSecret = GetEnvironmentVariableRequired("NOTION_CLIENT_SECRET");

    [Fact]
    public async Task Create_and_revoke_token()
    {
        // Arrange
        var createRequest = new CreateTokenRequest
        {
            Code = "03b3bd2d-6b96-4104-a9f4-ee04d881532c",
            ClientId = _clientId,
            ClientSecret = _clientSecret,
            RedirectUri = "https://localhost:5001",
        };

        // Act
        var response = await Client.AuthenticationClient.CreateTokenAsync(createRequest);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.AccessToken);

        // revoke token
        await Client.AuthenticationClient.RevokeTokenAsync(new RevokeTokenRequest
        {
            Token = response.AccessToken,
            ClientId = _clientId,
            ClientSecret = _clientSecret
        });
    }

    [Fact]
    public async Task Introspect_token()
    {
        // Arrange
        var createRequest = new CreateTokenRequest
        {
            Code = "036822b2-62c1-42f4-95ea-0153e69cc20e",
            ClientId = _clientId,
            ClientSecret = _clientSecret,
            RedirectUri = "https://localhost:5001",
        };

        // Act
        var response = await Client.AuthenticationClient.CreateTokenAsync(createRequest);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.AccessToken);

        // introspect token
        var introspectResponse = await Client.AuthenticationClient.IntrospectTokenAsync(new IntrospectTokenRequest
        {
            Token = response.AccessToken,
            ClientId = _clientId,
            ClientSecret = _clientSecret
        });

        Assert.NotNull(introspectResponse);
        Assert.True(introspectResponse.IsActive);

        // revoke token
        await Client.AuthenticationClient.RevokeTokenAsync(new RevokeTokenRequest
        {
            Token = response.AccessToken,
            ClientId = _clientId,
            ClientSecret = _clientSecret
        });
    }
}
