using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class WoodenToyWorkflow : Workflow<GiftWorkflowInput, GiftWorkflowOutput>
    {
        public override async Task<GiftWorkflowOutput> RunAsync(WorkflowContext context, GiftWorkflowInput input)
        {
            // The elf managing the inventory database wil execute a query to identify the parts for the wooden gift.
            var lookupPartsOutput = await context.CallActivityAsync<LookupPartsOutput>(
                nameof(LookupPartsActivity),
                new LookupPartsInput(input.GiftId, input.WorkbenchId));

            // Many elves will collect the individual parts and deliver it to the workbench.
            var collectPartTasks = new List<Task<CollectPartOutput>>();
            foreach (var partId in lookupPartsOutput.PartIds)
            {
                collectPartTasks.Add(context.CallActivityAsync<CollectPartOutput>(
                    nameof(CollectPartActivity),
                    new CollectPartInput(input.GiftId, partId, input.WorkbenchId)));
            }

            // Wait for all the elves to finish collecting the parts.
            await Task.WhenAll(collectPartTasks);

            // The construction elf will assemble the wooden toy.
            await context.CallActivityAsync<AssembleToyOutput>(
                nameof(AssembleToyActivity),
                new AssembleToyInput(input.GiftId, input.WorkbenchId));
            
            // The painter elf will paint the toy.
            await context.CallActivityAsync<PaintToyOutput>(
                nameof(PaintToyActivity),
                new PaintToyInput(input.GiftId, input.WorkbenchId));

            // Return to the main ChristmasGiftWorkflow!
            return new GiftWorkflowOutput($"The wooden toy is ready for {input.Name}!");
        }
    }

    public record GiftWorkflowInput(string GiftId, string Name, string WorkbenchId);
    public record GiftWorkflowOutput(string Message);
}