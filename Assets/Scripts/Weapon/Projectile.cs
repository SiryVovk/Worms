using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Terrein terrain;
    private float explosionRadius;
    private float damage;
    private float lifetime;

    public void Init(Terrein terrain, float explosionRadius, float damage, float lifetime)
    {
        this.terrain = terrain;
        this.explosionRadius = explosionRadius;
        this.damage = damage;
        this.lifetime = lifetime;
    }

    private void Explode()
    {
        terrain.DestroyTerrain(transform.position, explosionRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
        Destroy(gameObject);
    }

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
