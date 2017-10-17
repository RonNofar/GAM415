using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manifold
{
    public class MainMenuHandler : MonoBehaviour
    {
        public void OnStartButton()
        {
            SceneManager.LoadScene(2);
        }
    }
}
