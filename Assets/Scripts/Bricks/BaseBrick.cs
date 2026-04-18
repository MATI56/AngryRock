using UnityEngine;

public abstract class BaseBrick : MonoBehaviour, IBrick
{
    [SerializeField] protected BrickStats _brickStats;
    public abstract void OnDeath();

    public abstract void OnSpawn();

    public abstract void TakeDamage(float damage);
}

public interface IBrick
{
    void OnSpawn();
    void TakeDamage(float damage);
    void OnDeath();
}