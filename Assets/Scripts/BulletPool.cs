using UnityEngine;

public class BulletPool : ObjectPool<Bullet> 
{
    public Bullet Get(Vector2 direction = default)
    {
        Bullet bullet = base.Get();
        bullet.gameObject.SetActive(true);
        bullet.SetDirection(direction);
        bullet.OnCollisionDetected += Release;

        return bullet;
    }

    public override void Release(Bullet bullet)
    {
        bullet.OnCollisionDetected -= Release;
        base.Release(bullet);
    }
}
