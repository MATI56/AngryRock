using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultBall", menuName = "Balls/DefaultBall")]
public class DefaultBall : BaseBall
{
    [SerializeField] private DefaultBallStats _stats;

    public override void OnHitEffect(BallContext ctx, Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IBrick>(out IBrick brick))
        {
            brick.TakeDamage(_stats.Damage);
        }
    }

    public override void OnStart(BallContext ctx)
    {
        ctx.Rb.mass = _stats.Weight;
        ctx.LifeSeconds = _stats.LifeSeconds;
        ctx.Rb.useGravity = true;
        ctx.Rb.isKinematic = false;
        ctx.Rb.AddForce(-ctx.Rb.transform.up * _stats.InicialImpulse, ForceMode.Impulse);
    }

    public override void OnStop(BallContext ctx)
    {
        ctx.Rb.useGravity = false;
        ctx.Rb.isKinematic = true;
        return;
    }

    public override void OnUpdate(BallContext ctx)
    {
        return;
    }
}
