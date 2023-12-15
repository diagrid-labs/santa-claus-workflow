using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class CollectPartActivity : WorkflowActivity<CollectPartInput, CollectPartOutput>
    {
        public override Task<CollectPartOutput> RunAsync(
            WorkflowActivityContext context,
            CollectPartInput input)
        {
            Console.WriteLine($"Collecting {input.PartId} for {input.GiftId}...");
            Thread.Sleep(1000);

            return Task.FromResult(new CollectPartOutput(true));
        }
    }

    public record CollectPartInput(string GiftId, string PartId, string workbenchId);
    public record CollectPartOutput(bool IsCollected);
}