using UnityEngine;

namespace Manifold.Preload
{
    public class App : MonoBehaviour
    {
        static protected App _instance;
        static public App Instance { get { return _instance; } }

        [HideInInspector] public string loadScene = null;

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.LogWarning("App is already in play. Deleting new!", gameObject);
                Destroy(gameObject);
            }
            else
            { _instance = this; }
        }

        private void Start()
        {
            if (loadScene == null)
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            else
                UnityEngine.SceneManagement.SceneManager.LoadScene(loadScene);
        }
    }
}
