using Assets.Scripts.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBoardView : MonoBehaviour , IView
{
    [SerializeField] private Button _abilityFirstButton;
    [SerializeField] private Button _abilitySecondButton;

    [SerializeField] private TextMeshProUGUI _abilityFirstText;
    [SerializeField] private TextMeshProUGUI _abilitySecondText;

    public event EventHandler<int> OnAbilityChanged;

    private void Awake()
    {
        _abilityFirstButton.onClick.AddListener(ChangeFirstAbility);
        _abilitySecondButton.onClick.AddListener(ChangeSecondAbility);
    }

    private void ChangeFirstAbility()
    {
        _abilityFirstButton.interactable = false;
        _abilitySecondButton.interactable = true;

        _abilitySecondButton.image.color = Color.white;
        _abilityFirstButton.image.color = Color.gray;

        OnAbilityChanged?.Invoke(this, 0);
    }

    private void ChangeSecondAbility()
    {
        _abilitySecondButton.interactable = false;
        _abilityFirstButton.interactable = true;

        _abilityFirstButton.image.color = Color.white;
        _abilitySecondButton.image.color = Color.gray;

        OnAbilityChanged?.Invoke(this, 1);
    }

    public void Show()
    {
        gameObject.SetActive(true);

        _abilitySecondButton.image.color = Color.white;
        _abilityFirstButton.image.color = Color.white;

        _abilityFirstButton.interactable = true;
        _abilitySecondButton.interactable = true;
    }

    public void Init(List<IActiveAbility> abilities)
    {
        _abilityFirstText.text = abilities[0].Name;
        _abilitySecondText.text = abilities[1].Name;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
