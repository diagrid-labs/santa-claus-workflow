using System.Text.Json.Serialization;
using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class ChristmasWishWorkflow : Workflow<ChristmasWishInput, ChristmasWishOutput>
    {
        public override async Task<ChristmasWishOutput> RunAsync(WorkflowContext context, ChristmasWishInput input)
        {
            // Check Santa book if the person is naughty or nice.
            var naughtyOrNiceOutput = await context.CallActivityAsync<NaughtyOrNiceOutput>(
                nameof(NaughtyOrNiceActivity),
                new NaughtyOrNiceInput(input.Name));
            
            if (naughtyOrNiceOutput.NaughtyOrNice == NaughtyOrNice.Naughty)
            {
                return new ChristmasWishOutput($"Sorry {input.Name}, you are on the naughty list!");
            }

            // Register the person & their wish in Santa's database.
            var registerWishOutput = await context.CallActivityAsync<RegisterWishOutput>(
                nameof(RegisterWishActivity),
                new RegisterWishInput(
                    input.Name,
                    input.GiftType));

            // The floor manager elf will assign a workbench where the gift will be made and wrapped.
            var assignWorkbenchOutput = await context.CallActivityAsync<AssignWorkbenchOutput>(
                nameof(AssignWorkbenchActivity),
                new AssignWorkbenchInput(registerWishOutput.GiftId));

            // Find the workflow that matches the GiftType.
            var workflowName = GiftWorkflowMap[input.GiftType];

            // Call the gift specific workflow.
            var giftWorkflowOutput = await context.CallChildWorkflowAsync<GiftWorkflowOutput>(
                workflowName,
                new GiftWorkflowInput(registerWishOutput.GiftId, input.Name, assignWorkbenchOutput.WorkbenchId));

            // An elf will wrap the gift.
            await context.CallActivityAsync<WrapGiftOutput>(
                nameof(WrapGiftActivity),
                new WrapGiftInput(
                    input.Name,
                    registerWishOutput.GiftId,
                    assignWorkbenchOutput.WorkbenchId));

            // One of the logistic elves will collect the gift from
            // the workbench and put it in the sleigh.
            await context.CallActivityAsync<PutGiftInSleighOutput>(
                nameof(PutGiftInSleighActivity),
                new PutGiftInSleighInput(
                    registerWishOutput.GiftId,
                    assignWorkbenchOutput.WorkbenchId));

            return new ChristmasWishOutput(giftWorkflowOutput.Message);
        }

        private readonly Dictionary<GiftType, string> GiftWorkflowMap = new()
        {
            { GiftType.Book, nameof(BookWorkflow) },
            { GiftType.WoodenToy, nameof(WoodenToyWorkflow) },
            { GiftType.CatalystEarlyAccess, nameof(CatalystEarlyAccessWorkflow) },
        };
    }

    public record ChristmasWishInput(string Name, GiftType GiftType);
    public record ChristmasWishOutput(string Message);
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GiftType
    {
        Book = 0,
        WoodenToy = 1,
        CatalystEarlyAccess = 2,
    }
}