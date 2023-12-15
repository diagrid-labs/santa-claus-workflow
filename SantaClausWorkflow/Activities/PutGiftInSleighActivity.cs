using Dapr.Client;
using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class PutGiftInSleighActivity : WorkflowActivity<PutGiftInSleighInput, PutGiftInSleighOutput>
    {
        private readonly DaprClient _daprClient;

        public PutGiftInSleighActivity(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public override async Task<PutGiftInSleighOutput> RunAsync(
            WorkflowActivityContext context,
            PutGiftInSleighInput input)
        {
            Console.WriteLine($"Picking up {input.GiftId} from {input.WorkbenchId} and putting it in the sleigh...");

            // Remove the workbench state
            await _daprClient.DeleteStateAsync("statestore", input.WorkbenchId);

            return new PutGiftInSleighOutput(true);
        }
    }

    public record PutGiftInSleighInput(string GiftId, string WorkbenchId);
    public record PutGiftInSleighOutput(bool IsSuccess);
}