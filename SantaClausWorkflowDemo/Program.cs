using Dapr.Workflow;
using SantaClausWorkflowDemo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDaprWorkflow(options =>
{
    // Note that it's also possible to register a lambda function as the workflow
    // or activity implementation instead of a class.
    options.RegisterWorkflow<WoodenToyWorkflow>();

    // These are the activities that get invoked by the workflow(s).
    options.RegisterActivity<RegisterGiftActivity>();
    options.RegisterActivity<AssignWorkbenchActivity>();
    options.RegisterActivity<IdentifyPartsActivity>();
    options.RegisterActivity<CollectPartActivity>();
    options.RegisterActivity<AssembleToyActivity>();
    options.RegisterActivity<PaintToyActivity>();
    options.RegisterActivity<WrapToyActivity>();
});

// Dapr uses a random port for gRPC by default. If we don't know what that port
// is (because this app was started separate from dapr), then assume 50001.
if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DAPR_GRPC_PORT")))
{
    Environment.SetEnvironmentVariable("DAPR_GRPC_PORT", "50001");
}

var app = builder.Build();

app.Run();
