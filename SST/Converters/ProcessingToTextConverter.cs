namespace SST.Converters
{
    public sealed class ProcessingToTextConverter : BooleanConverter<string>
    {
        public ProcessingToTextConverter() : base("Processing...", "Drag(RMB) or zoom to see the changes") { }
    }
}
