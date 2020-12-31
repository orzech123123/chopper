namespace Assets.Scripts.Interfaces
{
    public interface IDamagable
    {
        void TakeDamage(int damage);
        int CurrentHealth { get; }
        int TotalHealth { get; }
    }
}
