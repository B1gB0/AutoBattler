using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts.Services
{
    public class BattleService : Service, IBattleService
    {
        private Transform _playerPosition;
        private Transform _playerAttackPosition;
        
        private Transform _enemyPosition;
        private Transform _enemyAttackPosition;

        // public override UniTask Init()
        // {
        //     
        // }
    }
}
