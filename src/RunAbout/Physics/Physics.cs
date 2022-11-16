using System.Collections.Generic;

namespace RunAbout;

internal sealed class Physics : IPhysicsService
{
    public static IPhysicsService Singleton { get; } = new Physics();

    public void Update(float timestep)
    {
        foreach (var entity in entities)
            entity.Position += entity.Velocity * timestep;
    }

    public void Register(IPhysicsEntity entity) =>
        entities.Add(entity);

    private Physics() { }

    private List<IPhysicsEntity> entities = new ();
}
