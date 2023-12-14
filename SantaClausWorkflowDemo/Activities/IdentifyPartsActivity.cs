using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    /// <summary>
    /// This activity looks up the partIds for a given giftId.
    /// </summary>
    public class IdentifyPartsActivity : WorkflowActivity<IdentifyPartsInput, IdentifyPartsOutput>
    {
        public override Task<IdentifyPartsOutput> RunAsync(WorkflowActivityContext context, IdentifyPartsInput input)
        {
            Console.WriteLine($"{nameof(IdentifyPartsActivity)} with input: {input}");
            var random = new Random();
            var partsRange = new Range(1, random.Next(1, 10));
            var partIds = Enumerable.Range(partsRange.Start.Value, partsRange.End.Value).Select(x => $"{x}-{Guid.NewGuid()}").ToArray();

            return Task.FromResult(new IdentifyPartsOutput(true, partIds));
        }
    }

    public record IdentifyPartsInput(string GiftId);
    public record IdentifyPartsOutput(bool IsSuccess, string[] PartIds);
}