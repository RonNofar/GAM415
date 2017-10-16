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
        [SerializeField] bool X;
        [SerializeField] bool Y;
        [SerializeField] bool Z;
        [SerializeField] bool toOne;
        [SerializeField] float time = 1f;

        private Vector3 originalScale;

        [Header("Material Event")]
        [SerializeField] new Renderer renderer;
        [SerializeField] Material disabledMat;

        private void Start()
        {
            originalScale = tranToScale.localScale;
            if (renderer == null)
                renderer = GetComponent<Renderer>();
        }

        public override void Action()
        {
            if (!isOn)
                StartCoroutine(ScaleObject(time, toOne));
            ChangeMaterial(disabledMat);
        }

        IEnumerator ScaleObject(float time, bool toOne)
        {
            isOn = true;
            float startTime = Time.time;
            float i = 0;

            while (i < 1)
            {
                i = (Time.time - startTime) / time;
                float temp = 0.5f + Mathf.Cos(i * Mathf.PI) / 2;
                if (toOne) temp = 1 - temp;

                Mathf.Clamp(temp, 0, 1);
                tranToScale.localScale = new Vector3(
                    X ? originalScale.x * temp : tranToScale.localScale.x,
                    Y ? originalScale.y * temp : tranToScale.localScale.y,
                    Z ? originalScale.z * temp : tranToScale.localScale.z);

                yield return null;
            }

            if (!toOne)
            {
                tranToScale.gameObject.SetActive(false);
            }
        }

        public void ChangeMaterial(Material mat)
        {
            renderer.material = mat;
        }
    }
}
