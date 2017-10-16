using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manifold
{
    public class ToggleSwitch : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Action();
            }
        }

        public virtual void Action()
        {

        }
    }
}
