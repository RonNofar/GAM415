using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manifold
{
    public class GlassToggle : ToggleSwitch
    {
        private bool isOn;

        [Header("Scale Event")]
        [SerializeField] Transform tranToScale;
        public bool X;
        public bool Y;
        public bool Z;
        public bool toOne;
        public float time = 1f;

        private Vector3 originalScale;

        [Header("Material Event")]
        [SerializeField] new Renderer renderer;
        public Material disabledMat;

        private void Start()
        {
            
            if (renderer == null)
                renderer = GetComponent<Renderer>();
        }

        public override void Action()
        {
            if (!isOn)
                StartCoroutine(ScaleObject(tranToScale, time, toOne, X, Y, Z)); isOn = true;
            ChangeMaterial(renderer, disabledMat);
        }

        static public IEnumerator ScaleObject(Transform tran, float time, bool toOne, bool X, bool Y, bool Z)
        {
            Vector3 originalScale = tran.localScale;

            float startTime = Time.time;
            float i = 0;

            while (i < 1)
            {
                i = (Time.time - startTime) / time;
                float temp = 0.5f + Mathf.Cos(i * Mathf.PI) / 2;
                if (toOne) temp = 1 - temp;

                Mathf.Clamp(temp, 0, 1);
                tran.localScale = new Vector3(
                    X ? originalScale.x * temp : tran.localScale.x,
                    Y ? originalScale.y * temp : tran.localScale.y,
                    Z ? originalScale.z * temp : tran.localScale.z);

                yield return null;
            }

            if (!toOne)
            {
                tran.gameObject.SetActive(false);
            }
        }

        static public void ChangeMaterial(Renderer ren, Material mat)
        {
            ren.material = mat;
        }
    }
}
