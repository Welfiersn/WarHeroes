using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class PreparationView : MonoBehaviour, IPreparationView
    {
        [SerializeField] private CombatView _combatView;

        [SerializeField] private SpritesConfig _spritesConfig;
        [SerializeField] private UnitsConfig _unitsConfig;
        [SerializeField] private GameConfig _gameConfig;

        [SerializeField] private List<UnitsPreparationView> _unitsPreparationViews;
        public List<UnitsPreparationView> UnitsPreparationViews => _unitsPreparationViews;

        [SerializeField] private Button _playButton;

        private PreparationPresenter _presenter;

        private void OnEnable()
        {
            if (_presenter == null) _presenter = new PreparationPresenter(this);


            _playButton.onClick.AddListener(PlayButtonClicked);

            _presenter.Player1_OnUnitAmountChanged += Player1_UnitAmountChanged;
            _presenter.Player2_OnUnitAmountChanged += Player2_UnitAmountChanged;


            for (int i = 0; i < _unitsPreparationViews.Count; i++)
            {
                for (int j = 0; j < _unitsPreparationViews[i].UnitPreparationViews.Count; j++)
                {
                    IUnitPreparationView unitPreparationView = _unitsPreparationViews[i].UnitPreparationViews[j];

                    unitPreparationView.OnMinusButtonClicked += MinusButtonClicked;
                    unitPreparationView.OnPlusButtonClicked += PlusButtonClicked;

                    switch (unitPreparationView.PlayerNumber)
                    {
                        case PlayerNumber.Player1:
                            switch (unitPreparationView.TypeRole)
                            {
                                case TypeRole.WARRIOR:
                                    _presenter.Player1_OnWarriorAmountChanged += unitPreparationView.ChangeUnitAmount;
                                    break;
                                case TypeRole.ARCHER:
                                    _presenter.Player1_OnArcherAmountChanged += unitPreparationView.ChangeUnitAmount;
                                    break;
                                case TypeRole.MAGE:
                                    _presenter.Player1_OnMageAmountChanged += unitPreparationView.ChangeUnitAmount;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case PlayerNumber.Player2:
                            switch (unitPreparationView.TypeRole)
                            {
                                case TypeRole.WARRIOR:
                                    _presenter.Player2_OnWarriorAmountChanged += unitPreparationView.ChangeUnitAmount;
                                    break;
                                case TypeRole.ARCHER:
                                    _presenter.Player2_OnArcherAmountChanged += unitPreparationView.ChangeUnitAmount;
                                    break;
                                case TypeRole.MAGE:
                                    _presenter.Player2_OnMageAmountChanged += unitPreparationView.ChangeUnitAmount;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveAllListeners();

            _presenter.Player1_OnUnitAmountChanged -= Player1_UnitAmountChanged;
            _presenter.Player2_OnUnitAmountChanged -= Player2_UnitAmountChanged;

            for (int i = 0; i < _unitsPreparationViews.Count; i++)
            {
                for (int j = 0; j < _unitsPreparationViews[i].UnitPreparationViews.Count; j++)
                {
                    IUnitPreparationView unitPreparationView = _unitsPreparationViews[i].UnitPreparationViews[j];

                    unitPreparationView.OnMinusButtonClicked -= MinusButtonClicked;
                    unitPreparationView.OnPlusButtonClicked -= PlusButtonClicked;

                    switch (unitPreparationView.TypeRole)
                    {
                        case TypeRole.WARRIOR:
                            _presenter.Player1_OnWarriorAmountChanged -= unitPreparationView.ChangeUnitAmount;
                            break;
                        case TypeRole.ARCHER:
                            _presenter.Player1_OnArcherAmountChanged -= unitPreparationView.ChangeUnitAmount;
                            break;
                        case TypeRole.MAGE:
                            _presenter.Player1_OnMageAmountChanged -= unitPreparationView.ChangeUnitAmount;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void Init(List<IDataPlayer> dataPlayers)
        {
            InitUnits(dataPlayers);

            _presenter.Init(dataPlayers, _gameConfig, _unitsConfig);
        }

        private void InitUnits(List<IDataPlayer> dataPlayers)
        {
            for (int i = 0; i < dataPlayers.Count; i++)
            {
                _unitsPreparationViews[i].PlayerName.text = dataPlayers[i].Name;
                _unitsPreparationViews[i].UnitAmount.text = "Можно взять: " + _gameConfig.FieldHeight.ToString();

                for (int j = 0; j < _unitsPreparationViews[i].UnitPreparationViews.Count; j++)
                {
                    IUnitPreparationView unitPreparationView = _unitsPreparationViews[i].UnitPreparationViews[j];

                    unitPreparationView.UnitAmount.text = "x0";
                    unitPreparationView.Image.sprite = _spritesConfig.Configs[(int)dataPlayers[i].Faction * 3 + j].Sprite;
                }
            }
        }

        public void PlayButtonClicked()
        {
            _combatView.Show();
            _combatView.Init(_presenter.Players);
            Hide();
        }

        public void PlusButtonClicked(object sender, IUnitPreparationView unitPreparationView)
        {
            _presenter.AddUnit(unitPreparationView);
        }

        public void MinusButtonClicked(object sender, IUnitPreparationView unitPreparationView)
        {
            _presenter.RemoveUnit(unitPreparationView);
        }

        public void Player1_UnitAmountChanged(object sender, int amount)
        {
            _unitsPreparationViews[0].UnitAmount.text = "Можно взять: " + amount.ToString();
        }

        public void Player2_UnitAmountChanged(object sender, int amount)
        {
            _unitsPreparationViews[1].UnitAmount.text = "Можно взять: " + amount.ToString();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}