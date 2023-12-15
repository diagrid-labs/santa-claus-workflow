using Dapr.Workflow;
using SantaClausWorkflowDemo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDaprWorkflowClient();
builder.Services.AddDaprWorkflow(options =>
{
    // Note that it's also possible to register a lambda function as the workflow
    // or activity implementation instead of a class.
    options.RegisterWorkflow<ChristmasGiftWorkflow>();
    options.RegisterWorkflow<BookWorkflow>();
    options.RegisterWorkflow<WoodenToyWorkflow>();
    options.RegisterWorkflow<GetCatalystEarlyAccessWorkflow>();

    // These are the activities that get invoked by the workflow(s).
    // The ChristmasGiftWorkflow activities:
    options.RegisterActivity<NaughtyOrNiceActivity>();
    options.RegisterActivity<RegisterGiftActivity>();
    options.RegisterActivity<WrapGiftActivity>();
    options.RegisterActivity<PutGiftInSleighActivity>();

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

app.Run();

app.MapPost("/start", async (ChristmasGiftInput input) =>
{
     var workflowClient = app.Services.GetRequiredService<DaprWorkflowClient>();

    var instanceId = await workflowClient.ScheduleNewWorkflowAsync(
        name: nameof(ChristmasGiftWorkflow),
        input: input);
    
    return instanceId;
});