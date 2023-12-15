using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class NaughtyOrNiceActivity : WorkflowActivity<NaughtyOrNiceInput, NaughtyOrNiceOutput>
    {
        public override Task<NaughtyOrNiceOutput> RunAsync(
            WorkflowActivityContext context,
            NaughtyOrNiceInput input)
        {
            Console.WriteLine($"Checking if {input.Name} is naughty or nice...");
            var random = new Random();
            Thread.Sleep(random.Next(500, 1000));

            return Task.FromResult(new NaughtyOrNiceOutput(NaughtyOrNice.Nice));
        }
    }

    public record NaughtyOrNiceInput(string Name);
    public record NaughtyOrNiceOutput(NaughtyOrNice NaughtyOrNice);
    public enum NaughtyOrNice
    {
        Nice = 0,
        Naughty = 1
    }
}