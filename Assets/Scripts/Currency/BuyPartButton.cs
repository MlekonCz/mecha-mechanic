using MechParts;
using TMPro;
using UnityEngine;

namespace Currency
{
    public class BuyPartButton : MonoBehaviour
    {
        [SerializeField] private BodyPartDefinition bodyPart = null;
        [SerializeField] private TMP_Text buyText;
        [SerializeField] private Transform deliveryLocation;
        [SerializeField] private GameObject notEnoughMoneyText;
        private PlayerMoney _playerMoney;
        private float _cost;
        
        private void Start()
        {
            _cost = bodyPart.cost;
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
            bodyPart.DeliverPart(deliveryLocation);
            _playerMoney.SetMoney(-_cost);
        }

    }
}
