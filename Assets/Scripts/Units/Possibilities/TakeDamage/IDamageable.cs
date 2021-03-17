namespace Units.Possibilities.TakeDamage
{
    public interface IDamageable
    {
        void TakeDamage(Unit aggressor, Unit victim, float damage);
        void TakeDamage(Unit aggressor, Unit victim, float damage, bool onlyHealth);
        void Counterattack(Unit aggressor, Unit victim);
    }
}