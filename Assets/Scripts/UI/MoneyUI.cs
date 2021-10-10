
using System;
using Currency;
using TMPro;
using UnityEngine;

namespace UI
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text moneyText;
        private float currentMoney;

        private void Start()
        {
            moneyText.text = currentMoney.ToString();
        }

        public void UpdateMoneyUI(float newMoney) //add later animation for decreasing money
        {
            currentMoney = newMoney;
            moneyText.text = currentMoney.ToString("F2");
        }

        
    }
}
