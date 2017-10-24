using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manifold
{
    public class Turn : MonoBehaviour
    {
        [SerializeField] Vector3 RotationVector;
        private Transform m_Transform;

        private void Awake()
        {
            m_Transform = GetComponent<Transform>();
        }
        private void FixedUpdate()
        {
            m_Transform.Rotate(RotationVector);
        }
    }
}
