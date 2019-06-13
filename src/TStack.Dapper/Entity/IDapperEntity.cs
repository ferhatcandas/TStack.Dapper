namespace TStack.Dapper.Entity
{
    public interface IDapperEntity
    {
    }
    public interface IDapperEntity<TPrimary> : IDapperEntity
    {
        TPrimary Id { get; set; }
    }
}
