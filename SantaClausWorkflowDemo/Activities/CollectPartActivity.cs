using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    /// <summary>
    /// This activity instructs an elf to collect a part and put it on the designated workbench.
    /// </summary>
    public class CollectPartActivity : WorkflowActivity<CollectPartInput, CollectPartOutput>
    {
        public override Task<CollectPartOutput> RunAsync(WorkflowActivityContext context, CollectPartInput input)
        {
            Console.WriteLine($"{nameof(CollectPartActivity)} with input: {input}");
            var random = new Random();
            Thread.Sleep(random.Next(1000, 5000));
            return Task.FromResult(new CollectPartOutput(true));
        }
    }

    public record CollectPartInput(string GiftId, string partId, string workbenchId);
    public record CollectPartOutput(bool IsCollected);
}