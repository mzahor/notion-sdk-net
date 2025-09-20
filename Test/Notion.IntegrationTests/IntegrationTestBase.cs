﻿using System;
using Notion.Client;

namespace Notion.IntegrationTests;

public abstract class IntegrationTestBase
{
    protected readonly INotionClient Client;
    protected readonly string ParentPageId;
    protected readonly string ParentDatabaseId;

    protected IntegrationTestBase()
    {
        var options = new ClientOptions { AuthToken = Environment.GetEnvironmentVariable("NOTION_AUTH_TOKEN") };

        Client = NotionClientFactory.Create(options);

        ParentPageId = GetEnvironmentVariableRequired("NOTION_PARENT_PAGE_ID");
        ParentDatabaseId = GetEnvironmentVariableRequired("NOTION_PARENT_DATABASE_ID");
    }
    
    protected static string GetEnvironmentVariableRequired(string envName)
    {
        return Environment.GetEnvironmentVariable(envName) ??
               throw new InvalidOperationException($"Environment variable '{envName}' is required.");
    }
}
