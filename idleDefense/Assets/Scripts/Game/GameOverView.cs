using Zenject;

using DG.Tweening;

using Infrastructure;

using UnityEngine;

namespace Game.GameOver.View
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _gameOverPanel;

        [SerializeField] private float _animationDuration;

        private SceneLoader _sceneLoader;

        private Tween _fade;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void OnDestroy()
        {
            _fade.Kill();
        }

        public void FadeAnimation()
        {
            _gameOverPanel.gameObject.SetActive(true);

            if (_gameOverPanel == null)
                return;

            _fade = _gameOverPanel.DOFade(1, _animationDuration);

            _fade.OnComplete(() =>
            {
                _fade.Kill();

                _sceneLoader.LoadSceneAsync();
            });
        }
    }
}