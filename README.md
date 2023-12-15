# Santa Claus Workflow

Santa Claus is using Dapr Workflow to ensure everyone gets their Christmas gift on time in the holiday season. He has a large and diverse team of elves who are responsible for different tasks such as making toys, wrapping gifts, and loading the sleigh. Each task is an activity that can be executed by one or more elves. Santa needs to coordinate the activities of all the elves in a reliable and efficient way.

That's where Dapr Workflow comes in. Santa defines his workflow in code (he prefers C#, but Python or Java can also be used) where he specifies the sequence and conditions of the activities. Dapr Workflow takes care of the orchestration logic, fault tolerance, and scalability of the workflow, so Santa can focus on the business logic and optimal elf activity orchestration.

These are the worfklows Santa has written:

- [SantaClausWorkflow](./SantaClausWorkflow/Workflows/SantaClausWorkflow.cs): The main workflow that takes an array of wishes and starts a child workflow (ChristmasGiftWorkflow) for each of the wishes.
- [ChristmasGiftWorkflow](./SantaClausWorkflow/Workflows/ChristmasWishWorkflow.cs): This workflow contains activities to check if the person is naughty or nice, and if they are nice, the a gift is selected and registered for that person. Based on the type of gift another child workflow is started to make the gift. Once that child workflow is finished, activities are called to wrap the gift and load it into the sleigh.
- [BookWorkflow](./SantaClausWorkflow/Workflows/BookWorkflow.cs): This workflow contains activities to write a book, and bind the pages.
- [WoodenToyWorkflow](./SantaClausWorkflow/Workflows/WoodenToyWorkflow.cs): This workflow contains activities to build a wooden toy. It calls activities to lookup the parts, assemble the parts, and paint the toy.
- [CatalystEarlyAccessWorkflow](./SantaClausWorkflow/Workflows/CatalystEarlyAccessWorkflow.cs): This workflow contains one activity return a link to get early access to Diagrid Catalyst, so that person can make complex architectures simple, reliable and secure with APIs that connect to their existing infrastructure and work with their existing code.

## Prerequisites

1. [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
2. [Dapr CLI](https://docs.dapr.io/getting-started/install-dapr-cli/)
   - Ensure that you're using v1.12 (or higher) of the Dapr runtime and the CLI.
3. A REST client, such as [cURL](https://curl.se/), or the VSCode [REST client extension](https://marketplace.visualstudio.com/items?itemName=humao.rest-client).

To run the SantaWorkflow:

1. Build the solution:

    ```bash
    dotnet build
    ```

2. Use the Dapr CLI to run the web app:

    ```bash
    dapr run -f .
    ```

3. Use the endpoints in the [local-workflow-test.http](./local-workflow-tests.http) file to start the workflow and check the status.
