using UnityEngine;
using System;

namespace CthulhuGame
{
    public class Money : MonoBehaviour
    {
        [SerializeField] private int _currentMoney;
        public int CurrentMoney => _currentMoney;

        public event Action OnMoneyChanged;

        public void TryChangeMoneyAmount(int amount)
        {            
            if (amount != 0)
            {
                int currentMoney = _currentMoney + amount;

                if (currentMoney >= 0)
                {
                    _currentMoney = currentMoney;

                    OnMoneyChanged?.Invoke();
                }
            }
        }
    }
}