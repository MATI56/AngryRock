using UnityEngine;
using UnityEngine.UIElements;

public class ExplosiveBox : BaseBrick
{

    [SerializeField] private GameObject _destroyBrick;

    private float _currentHealth;
    public override void OnDeath()
    {
        Instantiate(_destroyBrick, transform.position, transform.rotation);

        Destroy(gameObject);
        Collider[] hits = new Collider[20];
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, 100f, hits);

        for (int j = 0; j < hitCount; j++)
        {
            Collider hitCollider = hits[j];
            if (hitCollider.TryGetComponent<IBrick>(out IBrick brick))
            {
                brick.TakeDamage(10f);
                Collider[] Inerhits = new Collider[20];
                int InerhitCount = Physics.OverlapSphereNonAlloc(transform.position, 100f, hits);
                for(int i = 0; i < InerhitCount; i++)
                {
                    if (hitCollider.TryGetComponent(out Rigidbody Inerrb))
                    {
                        Inerrb.AddExplosionForce(100f, transform.position, 100f, 1f, ForceMode.Impulse);
                    }
                }
            }
            if (hitCollider.TryGetComponent(out Rigidbody rb))
            {
                rb.AddExplosionForce(10f, transform.position, 10f, 1f, ForceMode.Impulse);
            }
        }
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
