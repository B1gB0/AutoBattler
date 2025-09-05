namespace Project.Scripts.Characters.Enemy
{
    public abstract class Enemy : Character
    {
        public string RewardedWeaponId { get; private set; }

        public void Construct(float healthValue, int damage, int power, int endurance, int agility, string rewardedWeaponId)
        {
            base.Construct(healthValue, damage, power, endurance, agility);
            RewardedWeaponId = rewardedWeaponId;
        }
    }
}