using Project.Scripts.Characters.Enemy;
using Project.Scripts.UI;
using UnityEngine;

namespace Project.Scripts.Services
{
    public interface IBattleService
    {
        public void GetAllParams(UIRoot uiRoot, Transform playerPosition, Transform playerAttackPosition,
            Transform enemyPosition, Transform enemyAttackPosition);

        public void GetCurrentEnemy(Enemy enemy);
        
        public void Dispose();
    }
}