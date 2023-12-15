using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class SantaClausWorkflow : Workflow<ChristmasWishInput[], string[]>
    {
        public override async Task<string[]> RunAsync(WorkflowContext context, ChristmasWishInput[] wishes)
        {
            var christmasWishWorkflowTasks = new List<Task<ChristmasWishOutput>>();
            
            foreach (var wish in wishes)
            {
                christmasWishWorkflowTasks.Add(context.CallChildWorkflowAsync<ChristmasWishOutput>(
                    nameof(ChristmasWishWorkflow),
                    wish));
            }

            var christmasWishOutputs = await Task.WhenAll(christmasWishWorkflowTasks);

            await context.CallActivityAsync<DeliverGiftsOutput>(
                nameof(DeliverGiftsActivity),
                new DeliverGiftsInput());

            return christmasWishOutputs.Select(m => m.Message).ToArray();
        }
    }
}