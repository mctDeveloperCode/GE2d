using Microsoft.Xna.Framework;
using Xunit;

namespace RunAbout.Tests;

public class PhysicsTests
{
    [Fact]
    public void ObjectAtRestShouldStayAtRest()
    {
        IPhysicsService physics = Physics.Singleton;
        Vector2 initialPosition = new (42.0f, 11.0f);
        TestPhysicsEntity entity = new (position: initialPosition);
        physics.Register(entity);

        for (int n = 1; n <= 1000; n++)
        {
            physics.Update(TimeStep);

            MathAssert.Equal(initialPosition, entity.Position);
            MathAssert.Equal(Vector2.Zero, entity.Velocity);
        }
    }

    [Fact]
    public void ObjectInMotionShouldStayInMotion()
    {
        IPhysicsService physics = Physics.Singleton;
        Vector2 initialPosition = new (42.0f, 11.0f);
        Vector2 velocity = new (12.0f, 41.0f);
        TestPhysicsEntity entity = new (position: initialPosition, velocity: velocity);
        physics.Register(entity);

        var position = initialPosition;

        for (int n = 1; n <= 1000; n++)
        {
            physics.Update(TimeStep);

            position = initialPosition + velocity * TimeStep * n;

            MathAssert.Equal(position, entity.Position);
            MathAssert.Equal(velocity, entity.Velocity);
        }
    }

    // A typical frame rate in XNA is 60 frames per second, however using (1.0f / 60.0f)
    // introduces too much round off error into the calculations. By using 64 frames per
    // second the time step value is exactly represented by a float and round off error
    // is greatly reduced.
    private float TimeStep { get; } = 0.015625f;
}
