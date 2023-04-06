using Game.GameOver.View;
using Zenject;

namespace Game.GameOver
{
    public class GameOver
    {
        private GameOverView _view;

        [Inject]
        public GameOver(GameOverView view)
        {
            _view = view;
        }

        public void FinishGame()
        {
            _view.FadeAnimation();
        }
    }
}