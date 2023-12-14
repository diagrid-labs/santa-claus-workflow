using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class AssembleToyActivity : WorkflowActivity<RegisterGift, BuildToyOutput>
    {
        public override Task<BuildToyOutput> RunAsync(WorkflowActivityContext context, RegisterGift input)
        {
            Console.WriteLine($"{nameof(AssembleToyActivity)} with input: {input}");
            var random = new Random();
            Thread.Sleep(random.Next(1000, 5000));
            return Task.FromResult(new BuildToyOutput(true));
        }
    }

    public record RegisterGift(string GiftId, string[] PartIds);
    public record BuildToyOutput(bool IsSuccess);
}