using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class RegisterGiftActivity : WorkflowActivity<RegisterGiftInput, RegisterGiftOutput>
    {
        public override Task<RegisterGiftOutput> RunAsync(
            WorkflowActivityContext context,
            RegisterGiftInput input)
        {
            var giftId = $"Gift-{Guid.NewGuid()}";
            Console.WriteLine($"Registering {giftId} for {input.Name} ({input.GiftType})");
            Thread.Sleep(1000);

            return Task.FromResult(new RegisterGiftOutput(giftId));
        }
    }

    public record RegisterGiftInput(string Name, GiftType GiftType);
    public record RegisterGiftOutput(string GiftId);
}