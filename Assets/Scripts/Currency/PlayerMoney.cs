using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Currency
{
    public class PlayerMoney : MonoBehaviour
    {
        private float _currentMoney = 0f;
        
        public float GetMoney()
        {
            return _currentMoney;
        }

        public void SetMoney(float moneyChange)
        {
            _currentMoney += moneyChange;
            foreach (MoneyUI moneyUI in FindObjectsOfType<MoneyUI>())
            {
                moneyUI.UpdateMoneyUI(_currentMoney);
            }
        }
       
    }
}