using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class RegisterGiftActivity : WorkflowActivity<RegisterGiftInput, RegisterGiftOutput>
    {
        public override Task<RegisterGiftOutput> RunAsync(WorkflowActivityContext context, RegisterGiftInput input)
        {
            Console.WriteLine($"{nameof(RegisterGiftActivity)} with input: {input}");
            var giftId = $"Gift-{Guid.NewGuid().ToString()}";
            return Task.FromResult(new RegisterGiftOutput(giftId));
        }
    }

    public record RegisterGiftInput(string Name, string ToyName);
    public record RegisterGiftOutput(string GiftId);
}