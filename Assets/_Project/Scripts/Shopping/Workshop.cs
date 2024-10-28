using UnityEngine;

namespace CthulhuGame
{
    public class Workshop : MonoBehaviour
    {
        [SerializeField] private int _repairCost;
        
        private static Health _health;

        private static int _cost;

        private Collider2D _player;

        #region UnityEvents
        private void Start()
        {
            _health = Player.Instance.Ship.Health;
            _cost = _repairCost;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player")) // Временное решение.
            {
                _player = collision;

                Player.Instance.Ship.SendWorkshopMessage(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision == _player) // Временное решение.
            {
                Player.Instance.Ship.SendWorkshopMessage(false);

                _player = null;
            }
        }
        #endregion

        public static int CurrentRepairCost()
        {
            if (_health.CurrentHealth < _health.MaxHealth)
            {
                int currentRepairCost = _cost * _health.CurrentHealth / _health.MaxHealth;
                return currentRepairCost;
            }
            else
            {
                return 0;
            }
        }

        public static void TryRepairShip()
        {
            if (CurrentRepairCost() > 0)
            {
                var money = Player.Instance.Money;

                if (money.CurrentMoney >= CurrentRepairCost())
                {
                    money.TryChangeMoneyAmount(-Mathf.Abs(CurrentRepairCost()));

                    _health.RestoreHealth();
                }
            }
        }
    }
}