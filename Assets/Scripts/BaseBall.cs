using UnityEngine;
using UnityEngine.UI;

public abstract class BaseBall : ScriptableObject, IBallEffect
{
    public bool IsActive = false;
    public virtual void OnStart(BallContext ctx) => IsActive = true;
    public virtual void OnStop(BallContext ctx) => IsActive = false;
    public abstract void OnUpdate(BallContext ctx);
    public abstract void OnHitEffect(BallContext ctx, Collision collision);
    public abstract string GetName();
    public abstract int GetCost();
    public abstract Sprite GetSprite();
}

public interface IBallEffect
{
    public void OnStart(BallContext ctx);
    public void OnStop(BallContext ctx);
    public void OnUpdate(BallContext ctx);
    public void OnHitEffect(BallContext ctx, Collision collision);
}


