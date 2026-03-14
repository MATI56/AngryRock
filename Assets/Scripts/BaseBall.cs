using UnityEngine;

public abstract class BaseBall : ScriptableObject, IBallEffect
{
    public abstract void OnStart(BallContext ctx);
    public abstract void OnStop(BallContext ctx);
    public abstract void OnUpdate(BallContext ctx);
    public abstract void OnHitEffect(BallContext ctx, Collision collision);
}

public interface IBallEffect
{
    public void OnStart(BallContext ctx);
    public void OnStop(BallContext ctx);
    public void OnUpdate(BallContext ctx);
    public void OnHitEffect(BallContext ctx, Collision collision);
}


