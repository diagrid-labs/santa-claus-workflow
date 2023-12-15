using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class BindBookActivity : WorkflowActivity<BindBookInput, BindBookOutput>
    {
        public override Task<BindBookOutput> RunAsync(
            WorkflowActivityContext context,
            BindBookInput input)
        {
            Console.WriteLine($"Binding {input.NumberOfPages} pages for {input.GiftId}...");

            var random = new Random();
            Thread.Sleep(random.Next(100, 100 * input.NumberOfPages));

            return Task.FromResult(new BindBookOutput(true));
        }
    }

    public record BindBookInput(string GiftId, string WorkbenchId, int NumberOfPages);
    public record BindBookOutput(bool IsSuccess);
}