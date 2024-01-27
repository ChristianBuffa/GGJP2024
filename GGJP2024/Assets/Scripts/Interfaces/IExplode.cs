using UnityEngine;

public interface IExplode
{
    public abstract void Explode(Vector2 explosionPosition, float explosionRadius, int damage);
}
