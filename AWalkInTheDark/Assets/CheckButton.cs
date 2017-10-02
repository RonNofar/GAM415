using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manifold
{
    public class CheckButton : MonoBehaviour
    {
        [Tooltip("0 for O, 1 for V")]
        public Sprite[] checkOV;
        [SerializeField]
        private SpriteRenderer m_SR;

        public bool isOn = false;

        // Use this for initialization
        void Start()
        {
            m_SR.sprite = checkOV[isOn ? 1 : 0];
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Triggered");
            if (!isOn)
            {
                Debug.Log("T !isOn");
                TurnOn();
                // Play sound here

                GetComponent<DuplicateHandler>().InvokeActionInDuplicates(1);
            }
        }

        void TurnOn()
        {
            isOn = true;
            m_SR.sprite = checkOV[1];
            try
            {
                Destroy(GetComponent<Collider>());
            }
            finally
            {
                Debug.Log("On: " + gameObject.transform.parent.name);
            }
        }
    }
}
