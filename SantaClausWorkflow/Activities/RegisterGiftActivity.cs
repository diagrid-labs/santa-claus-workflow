using Dapr.Client;
using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class RegisterWishActivity : WorkflowActivity<RegisterWishInput, RegisterWishOutput>
    {
        private readonly DaprClient _daprClient;

        public RegisterWishActivity(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public override async Task<RegisterWishOutput> RunAsync(
            WorkflowActivityContext context,
            RegisterWishInput input)
        {
            var giftId = $"Gift-{Guid.NewGuid()}";
            Console.WriteLine($"Registering {giftId} for {input.Name} ({input.GiftType})");

            await _daprClient.SaveStateAsync("statestore", input.Name , new { input.GiftType, giftId });
            Thread.Sleep(1000);

            return new RegisterWishOutput(giftId);
        }
    }

    public record RegisterWishInput(string Name, GiftType GiftType);
    public record RegisterWishOutput(string GiftId);
}