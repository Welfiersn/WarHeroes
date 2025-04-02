using System.Collections.Generic;

namespace Assets.Scripts.UI
{
    public interface IPreparationView : IView
    {
        public List<UnitsPreparationView> UnitsPreparationViews { get; }
    }
}
