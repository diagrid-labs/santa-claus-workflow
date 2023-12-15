using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class AssignWorkbenchActivity : WorkflowActivity<AssignWorkbenchInput, AssignWorkbenchOutput>
    {
        public override Task<AssignWorkbenchOutput> RunAsync(
            WorkflowActivityContext context,
            AssignWorkbenchInput input)
        {
            var workbenchId = $"Workbench-{Guid.NewGuid()}";
            Console.WriteLine($"Assigning {workbenchId} for {input.GiftId}...");
            Thread.Sleep(1000);

            return Task.FromResult(new AssignWorkbenchOutput(workbenchId));
        }
    }

    public record AssignWorkbenchInput(string GiftId);
    public record AssignWorkbenchOutput(string WorkbenchId);
}