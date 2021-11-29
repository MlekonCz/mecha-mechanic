using MechParts;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Currency
{
    public class BuyPartButton : MonoBehaviour
    {
        [FormerlySerializedAs("bodyPart")] [SerializeField] private MechPartDefinition _mechPart = null;
        [SerializeField] private TMP_Text buyText;
        [SerializeField] private Transform deliveryLocation;
        [SerializeField] private GameObject notEnoughMoneyText;
        private PlayerMoney _playerMoney;
        private float _cost;
        
        private void Start()
        {
            _cost = _mechPart.cost;
            buyText.text ="Buy for " + _cost.ToString("F1") + " $";
        }
        
        public void BuyPart()
        {
            _playerMoney = FindObjectOfType<PlayerMoney>();
            if (_playerMoney.GetMoney() < _cost)
            {
               GameObject refuseTX = Instantiate(notEnoughMoneyText, transform.position, Quaternion.identity);
               refuseTX.transform.parent = transform;
                Destroy(refuseTX, 2f);
                return;
            }
            // _mechPart.DeliverPart(deliveryLocation);
            _playerMoney.SetMoney(-_cost);
        }

    }
}
