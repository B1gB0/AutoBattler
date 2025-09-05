using UnityEngine;

namespace Project.Scripts.Characters
{
    public abstract class Character : MonoBehaviour
    {
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Health.Health Health{ get; private set; }
        
        public int Damage { get; private set; }
        public int Power { get; private set; }
        public int Endurance { get; private set; }
        public int Agility { get; private set; }

        public void Construct(float healthValue, int damage, int power, int endurance, int agility)
        {
            Health.SetHealthValue(healthValue);
            Damage = damage;
            Power = power;
            Endurance = endurance;
            Agility = agility;
        }
    }
}