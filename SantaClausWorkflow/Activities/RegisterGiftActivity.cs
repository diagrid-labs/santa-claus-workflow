using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class RegisterWishActivity : WorkflowActivity<RegisterWishInput, RegisterWishOutput>
    {
        public override Task<RegisterWishOutput> RunAsync(
            WorkflowActivityContext context,
            RegisterWishInput input)
        {
            var giftId = $"Gift-{Guid.NewGuid()}";
            Console.WriteLine($"Registering {giftId} for {input.Name} ({input.GiftType})");
            Thread.Sleep(1000);

            return Task.FromResult(new RegisterWishOutput(giftId));
        }
    }

    public record RegisterWishInput(string Name, GiftType GiftType);
    public record RegisterWishOutput(string GiftId);
}