using UnityEngine;
using UnityEngine.UI;

public abstract class BaseBall : ScriptableObject, IBallEffect
{
    public abstract void OnStart(BallContext ctx);
    public abstract void OnStop(BallContext ctx);
    public abstract void OnUpdate(BallContext ctx);
    public abstract void OnHitEffect(BallContext ctx, Collision collision);
    public abstract string GetName();
    public abstract int GetCost();
    public abstract Sprite GetSprite();
    public abstract BallStats GetStats();
}

public interface IBallEffect
{
    public void OnStart(BallContext ctx);
    public void OnStop(BallContext ctx);
    public void OnUpdate(BallContext ctx);
    public void OnHitEffect(BallContext ctx, Collision collision);
}


