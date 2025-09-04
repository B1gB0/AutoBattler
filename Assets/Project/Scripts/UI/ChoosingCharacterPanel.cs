using System;
using System.Collections.Generic;
using Project.Scripts.DataBase.Data;
using Project.Scripts.UI.View;
using UnityEngine;

namespace Project.Scripts.UI
{
    public class ChoosingCharacterPanel : MonoBehaviour, IView
    {
        [SerializeField] private List<CardView> _characterViews = new ();

        public event Action OnCharacterButtonClicked;
        public event Action<PlayerClassesData> OnCharacterDataGetted;

        public void Construct(List<PlayerClassesData> data)
        {
            for (int i = 0; i < _characterViews.Count; i++)
            {
                _characterViews[i].GetData(data[i]);
            }
        }

        private void OnEnable()
        {
            foreach (var character in _characterViews)
            {
                character.GetCharacterButtonClicked += OnButtonClicked;
            }
        }

        private void OnDisable()
        {
            foreach (var character in _characterViews)
            {
                character.GetCharacterButtonClicked -= OnButtonClicked;
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnButtonClicked(PlayerClassesData data)
        {
            OnCharacterButtonClicked?.Invoke();
            OnCharacterDataGetted?.Invoke(data);
            gameObject.SetActive(false);
        }
    }
}