using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class PaintToyActivity : WorkflowActivity<PaintToyInput, PaintToyOutput>
    {
        public override Task<PaintToyOutput> RunAsync(WorkflowActivityContext context, PaintToyInput input)
        {
            Console.WriteLine($"{nameof(PaintToyActivity)} with input: {input}");
            var random = new Random();
            Thread.Sleep(random.Next(500, 2500));
            return Task.FromResult(new PaintToyOutput(true));
        }
    }

    public record PaintToyInput(string GiftId, string[] PartIds);
    public record PaintToyOutput(bool IsSuccess);
}