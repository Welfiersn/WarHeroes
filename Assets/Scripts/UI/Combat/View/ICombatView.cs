using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Combat
{
    public interface ICombatView : IView
    {
        public InfoBoardView InfoBoardView { get; }

        public AbilityBoardView AbilityBoardView { get; }

        public EndGameView EndGameView { get; }

        public TextMeshProUGUI RoundTitle { get; }
        public TextMeshProUGUI PlayerTitle { get; }

        public Button AbilityButton { get; }
        public Button MoveButton { get; }
        public Button SkipButton { get; }
        public Button QuitButton { get; }

        public CellView[,] CellsViews { get; }
    }
}
