using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class AssignWorkbenchActivity : WorkflowActivity<AssignWorkbenchInput, AssignWorkbenchOutput>
    {
        public override Task<AssignWorkbenchOutput> RunAsync(WorkflowActivityContext context, AssignWorkbenchInput input)
        {
            Console.WriteLine($"{nameof(AssignWorkbenchActivity)} with input: {input}");
            var workbenchId = $"Workbench-{Guid.NewGuid().ToString()}";
            return Task.FromResult(new AssignWorkbenchOutput(workbenchId));
        }
    }

    public record AssignWorkbenchInput(string GiftId);
    public record AssignWorkbenchOutput(string workbenchId);
}