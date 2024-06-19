public class EnemyPool : ObjectPool<Enemy> 
{
    public override Enemy Get()
    {
        Enemy enemy = base.Get();
        enemy.gameObject.SetActive(true);
        enemy.OnDying += Release;

        return enemy;
    }

    public override void Release(Enemy enemy)
    {
        enemy.OnDying -= Release;
        base.Release(enemy);
    }

    public override void Reset()
    {
        foreach (Enemy enemy in Pool)
            enemy.Reset();

        foreach (Enemy enemy in BusyObjects)
            enemy.Reset();

        base.Reset();
    }
}
