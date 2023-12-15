using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class DetermineBookContentActivity : WorkflowActivity<DetermineBookContentInput, DetermineBookContentOutput>
    {
        public override Task<DetermineBookContentOutput> RunAsync(
            WorkflowActivityContext context,
            DetermineBookContentInput input)
        {
            Console.WriteLine($"Determining the book content and length for {input.GiftId}...");
            Thread.Sleep(1000);
            var random = new Random();
            var numberOfPages = random.Next(10, 100);

            return Task.FromResult(new DetermineBookContentOutput(true, numberOfPages));
        }
    }

    public record DetermineBookContentInput(string GiftId, string WorkbenchId);
    public record DetermineBookContentOutput(bool IsSuccess, int NumberOfPages);
}