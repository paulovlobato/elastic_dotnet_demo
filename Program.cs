// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
using Nest;
using System;

static void Main()
{
    var client = InitializeElasticClient();
    SetupIndex(client);

    var myModel = new MyModel { Id = 1, Name = "Example" };
    var indexResponse = client.IndexDocument(myModel);

    if (!indexResponse.IsValid)
    {
        Console.WriteLine($"Indexing failed: {indexResponse.DebugInformation}");
        if (indexResponse.OriginalException != null)
            Console.WriteLine($"Exception: {indexResponse.OriginalException.Message}");
    }

    // search that bloody data
    SearchAndDisplay(client, "Example");

    Console.ReadKey();

}

static ElasticClient InitializeElasticClient()
{
    var settings = new ConnectionSettings(new Uri("https://localhost:9200"))
        .BasicAuthentication("elastic", "F7LKW_ZJTfq=f*N42-SC")
        .DefaultIndex("my_index")
        .ServerCertificateValidationCallback((o, certificate, chain, errors) => true); // Ignore certificate validation;

    var client = new ElasticClient(settings);

    return client;

}

static void SetupIndex(ElasticClient client)
{
    var existsResponse = client.Indices.Exists("my_index");

    if (!existsResponse.Exists)
    {
        var createIndexResponse = client.Indices.Create("my_index", indexDescriptor =>
            indexDescriptor.Map<MyModel>(mappingDescriptor => mappingDescriptor.AutoMap())
        );
    }
}

static void SearchAndDisplay(ElasticClient client, string searchTerm)
{
    var searchResponse = client.Search<MyModel>(s => s
        .Query(q => q
            .Match(m => m
                .Field(f => f.Name)
                .Query(searchTerm)
            )
        )
    );

    foreach (var hit in searchResponse.Hits)
    {
        Console.WriteLine($"Document ID: {hit.Id} - Document: {hit.Source}");
    }
}

Main();