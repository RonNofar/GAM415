using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Manifold
{
    public class GravityReverser : MonoBehaviour
    {
        [SerializeField] float cooldownTime = 1f;
        [SerializeField] Material cooldownMat;
        [SerializeField] SpriteRenderer sr;
        [SerializeField] Sprite coolDownSp;

        private Renderer ren;
        private Material originalMat;
        private Sprite originalSprite;
        private bool isCoolingDown = false;

        private void Start()
        {
            ren = GetComponent<Renderer>();
            originalMat = ren.material;

            originalSprite = sr.sprite;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player" && !isCoolingDown)
            {
                collision.gameObject.GetComponent<RigidbodyFirstPersonController>().ReverseGravity();
                StartCoroutine(StartCoolDown(cooldownTime));
            }
        }

        IEnumerator StartCoolDown(float time)
        {
            isCoolingDown = true;
            ren.material = cooldownMat;
            sr.sprite = coolDownSp;

            yield return new WaitForSeconds(time);

            isCoolingDown = false;
            ren.material = originalMat;
            sr.sprite = originalSprite;
        }
    }
}
