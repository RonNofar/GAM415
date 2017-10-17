using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manifold
{
    public class GlassToggleDuplicate : Duplicate {

        [SerializeField] Transform scaleTran;
        [SerializeField] Renderer rend;
        private GlassToggle gToggle;

        void Start() {
            base.Start();
            gToggle = dupHand.gameObject.GetComponent<GlassToggle>();
        }

        public override void Act1()
        {
            if (debugCheck) Debug.Log("In Act1");
            StartCoroutine(
                GlassToggle.ScaleObject(
                    scaleTran, 
                    gToggle.time, 
                    gToggle.toOne, 
                    gToggle.X, 
                    gToggle.Y, 
                    gToggle.Z));
            GlassToggle.ChangeMaterial(rend, gToggle.disabledMat);
        }
    }
}
