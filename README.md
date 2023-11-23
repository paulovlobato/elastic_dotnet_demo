# Elasticsearch C# Demo Project

## Overview
This project demonstrates the integration of Elasticsearch with a C# console application. It includes basic operations such as indexing, searching, and displaying results.

## Prerequisites
- .NET 5.0 or later
- Elasticsearch 7.x or later
- An instance of Elasticsearch running on `https://localhost:9200`

## Setup
1. Clone the repository.
2. Ensure Elasticsearch is running.
3. Build and run the project using .NET CLI commands:

```dotnet build```

```dotner run```

## Functionality
- Indexing a simple document.
- Searching for documents based on a query.
- Displaying search results in the console.

## Notes
- The project uses NEST, the official Elasticsearch .NET client.
- For demo purposes, certificate validation is disabled. This is not recommended for production environments.