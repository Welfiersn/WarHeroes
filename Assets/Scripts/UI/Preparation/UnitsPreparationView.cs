using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UnitsPreparationView : MonoBehaviour, IUnitsPreparationView
    {
        [SerializeField] private TextMeshProUGUI _playerName;
        public TextMeshProUGUI PlayerName
        {
            get { return _playerName; }
            set { _playerName = value; }
        }

        [SerializeField] private TextMeshProUGUI _unitAmount;
        public TextMeshProUGUI UnitAmount
        {
            get { return _unitAmount; }
            set { _unitAmount = value; }
        }

        [SerializeField] private List<UnitPreparationView> units;
        public List<UnitPreparationView> UnitPreparationViews => units;
    }
}
