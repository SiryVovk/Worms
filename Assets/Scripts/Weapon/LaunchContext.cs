using System.Numerics;

public readonly struct LaunchContext
{
    public readonly float LaunchForce;
    public readonly Vector2 Direction;

    public LaunchContext(float launchForce, Vector2 direction)
    {
        LaunchForce = launchForce;
        Direction = direction;
    }
}
