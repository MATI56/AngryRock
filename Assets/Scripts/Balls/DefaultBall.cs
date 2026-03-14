using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultBall", menuName = "Balls/DefaultBall")]
public class DefaultBall : BaseBall
{
    [SerializeField] private DefaultBallStats _stats;

    public override void OnHitEffect(BallContext ctx, Collision collision)
    {
       return;
    }

    public override void OnStart(BallContext ctx)
    {
        ctx.Rb.mass = _stats.Weight;
        ctx.LifeSeconds = _stats.LifeSeconds;
        ctx.Rb.useGravity = true;
    }

    public override void OnStop(BallContext ctx)
    {
        ctx.Rb.useGravity = false;
        ctx.Rb.linearVelocity = Vector3.zero;
        ctx.Rb.angularVelocity = Vector3.zero;
        return;
    }

    public override void OnUpdate(BallContext ctx)
    {
        return;
    }
}
