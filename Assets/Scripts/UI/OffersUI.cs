using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class OffersUI : MonoBehaviour
    {
        /// <summary>
        /// Going to rewrite whole customer logic later so I wont do anything here yet
        /// </summary>
        [SerializeField] private GameObject offerUI;
        
        [Header("Offer 1")]
        [SerializeField] private TMP_Text offer1Text = null;
        [SerializeField] private TMP_Text reward1Text = null;
        private bool _activeFirstOffer;
        
        [Header("Offer 2")]
        [SerializeField] private TMP_Text offer2Text = null;
        [SerializeField] private TMP_Text reward2Text = null;
        private bool _activeSecondOffer;

        private void Update()
        {
            if (!_activeFirstOffer && !_activeSecondOffer)
            {
                offerUI.SetActive(false);
            }
            else
            {
                offerUI.SetActive(true);
            }
        }

        public bool ShowFirstOffer(float money, List<String> attributes)
        {
            if (_activeFirstOffer)
            {
                return false;
            }

            _activeFirstOffer = true;
            if (attributes.Count == 0)
            {
                offer1Text.text = "1. Whatever mech will be fine.";
            }
            else
            {
                offer1Text.text = "1. Mech attributes needs to be: " + string.Join(", ", attributes);
            }
            reward1Text.text = "Reward: " + money.ToString("F1") + "$";
            
            return true;
        }

        public void HideFirstOffer()
        {
            _activeFirstOffer = false;
            offer1Text.text = "";
            reward1Text.text = "";
        }
        
        public bool ShowSecondOffer(float money, List<String> attributes)
        {
            if (_activeSecondOffer)
            {
                return false;
            }

            _activeSecondOffer = true;
            
            if (attributes.Count == 0)
            {
                offer2Text.text = "2. Whatever mech will be fine.";
            }
            else
            {
                offer2Text.text = "2. Mech attributes needs to be: " + string.Join(", ", attributes);
            }
            reward2Text.text = "Reward: " + money.ToString("F1") + "$";
            return true;
        }
        
        public void HideSecondOffer()
        {
            _activeSecondOffer = false;
            offer2Text.text = "";
            reward2Text.text = "";
        }
        
    }
}
