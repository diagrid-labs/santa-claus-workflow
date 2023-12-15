using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class PaintToyActivity : WorkflowActivity<PaintToyInput, PaintToyOutput>
    {
        public override Task<PaintToyOutput> RunAsync(
            WorkflowActivityContext context,
            PaintToyInput input)
        {
            Console.WriteLine($"Start to paint {input.GiftId} on {input.WorkbenchId}...");
            Thread.Sleep(1000);

            return Task.FromResult(new PaintToyOutput(true));
        }
    }

    public record PaintToyInput(string GiftId, string WorkbenchId);
    public record PaintToyOutput(bool IsSuccess);
}