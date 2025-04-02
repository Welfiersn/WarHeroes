using System.Collections.Generic;
using TMPro;

namespace Assets.Scripts.UI
{
    public interface IUnitsPreparationView
    {
        public TextMeshProUGUI PlayerName { get; set; }
        public TextMeshProUGUI UnitAmount { get; set; }
        public List<UnitPreparationView> UnitPreparationViews { get; }
    }
}
