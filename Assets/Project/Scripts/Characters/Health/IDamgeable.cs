using System;

namespace Project.Scripts.Characters.Health
{
    public interface IDamageable
    {
        event Action Die;

        void TakeDamage(float damage);
    }
}