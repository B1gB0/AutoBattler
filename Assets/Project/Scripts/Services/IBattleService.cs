using System;
using Project.Scripts.Characters.Player;
using Project.Scripts.UI;
using UnityEngine;

namespace Project.Scripts.Services
{
    public interface IBattleService
    {
        public void GetAllParams(UIRoot uiRoot, Transform playerPosition, Transform playerAttackPosition,
            Transform enemyPosition, Transform enemyAttackPosition, Player player);

        public void Dispose();
        public event Action OnVictoryPlayer;
        public event Action OnBattleCompleted;
    }
}