namespace RunAbout;

internal interface IPhysicsService
{
    void Update(float timestep);
    void Register(IPhysicsEntity entity);
}
