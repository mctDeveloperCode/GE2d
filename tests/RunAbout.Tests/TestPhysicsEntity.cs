using Microsoft.Xna.Framework;

namespace RunAbout.Tests;

internal sealed class TestPhysicsEntity : IPhysicsEntity
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }

    public TestPhysicsEntity(Vector2 position) : this(position, velocity: Vector2.Zero) { }

    public TestPhysicsEntity(Vector2 position, Vector2 velocity) =>
        (Position, Velocity) = (position, velocity);
}
