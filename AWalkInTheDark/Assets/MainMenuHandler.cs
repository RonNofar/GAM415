using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manifold
{
    public class MainMenuHandler : MonoBehaviour
    {
        [SerializeField] BlackScreenFade _BSF;

        public void OnStartButton()
        {
            _BSF.StartCoroutine(
                _BSF.StartSequence(
                    () => { StartGame(); }));
        }

        private void StartGame()
        {
            SceneManager.LoadScene(2);
        }
    }
}
