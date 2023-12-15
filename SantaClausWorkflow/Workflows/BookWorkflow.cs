using Dapr.Workflow;

namespace SantaClausWorkflowDemo
{
    public class BookWorkflow : Workflow<GiftWorkflowInput, GiftWorkflowOutput>
    {
        public override async Task<GiftWorkflowOutput> RunAsync(WorkflowContext context, GiftWorkflowInput input)
        {
            // A team of elves will brainstorm about the content of the book and how many pages the book will be.
            var determineBookContentOutput = await context.CallActivityAsync<DetermineBookContentOutput>(
                nameof(DetermineBookContentActivity),
                new DetermineBookContentInput(
                    input.GiftId,
                    input.WorkbenchId));

            // Many elves will write a page for the book.
            var writePageTasks = new List<Task<WritePageOutput>>();
            for (var i = 0; i < determineBookContentOutput.NumberOfPages; i++)
            {
                writePageTasks.Add(context.CallActivityAsync<WritePageOutput>(
                    nameof(WritePageActivity),
                    new WritePageInput(
                        input.GiftId,
                        input.WorkbenchId,
                        i)));
            }
            
            // Wait for all the elves to finish writing the pages.
            await Task.WhenAll(writePageTasks);

            // The book binder elf will bind the pages together.
            await context.CallActivityAsync<BindBookOutput>(
                nameof(BindBookActivity),
                new BindBookInput(input.GiftId, input.WorkbenchId));

            // Return to the main ChristmasGiftWorkflow!
            return new GiftWorkflowOutput($"The book is ready for {input.Name}!");
        }
    }
}