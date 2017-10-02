using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manifold
{
    [ExecuteInEditMode]
    public class FatherLord : MonoBehaviour
    {
        public void KillChildren()
        {
            Debug.Log("Killing...");
            foreach (Transform child in transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }
}
