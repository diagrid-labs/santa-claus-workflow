using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class WritePageActivity : WorkflowActivity<WritePageInput, WritePageOutput>
    {
        public override Task<WritePageOutput> RunAsync(
            WorkflowActivityContext context,
            WritePageInput input)
        {
            Console.WriteLine($"Writing page {input.PageNumber} for {input.GiftId}...");
            Thread.Sleep(1000);

            return Task.FromResult(new WritePageOutput(true));
        }
    }

    public record WritePageInput(string GiftId, string WorkbenchId, int PageNumber);
    public record WritePageOutput(bool IsSuccess);
}