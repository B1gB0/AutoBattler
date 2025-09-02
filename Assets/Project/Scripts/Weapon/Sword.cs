using Project.Scripts.Characters.Damage;

namespace Project.Scripts.Weapon
{
    public class Sword : Weapon
    {
        public Sword(int damage, DamageType damageType) : base(damage, DamageType.Chopping)
        {
        }
    }
}