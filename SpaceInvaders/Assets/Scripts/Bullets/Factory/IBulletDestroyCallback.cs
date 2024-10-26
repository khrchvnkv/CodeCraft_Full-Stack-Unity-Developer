namespace Bullets.Factory
{
    public interface IBulletDestroyCallback
    {
        void Destroy(in Bullet bullet);
    }
}