using Microsoft.Xna.Framework;

namespace RunAbout;

internal interface IPhysicsEntity
{
    Vector2 Position { get; set; }
    Vector2 Velocity { get; set; }
}
