using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class DeliverGiftsActivity : WorkflowActivity<DeliverGiftsInput, DeliverGiftsOutput>
    {
        public override Task<DeliverGiftsOutput> RunAsync(
            WorkflowActivityContext context,
            DeliverGiftsInput input)
        {
            Console.WriteLine($"Delivering the gifts...ho, ho, ho!");

            return Task.FromResult(new DeliverGiftsOutput());
        }
    }

    public record DeliverGiftsInput();
    public record DeliverGiftsOutput();
}