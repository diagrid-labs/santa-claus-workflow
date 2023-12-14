using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class WrapToyActivity : WorkflowActivity<WrapToyInput, WrapToyOutput>
    {
        public override Task<WrapToyOutput> RunAsync(WorkflowActivityContext context, WrapToyInput input)
        {
            Console.WriteLine($"{nameof(WrapToyActivity)} with input: {input}");
            var random = new Random();
            Thread.Sleep(random.Next(500, 2500));
            return Task.FromResult(new WrapToyOutput(true));
        }
    }

    public record WrapToyInput(string GiftId, string[] PartIds);
    public record WrapToyOutput(bool IsSuccess);
}