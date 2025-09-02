using Project.Scripts.Characters.Damage;

namespace Project.Scripts.Weapon
{
    public abstract class Weapon
    {
        protected int Damage;
        protected DamageType DamageType;

        protected Weapon(int damage, DamageType damageType)
        {
            Damage = damage;
            DamageType = damageType;
        }
    }
}