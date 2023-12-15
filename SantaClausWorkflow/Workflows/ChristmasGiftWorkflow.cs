using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class ChristmasGiftWorkflow : Workflow<ChristmasGiftInput, ChristmasGiftOutput>
    {
        public override async Task<ChristmasGiftOutput> RunAsync(WorkflowContext context, ChristmasGiftInput input)
        {
            // Check Santa book if the person is naughty or nice.
            var naughtyOrNiceOutput = await context.CallActivityAsync<NaughtyOrNiceOutput>(
                nameof(NaughtyOrNiceActivity),
                new NaughtyOrNiceInput(input.Name));
            
            if (naughtyOrNiceOutput.NaughtyOrNice == NaughtyOrNice.Naughty)
            {
                return new ChristmasGiftOutput($"Sorry {input.Name}, you are on the naughty list!");
            }

            // Register the person & their gift in Santa's database.
            var registerGiftOutput = await context.CallActivityAsync<RegisterGiftOutput>(
                nameof(RegisterGiftActivity),
                new RegisterGiftInput(
                    input.Name,
                    input.GiftType));

            // The floor manager elf will assign a workbench where the gift will be made and wrapped.
            var assignWorkbenchOutput = await context.CallActivityAsync<AssignWorkbenchOutput>(
                nameof(AssignWorkbenchActivity),
                new AssignWorkbenchInput(registerGiftOutput.GiftId));

            // Find the workflow that matches the GiftType.
            var workflowName = GiftWorkflowMap[input.GiftType];

            // Call the gift specific workflow.
            var giftWorkflowOutput = await context.CallChildWorkflowAsync<GiftWorkflowOutput>(
                workflowName,
                new GiftWorkflowInput(registerGiftOutput.GiftId, input.Name, assignWorkbenchOutput.WorkbenchId));

            // An elf will wrap the gift.
            await context.CallActivityAsync<WrapGiftOutput>(
                nameof(WrapGiftActivity),
                new WrapGiftInput(
                    input.Name,
                    registerGiftOutput.GiftId,
                    assignWorkbenchOutput.WorkbenchId));

            // One of the logistic elves will collect the gift from
            // the workbench and put it in the sleigh.
            await context.CallActivityAsync<PutGiftInSleighOutput>(
                nameof(PutGiftInSleighActivity),
                new PutGiftInSleighInput(
                    registerGiftOutput.GiftId,
                    assignWorkbenchOutput.WorkbenchId));

            return new ChristmasGiftOutput(giftWorkflowOutput.Message);
        }

        private readonly Dictionary<GiftType, string> GiftWorkflowMap = new()
        {
            { GiftType.Book, nameof(BookWorkflow) },
            { GiftType.WoodenToy, nameof(WoodenToyWorkflow) },
            { GiftType.CatalystEarlyAccess, nameof(GetCatalystEarlyAccessWorkflow) },
        };
    }

    public record ChristmasGiftInput(string Name, GiftType GiftType);
    public record ChristmasGiftOutput(string message);
    public enum GiftType
    {
        Book = 0,
        WoodenToy = 1,
        CatalystEarlyAccess = 2,
    }
}