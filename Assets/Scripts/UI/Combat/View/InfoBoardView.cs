using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoBoardView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _unitNameText;
    public TextMeshProUGUI UnitNameText => _unitNameText;

    [SerializeField] private TextMeshProUGUI _healthUI;
    public TextMeshProUGUI HealthUI => _healthUI;

    [SerializeField] private TextMeshProUGUI _armorUI;
    public TextMeshProUGUI ArmorUI => _armorUI;

    [SerializeField] private TextMeshProUGUI _powerUI;
    public TextMeshProUGUI PowerUI => _powerUI;

    [SerializeField] private TextMeshProUGUI _energyUI;
    public TextMeshProUGUI EnergyUI => _energyUI;

    [SerializeField] private TextMeshProUGUI _distanceMoveUI;
    public TextMeshProUGUI DistanceMoveUI => _distanceMoveUI;

    [SerializeField] private TextMeshProUGUI _initiativeUI;
    public TextMeshProUGUI InitiativeUI => _initiativeUI;

    [SerializeField] private TextMeshProUGUI _descriptionUI;
    public TextMeshProUGUI DescriptionUI => _descriptionUI;
}
