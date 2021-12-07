using System;
using MechParts;
using MechPartStates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StructureCanvas : MonoBehaviour
    {
        [SerializeField] private TMP_Text _statusOfPart;
        [SerializeField] private TMP_Text _nameOfPart;
        
        [SerializeField] private Button _leaveButton;
        [SerializeField] private Button _removeButton;

        public event Action onLeaveClicked;
        public event Action onRemoveClicked;
        private void Awake()
        {
            _leaveButton.onClick.AddListener(OnLeaveClicked);
            _removeButton.onClick.AddListener(OnRemoveClicked);
        }

        public void UpdateStatusOfPart(string status, string nameOfPart)
        {
            _statusOfPart.text = "Status: " + status;
            _nameOfPart.text = "Name: " + nameOfPart;
        }
        private void OnLeaveClicked()
        {
          onLeaveClicked?.Invoke();
        }
        private void OnRemoveClicked()
        {
          onRemoveClicked?.Invoke();
        }

        
        private void OnDestroy()
        {
            _leaveButton.onClick.RemoveAllListeners();
            _removeButton.onClick.RemoveAllListeners();
        }
    }
}
