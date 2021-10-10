using System;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

namespace Customers
{
    public class CustomerCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject canvas = null;
        [SerializeField] private TMP_Text customerBasicText = null;
        [SerializeField] private TMP_Text customerText;
        [SerializeField] private Customer customer;

        [SerializeField] private GameObject acceptButton;
        [SerializeField] private GameObject sellButton;

        [SerializeField] private GameObject refuseText;
        [SerializeField] private GameObject offer1;
        [SerializeField] private GameObject offer2;
        private List<string> _namesOfAttributes = new List<string>();
        
        private bool _offerIsActive = false;
        private bool _secondOfferActive = false;
        private float _priceForMech;
        private bool _updatedDialogue = false;
        private bool _stoppedShopping = false;
        

        public void SetCustomerCanvas(List<string> list, float price)
        {
            _priceForMech = price;
            UpdateListOfAttributes(list);

            if (_namesOfAttributes.Count == 0)
            {
                customerBasicText.text = "Good day.I would love to buy a mech. I will pay you for it : " +
                                         _priceForMech.ToString("F1") + "$";
            }
            else
            {
                customerBasicText.text = "Good day.I would love to buy a mech. " +
                                         "I need him to have these attributes: " +
                                         string.Join(", ", _namesOfAttributes)   +  " \nI will pay you for this mech if you fulfill my conditions: " +
                                         _priceForMech.ToString("F1") + "$";
            }
           
        }
        

        
        
        public void OpenWindow(float price)
        {
            if (_stoppedShopping){return;}
            if (!_offerIsActive)
            {
                sellButton.SetActive(false);
            }
            canvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
           
        }

        private void UpdateListOfAttributes(List<string> list)
        {
            if (!_updatedDialogue)
            {
                _namesOfAttributes = list;
                _updatedDialogue = true;
            }
        }

        public void CloseWindow()
        {
            canvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        public void AcceptOffer()
        {
            if (FindObjectOfType<OffersUI>().ShowFirstOffer(_priceForMech, _namesOfAttributes))
            {
                acceptButton.SetActive(false);
                sellButton.SetActive(true);
                _offerIsActive = true;
                offer1.SetActive(true);
                return;
            }
            else if (FindObjectOfType<OffersUI>().ShowSecondOffer(_priceForMech, _namesOfAttributes))
            {
                acceptButton.SetActive(false);
                sellButton.SetActive(true);
                _offerIsActive = true;
                _secondOfferActive = true;
                offer2.SetActive(true);
            }
            else
            {
                return;
            }
            
        }

        void StopShopping()
        {
            customer.StopShopping();
            _stoppedShopping = true;
            
            if ( Cursor.lockState == CursorLockMode.Locked){return;}
            canvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void RejectOffer()
        {
            customerText.text = "I hope I will have better luck next time.";
            Invoke(nameof(StopShopping),1f);
            HideOffer();
        }
        public void SellMech()
        {
            if (!customer.SellMech())
            {
                GameObject refuseTx = Instantiate(refuseText, sellButton.transform.position, Quaternion.identity);
                refuseTx.transform.parent = sellButton.transform;
                Destroy(refuseTx, 2f);
                return;
            }
            
            customerText.text = "Thank you very much for selling me your Mech.";
            Invoke(nameof(StopShopping),1.5f);
            HideOffer();
            
        }

        void HideOffer()
        {
            if (!_secondOfferActive)
            {
                FindObjectOfType<OffersUI>().HideFirstOffer();
                _secondOfferActive = false;
                offer1.SetActive(false);
            }
            else
            {
                FindObjectOfType<OffersUI>().HideSecondOffer();
                offer2.SetActive(false);
            } 
        }

        void RestartRefuseText()
        {
            refuseText.SetActive(false);
        }
        
        
        
        void DisplayText() 
        {
            //later add logic for different dialogues
        }
    }
}
