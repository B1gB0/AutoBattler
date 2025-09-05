using Project.Scripts.Characters.Damage;

namespace Project.Scripts.Weapon
{
    public class Weapon
    {
        public Weapon(int damage, DamageType damageType, string name)
        {
            Damage = damage;
            DamageType = damageType;
            Name = name;
        }
        
        public int Damage { get; private set; }
        public DamageType DamageType { get; private set; }
        public string Name { get; private set; }
    }
}