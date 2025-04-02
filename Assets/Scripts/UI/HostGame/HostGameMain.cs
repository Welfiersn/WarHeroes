using UnityEngine;

namespace Assets.Scripts.UI
{
    public class HostGameMain : MonoBehaviour
    {
        [SerializeField] private HostGameView _gameUIView;

        public void Awake()
        {
            var view = _gameUIView;
            HostGamePresenter presenter = new HostGamePresenter(view);

            view.Init(presenter);
        }
    }
}
