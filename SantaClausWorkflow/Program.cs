using Dapr.Workflow;
using SantaClausWorkflowDemo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDaprClient();
builder.Services.AddDaprWorkflowClient();
builder.Services.AddDaprWorkflow(options =>
{
    options.RegisterWorkflow<SantaClausWorkflow>();
    options.RegisterWorkflow<ChristmasWishWorkflow>();
    options.RegisterWorkflow<BookWorkflow>();
    options.RegisterWorkflow<WoodenToyWorkflow>();
    options.RegisterWorkflow<GetCatalystEarlyAccessWorkflow>();

    // These are the activities that get invoked by the workflow(s).
    // The ChristmasGiftWorkflow activities:
    options.RegisterActivity<NaughtyOrNiceActivity>();
    options.RegisterActivity<RegisterWishActivity>();
    options.RegisterActivity<WrapGiftActivity>();
    options.RegisterActivity<PutGiftInSleighActivity>();
    options.RegisterActivity<DeliverGiftsActivity>();

    // Shared activities:
    options.RegisterActivity<AssignWorkbenchActivity>();

    // The WoodenToyWorkflow activities:
    options.RegisterActivity<LookupPartsActivity>();
    options.RegisterActivity<CollectPartActivity>();
    options.RegisterActivity<AssembleToyActivity>();
    options.RegisterActivity<PaintToyActivity>();
    
    // The BookWorkflow activities:
    options.RegisterActivity<DetermineBookContentActivity>();
    options.RegisterActivity<WritePageActivity>();
    options.RegisterActivity<BindBookActivity>();

    // The GetCatalystEarlyAccessWorkflow activities:
    options.RegisterActivity<CatalystEarlyAccessActivity>();
});

// Dapr uses a random port for gRPC by default. If we don't know what that port
// is (because this app was started separate from dapr), then assume 50001.
if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DAPR_GRPC_PORT")))
{
    Environment.SetEnvironmentVariable("DAPR_GRPC_PORT", "50001");
}

var app = builder.Build();
var workflowClient = app.Services.GetRequiredService<DaprWorkflowClient>();

app.MapPost("/start", async (ChristmasWishInput[] wishes) =>
{
    Console.WriteLine($"Starting workflow with {wishes.Length} wishes.");
    var instanceId = await workflowClient.ScheduleNewWorkflowAsync(
        name: nameof(SantaClausWorkflow),
        input: wishes);
    
    return instanceId;
});

app.MapGet("/status/{instanceId}", async (string instanceId) =>
{
    return await workflowClient.GetWorkflowStateAsync(instanceId);
});

app.Run();