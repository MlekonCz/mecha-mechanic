using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Currency
{
   public class StoreUpgrades : MonoBehaviour
   {
      [SerializeField] private float upgradeCost = 250f;
      [SerializeField] private TMP_Text priceText;
      [SerializeField] private GameObject[] upgradesToUnlock;
      [SerializeField] private GameObject notEnoughMoneyText;
      private PlayerMoney _playerMoney;
      [SerializeField] private GameObject winUI;
      [SerializeField] private GameObject loseUI;
      private TimeManager _timeManager;
      private bool gameWon;

      private void Start()
      {
         _timeManager = FindObjectOfType<TimeManager>();
         priceText.text = upgradeCost.ToString() + "$";
         if (winUI == null || loseUI == null)
         {
            return;
         }
         winUI.SetActive(false);
         loseUI.SetActive(false);
      }

      private void Update()
      {
         // if (_timeManager.currentDay >= 8 && gameWon == false)
         // {
         //    loseUI.SetActive(true);
         // }
      }

      public void BuyUpgrade()
      {
         _playerMoney = FindObjectOfType<PlayerMoney>();
         if (_playerMoney.GetMoney() < upgradeCost)
         {
            GameObject refuseTX = Instantiate(notEnoughMoneyText, transform.position, Quaternion.identity);
            refuseTX.transform.parent = transform;
            Destroy(refuseTX, 2f);
            return;
         }

         foreach (GameObject upgrade in upgradesToUnlock)
         {
            upgrade.SetActive(true);
         }
         gameObject.GetComponent<Button>().interactable = false;
         _playerMoney.SetMoney(-upgradeCost);
      }

      public void RepayLoan()
      {
         _playerMoney = FindObjectOfType<PlayerMoney>();
         if (_playerMoney.GetMoney() < upgradeCost)
         {
            GameObject refuseTX = Instantiate(notEnoughMoneyText, transform.position, Quaternion.identity);
            refuseTX.transform.parent = transform;
            Destroy(refuseTX, 2f);
            return;
         }
         _playerMoney.SetMoney(-upgradeCost);
         winUI.SetActive(true);
         gameObject.GetComponent<Button>().interactable = false;
      }

      public void CloseWindow()
      {
         winUI.SetActive(false);
         loseUI.SetActive(false);
      }

      public void MainMenu()
      {
         SceneManager.LoadScene(0);
      }


   }
}
