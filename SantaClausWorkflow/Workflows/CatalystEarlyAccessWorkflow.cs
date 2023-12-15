using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class CatalystEarlyAccessWorkflow : Workflow<GiftWorkflowInput, GiftWorkflowOutput>
    {
        public override async Task<GiftWorkflowOutput> RunAsync(WorkflowContext context, GiftWorkflowInput input)
        {
            // A Diagrid elf will put your name on the early access list for Catalyst,
            // so you can make complex architectures simple, reliable
            // and secure with APIs that connect to your existing infrastructure
            // and work with your existing code.
            var catalystEarlyAccessOutput = await context.CallActivityAsync<CatalystEarlyAccessOutput>(
                nameof(CatalystEarlyAccessActivity),
                new CatalystEarlyAccessAInput(input.Name));

            // Return to the main ChristmasGiftWorkflow!
            return new GiftWorkflowOutput(catalystEarlyAccessOutput.Message);
        }
    }
}