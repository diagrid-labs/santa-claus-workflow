using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    /// <summary>
    /// This activity looks up the partIds for a given giftId.
    /// </summary>
    public class LookupPartsActivity : WorkflowActivity<LookupPartsInput, LookupPartsOutput>
    {
        public override Task<LookupPartsOutput> RunAsync(
            WorkflowActivityContext context,
            LookupPartsInput input)
        {
            Console.WriteLine($"Looking up the parts for {input.GiftId}...");
            var random = new Random();
            var partsRange = new Range(1, random.Next(1, 10));
            var partIds = Enumerable.Range(partsRange.Start.Value, partsRange.End.Value).Select(x => $"Part{x}-{Guid.NewGuid()}").ToArray();
            Console.WriteLine($"Identified parts for {input.GiftId}: {string.Join(",", partIds)}...");

            return Task.FromResult(new LookupPartsOutput(true, partIds));
        }
    }

    public record LookupPartsInput(string GiftId);
    public record LookupPartsOutput(bool IsSuccess, string[] PartIds);
}