using Dapr.Client;
using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    /// <summary>
    /// This activity looks up the partIds for a given giftId.
    /// </summary>
    public class LookupPartsActivity : WorkflowActivity<LookupPartsInput, LookupPartsOutput>
    {
        private readonly DaprClient _daprClient;

        public LookupPartsActivity(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public override async Task<LookupPartsOutput> RunAsync(
            WorkflowActivityContext context,
            LookupPartsInput input)
        {
            Console.WriteLine($"Looking up the parts for {input.GiftId}...");
            Thread.Sleep(1000);
            var random = new Random();
            var partsRange = new Range(1, random.Next(1, 10));
            var partIds = Enumerable.Range(partsRange.Start.Value, partsRange.End.Value).Select(x => $"Part{x}-{Guid.NewGuid()}").ToArray();

            await _daprClient.SaveStateAsync("statestore", input.WorkbenchId , partIds);
            Console.WriteLine($"Identified parts for {input.GiftId}: {string.Join(",", partIds)}...");

            return new LookupPartsOutput(true, partIds);
        }
    }

    public record LookupPartsInput(string GiftId, string WorkbenchId);
    public record LookupPartsOutput(bool IsSuccess, string[] PartIds);
}