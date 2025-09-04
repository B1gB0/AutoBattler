using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private GameObject _rootBackground;
        [SerializeField] private Button _battleButton;

        public event Action OnStartBattle;

        private void Start()
        {
            _battleButton.onClick.AddListener(OnBattleButtonClicked);
        }

        public void ShowButton()
        {
            _battleButton.gameObject.SetActive(true);
        }
        
        public void HideButton()
        {
            _battleButton.gameObject.SetActive(false);
        }
        
        public void ShowBackground()
        {
            _rootBackground.gameObject.SetActive(true);
        }

        public void HideBackground()
        {
            _rootBackground.gameObject.SetActive(false);
        }

        private void OnBattleButtonClicked()
        {
            OnStartBattle?.Invoke();
        }
    }
}