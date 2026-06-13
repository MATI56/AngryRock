using UnityEngine;

public class DefaultBrick : BaseBrick
{
    [SerializeField] private GameObject _destroyBrick;

    private float _currentHealth;
    public override void OnDeath()
    {
        ShopManager.Instance.AddCoins(_brickStats.CoinValue);
        Instantiate(_destroyBrick, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public override void OnSpawn()
    {
        return;
    }

    public override void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth < 0)
            OnDeath();
    }

}
