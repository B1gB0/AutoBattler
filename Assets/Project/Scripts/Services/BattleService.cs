using System;
using Project.Scripts.Characters.Enemy;
using Project.Scripts.UI;
using Reflex.Attributes;
using UnityEngine;

namespace Project.Scripts.Services
{
    public class BattleService : Service, IBattleService, IDisposable
    {
        private Transform _playerPosition;
        private Transform _playerAttackPosition;
        
        private Transform _enemyPosition;
        private Transform _enemyAttackPosition;

        private Enemy _currentEnemy;

        private bool _isBattle = false;

        private UIRoot _uiRoot;

        [Inject]
        private void Construct(IEnemyService enemyService)
        {
            
        }

        public void GetAllParams(UIRoot uiRoot, Transform playerPosition, Transform playerAttackPosition,
            Transform enemyPosition, Transform enemyAttackPosition)
        {
            _uiRoot = uiRoot;
            _playerPosition = playerPosition;
            _playerAttackPosition = playerAttackPosition;
            _enemyPosition = enemyPosition;
            _enemyAttackPosition = enemyAttackPosition;
            
            _uiRoot.OnStartBattle += StartBattle;
        }

        public void GetCurrentEnemy(Enemy enemy)
        {
            _currentEnemy = enemy;
        }

        private async void StartBattle()
        {
            _isBattle = true;

            while (_isBattle)
            {
                
            }
        }

        public void Dispose()
        {
            _uiRoot.OnStartBattle -= StartBattle;
        }
    }
}
