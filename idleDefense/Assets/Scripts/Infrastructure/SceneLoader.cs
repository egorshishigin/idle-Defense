using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader : MonoBehaviour
    {
        private const int SceneIndex = 0;

        public void LoadSceneAsync()
        {
            StartCoroutine(SceneLoading());
        }

        private IEnumerator SceneLoading()
        {
            AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(SceneIndex);

            while (!sceneLoad.isDone)
            {
                yield return null;
            }
        }
    }
}