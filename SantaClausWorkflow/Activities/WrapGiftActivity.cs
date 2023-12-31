using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class WrapGiftActivity : WorkflowActivity<WrapGiftInput, WrapGiftOutput>
    {
        public override Task<WrapGiftOutput> RunAsync(
            WorkflowActivityContext context,
            WrapGiftInput input)
        {
            Console.WriteLine($"Wrapping {input.GiftId} for {input.Name}...");
            Thread.Sleep(1000);

            return Task.FromResult(new WrapGiftOutput(true));
        }
    }

    public record WrapGiftInput(string Name, string GiftId, string WorkbenchId);
    public record WrapGiftOutput(bool IsSuccess);
}