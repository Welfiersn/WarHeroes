using System;
using System.Collections.Generic;

namespace Assets.Scripts.UI
{
    public class HostGamePresenter
    {
        private IHostGameView _view;

        private string _errorText;

        public event EventHandler<string> OnErrorTextChanged;
        public Action<List<IDataPlayer>> OnDataApproved;

        public HostGamePresenter(IHostGameView hostGameUIView)
        {
            _view = hostGameUIView;
        }

        public void OnPlayButtonClicked()
        {
            List<IDataPlayer> dataPlayers = new List<IDataPlayer>();

            if (IsCorrectData() == false) return;



            for (int i = 0; i < _view.PlayersNamesField.Count; i++)
            {
                dataPlayers.Add(new DataPlayer(_view.PlayersNamesField[i].text,
                   (TypeFaction)_view.PlayersFactionDropdown[i].value));
            }

            OnDataApproved?.Invoke(dataPlayers);
        }

        private bool IsCorrectData()
        {
            _errorText = string.Empty;

            for (int i = 0; i < _view.PlayersNamesField.Count; i++)
            {
                int playerNumber = i + 1;

                if (string.IsNullOrEmpty(_view.PlayersNamesField[i].text))
                {
                    _errorText = $"Имя игрока №{playerNumber} - пустое";
                    OnErrorTextChanged?.Invoke(this, _errorText);
                    return false;
                }
            }

            _errorText = string.Empty;
            OnErrorTextChanged?.Invoke(this, _errorText);

            return true;
        }
    }
}
