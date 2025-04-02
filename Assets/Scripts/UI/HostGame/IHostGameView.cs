using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public interface IHostGameView : IView
    {
        public TextMeshProUGUI ErrorText { get; }

        public List<TMP_InputField> PlayersNamesField { get; }
        public List<TMP_Dropdown> PlayersFactionDropdown { get; }
    }
}
