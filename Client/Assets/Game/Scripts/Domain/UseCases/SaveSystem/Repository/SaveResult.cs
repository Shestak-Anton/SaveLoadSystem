namespace UseCases.Repository
{
    public record SaveResult(bool Success, int Version)
    {
        public static SaveResult WithError() => new(false, -1);
        public static SaveResult WithSuccess(int version) => new(true, version);
    }
}