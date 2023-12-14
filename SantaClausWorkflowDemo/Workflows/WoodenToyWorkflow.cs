using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class WoodenToyWorkflow : Workflow<WoodenToyWorkflowInput, WoodenToyWorkflowOutput>
    {
        public override async Task<WoodenToyWorkflowOutput> RunAsync(WorkflowContext context, WoodenToyWorkflowInput input)
        {
            await context.CallActivityAsync<string>(
                nameof(AssembleToyActivity),
                new AssembleToyInput(input.Name, new [] { input.WoodenToy }));
            
            
            var tasks = new List<Task<string>>();

            foreach (var name in input)
            {
                tasks.Add(context.CallActivityAsync<string>(
                    nameof(CreateGreetingActivity),
                    name));
            }

            var messages = await Task.WhenAll(tasks);

            return messages;
        }
    }

    public record WoodenToyWorkflowInput(string Name, string WoodenToy);
    public record WoodenToyWorkflowOutput(bool IsSuccess);
}