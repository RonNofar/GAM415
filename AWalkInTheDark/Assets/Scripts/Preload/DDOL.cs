using UnityEngine;

namespace Manifold.Preload
{
    public class DDOL : MonoBehaviour
    {

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
