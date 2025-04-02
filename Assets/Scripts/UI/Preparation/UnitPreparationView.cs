using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UnitPreparationView : MonoBehaviour, IUnitPreparationView
    {
        [SerializeField] private PlayerNumber _playerNumber;
        public PlayerNumber PlayerNumber => _playerNumber;

        [SerializeField] private TypeRole _typeRole;
        public TypeRole TypeRole => _typeRole;

        [SerializeField] private Image _image;
        public Image Image
        {
            get { return _image; }
            set { _image = value; }
        }

        [SerializeField] private Button _minusButton;
        public Button MinusButton => _minusButton;

        [SerializeField] private Button _plusButton;
        public Button PlusButton => _plusButton;

        [SerializeField] private TextMeshProUGUI _unitAmount;
        public TextMeshProUGUI UnitAmount
        {
            get { return _unitAmount; }
            set { _unitAmount = value; }
        }


        public event EventHandler<IUnitPreparationView> OnMinusButtonClicked;
        public event EventHandler<IUnitPreparationView> OnPlusButtonClicked;

        public void ChangeUnitAmount(object sender, int unitAmount)
        {
            _unitAmount.text = "x" + unitAmount.ToString();
        }

        private void OnEnable()
        {
            _minusButton.onClick.AddListener(MinusButtonClicked);
            _plusButton.onClick.AddListener(PlusButtonClicked);
        }

        private void OnDisable()
        {
            _minusButton.onClick.RemoveAllListeners();
            _plusButton.onClick.RemoveAllListeners();
        }

        private void MinusButtonClicked()
        {
            OnMinusButtonClicked?.Invoke(this, this);
        }

        private void PlusButtonClicked()
        {
            OnPlusButtonClicked?.Invoke(this, this);
        }
    }
}
