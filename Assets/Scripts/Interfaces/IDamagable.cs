namespace Assets.Scripts.Interfaces
{
    public interface IDamagable
    {
        void TakeDamage(int damage);
        bool IsFullyDamaged { get; }
    }
}
