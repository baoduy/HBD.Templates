namespace HBD.MediatR.EfAutoSave.Behaviors;

internal sealed class EfAutoSaveOptions
{
    public Type DbContextType { get; set; } = default!;
}