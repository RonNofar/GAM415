using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manifold.Level1
{
    public class Level1Puzzle1Button1Event1 : MonoBehaviour
    {
        [SerializeField]
        private GameObject checkObject;
        //private CheckButton checkButton;

        private Transform toScale;

        [SerializeField]
        private float totalTime = 2f;
        [SerializeField]
        private float scaler = 10f;
        [SerializeField] bool isDup = false;

        private bool isEvent1Triggered = false;

        void Start()
        {
            toScale = GetComponent<Transform>();
        }

        void FixedUpdate()
        {
            if (!isEvent1Triggered && !isDup)
            {
                if (checkObject.GetComponent<CheckButton>().isOn)
                {
                    //Debug.Log("YSA");
                    Act1();
                    isEvent1Triggered = true;
                }
            }
        }

        public void Act1()
        {
            TriggerEvent1();

            GetComponent<DuplicateHandler>().InvokeActionInDuplicates(1);
        }

        public void TriggerEvent1()
        {
            //Debug.Log("Triggered: " + gameObject.transform.parent);
            StartCoroutine(
                ScaleDownward(toScale, totalTime, scaler));
        }

        IEnumerator ScaleDownward(Transform tran, 
            float totalTime, float scaler)
        {
            float startTime = Time.time;
            float i = 0;
            float temp = 0;
            while (i < 1)
            {
                i = (Time.time - startTime) / totalTime;
                temp = Mathf.Clamp(((1 / i) - 1) / scaler, 0, 1);
                tran.localScale = new Vector3(
                    tran.localScale.x, 
                    temp, 
                    tran.localScale.z);
                yield return null;
            }

        }
    }
}
