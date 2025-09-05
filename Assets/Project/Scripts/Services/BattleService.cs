using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.Characters;
using Project.Scripts.Characters.Enemy;
using Project.Scripts.Characters.Player;
using Project.Scripts.UI;
using Reflex.Attributes;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Scripts.Services
{
    public class BattleService : Service, IBattleService, IDisposable
    {
        private Transform _playerPosition;
        private Transform _playerAttackPosition;
        private Transform _enemyPosition;
        private Transform _enemyAttackPosition;

        private IEnemyService _enemyService;
        private Player _player;
        private Enemy _currentEnemy;
        private bool _isBattle;
        private UIRoot _uiRoot;
        private bool _isPlayerTurn;
        private CancellationTokenSource _battleCancellationTokenSource;

        [Inject]
        private void Construct(IEnemyService enemyService)
        {
            _enemyService = enemyService;
        }
        
        public int CounterMoves { get; private set; }
        public event Action OnVictoryPlayer;
        public event Action OnBattleCompleted;

        public void GetAllParams(UIRoot uiRoot, Transform playerPosition, Transform playerAttackPosition,
            Transform enemyPosition, Transform enemyAttackPosition, Player player)
        {
            _uiRoot = uiRoot;
            _playerPosition = playerPosition;
            _playerAttackPosition = playerAttackPosition;
            _enemyPosition = enemyPosition;
            _enemyAttackPosition = enemyAttackPosition;
            _player = player;
            
            _uiRoot.OnStartBattle += StartBattle;
        }

        private void StartBattle()
        {
            _isBattle = true;
            _currentEnemy = _enemyService.CurrentEnemy;
            
            _battleCancellationTokenSource = new CancellationTokenSource();
            
            _isPlayerTurn = _player.Character.Agility >= _currentEnemy.Agility;
            
            BattleLoop(_battleCancellationTokenSource.Token).Forget();
        }

        private async UniTaskVoid BattleLoop(CancellationToken cancellationToken)
        {
            try
            {
                while (_isBattle && !cancellationToken.IsCancellationRequested)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: cancellationToken);
                    
                    if (_isPlayerTurn)
                    {
                        await PlayerAttack(cancellationToken);
                    }
                    else
                    {
                        await EnemyAttack(cancellationToken);
                    }
                    
                    if (_player.Character.Health.TargetHealth <= 0)
                    {
                        Debug.Log("Игрок проиграл!");
                        _isBattle = false;
                        OnBattleEnded(false).Forget();
                        break;
                    }
                    
                    if (_currentEnemy.Health.TargetHealth <= 0)
                    {
                        Debug.Log("Игрок победил!");
                        _isBattle = false;
                        OnBattleEnded(true).Forget();
                        break;
                    }
                    
                    _isPlayerTurn = !_isPlayerTurn;
                    CounterMoves++;
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Бой прерван");
            }
        }

        private async UniTask PlayerAttack(CancellationToken cancellationToken)
        {
            Debug.Log("Ход игрока");
            
            // Перемещение к точке атаки
            await MoveToPosition(_player.Character.transform, _playerAttackPosition.position, 0.3f, cancellationToken);
            
            // Анимация атаки игрока
            _player.Character.Animator.SetTrigger("Attack");
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: cancellationToken);
            
            // Выполняем атаку
            await PerformAttack(_player.Character, _currentEnemy, cancellationToken);
            
            // Возвращение на исходную позицию
            await MoveToPosition(_player.Character.transform, _playerPosition.position, 0.3f, cancellationToken);
        }

        private async UniTask EnemyAttack(CancellationToken cancellationToken)
        {
            Debug.Log("Ход врага");
            
            // Перемещение к точке атаки
            await MoveToPosition(_currentEnemy.transform, _enemyAttackPosition.position, 0.3f, cancellationToken);
            
            // Анимация атаки врага
            _currentEnemy.Animator.SetTrigger("Attack");
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: cancellationToken);
            
            // Выполняем атаку
            await PerformAttack(_currentEnemy, _player.Character, cancellationToken);
            
            // Возвращение на исходную позицию
            await MoveToPosition(_currentEnemy.transform, _enemyPosition.position, 0.3f, cancellationToken);
        }

        private async UniTask MoveToPosition(Transform objectToMove, Vector3 targetPosition, float duration, CancellationToken cancellationToken)
        {
            Vector3 startPosition = objectToMove.position;
            float elapsedTime = 0f;
            
            while (elapsedTime < duration && !cancellationToken.IsCancellationRequested)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                objectToMove.position = Vector3.Lerp(startPosition, targetPosition, t);
                await UniTask.Yield(cancellationToken);
            }
            
            // Убеждаемся, что объект точно находится в целевой позиции
            if (!cancellationToken.IsCancellationRequested)
            {
                objectToMove.position = targetPosition;
            }
        }

        private async UniTask PerformAttack(Character attacker, Character defender, CancellationToken cancellationToken)
        {
            // 1. Расчет шанса попадания
            int randomValue = UnityEngine.Random.Range(1, attacker.Agility + defender.Agility + 1);
            
            if (randomValue <= defender.Agility)
            {
                Debug.Log($"{attacker.name} промахнулся!");

                return; // Атака промахнулась
            }
            
            Debug.Log($"{attacker.name} попал по {defender.name}!");
            
            // 2. Расчет изначального урона
            int damage = attacker.Damage + attacker.Power;
            
            // 3. Применяем эффекты на атаки атакующего (заглушка)
            // damage = ApplyAttackerEffects(attacker, damage);
            
            // 4. Применяем эффекты на урон цели (заглушка)
            // damage = ApplyDefenderEffects(defender, damage);
            
            // 5. Наносим урон
            if (damage > 0)
            {
                defender.TakeDamage(damage);
                Debug.Log($"{attacker.name} нанес {damage} урона {defender.name}!");
                
                defender.Animator.SetTrigger("TakeDamage");

                await UniTask.Delay(TimeSpan.FromSeconds(0.3f), cancellationToken: cancellationToken);
            }
        }

        private async UniTaskVoid OnBattleEnded(bool playerWon)
        {
            _isBattle = false;
            
            if (playerWon)
            {
                //_uiRoot.ShowVictoryMessage();
                
                // Награда за победу
                await HandleVictoryRewards();
            }
            else
            {
                //_uiRoot.ShowDefeatMessage();
                
                // Обработка поражения
                await HandleDefeat();
            }
            
            // Возвращаем персонажей на исходные позиции
            await UniTask.WhenAll(
                MoveToPosition(_player.Character.transform, _playerPosition.position, 0.3f, _battleCancellationTokenSource.Token)
            );
            
            OnBattleCompleted?.Invoke();
        }

        private async UniTask HandleVictoryRewards()
        {
            // Выдаем награду за победу
            int expGained = _currentEnemy.Power * 10;
            //_player.AddExperience(expGained);
            
            // Показываем полученную награду
            //_uiRoot.ShowRewardMessage(expGained, _currentEnemy.RewardedWeaponId);
            
            await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: _battleCancellationTokenSource.Token);
            
            OnVictoryPlayer?.Invoke();
            
            _player.Character.SetStartHealth();
        }

        private async UniTask HandleDefeat()
        {
            // Обработка поражения
            await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: _battleCancellationTokenSource.Token);
            
            // Возможно, восстановление здоровья игрока или другие действия
            _player.Character.SetStartHealth();
        }

        public void Dispose()
        {
            _uiRoot.OnStartBattle -= StartBattle;
            
            // Отменяем текущий бой при уничтожении сервиса
            _battleCancellationTokenSource?.Cancel();
            _battleCancellationTokenSource?.Dispose();
        }
    }
}