using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class CatalystEarlyAccessActivity : WorkflowActivity<CatalystEarlyAccessAInput, CatalystEarlyAccessOutput>
    {
        public override Task<CatalystEarlyAccessOutput> RunAsync(
            WorkflowActivityContext context,
            CatalystEarlyAccessAInput input)
        {
            Console.WriteLine($"Getting the Catalyst Early Access link for {input.Name}...");
            Thread.Sleep(1000);
            string message = $"Hi {input.Name}, here's the link to get early access to Diagrid Catalyst: https://diagrid.ws/catalyst-early-access";
            Console.WriteLine(message);

            return Task.FromResult(new CatalystEarlyAccessOutput(true, message));
        }
    }

    public record CatalystEarlyAccessAInput(string Name);
    public record CatalystEarlyAccessOutput(bool IsSuccess, string Message);
}