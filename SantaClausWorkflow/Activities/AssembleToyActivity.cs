using Dapr.Client;
using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class AssembleToyActivity : WorkflowActivity<AssembleToyInput, AssembleToyOutput>
    {
        private readonly DaprClient _daprClient;

        public AssembleToyActivity(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public override async Task<AssembleToyOutput> RunAsync(
            WorkflowActivityContext context,
            AssembleToyInput input)
        {
            Console.WriteLine($"Starting assembly of {input.GiftId} at {input.WorkbenchId}...");

            var partIds = await _daprClient.GetStateAsync<string[]>("statestore", input.WorkbenchId);
            Console.WriteLine($"Assembling {partIds.Length} parts for {input.GiftId}...");

            var random = new Random();
            Thread.Sleep(random.Next(500, 500 * partIds.Length));

            return new AssembleToyOutput(true);
        }
    }

    public record AssembleToyInput(string GiftId, string WorkbenchId);
    public record AssembleToyOutput(bool IsSuccess);
}