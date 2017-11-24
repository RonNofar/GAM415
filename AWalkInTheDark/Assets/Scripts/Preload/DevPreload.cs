using UnityEngine;

// this should run absolutely first; use script execution order to do so.
// (of course normally never, ever, use the script execution order feature,
// this is an unusual case - just for development.)

namespace Manifold.Preload
{
    public class DevPreload : MonoBehaviour
    {
        void Awake()
        {
            GameObject check = GameObject.Find("__app");
            if (check == null)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("_preload");
                //App.Instance.loadScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            }
        }
    }
}
