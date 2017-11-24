using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manifold.LevelTransfer
{
    public class PlayerDirectionCheck : MonoBehaviour
    {

        [SerializeField] BoxCollider cameraCollider;
        [SerializeField] Portal associatedPortal;
        /// <summary>
        /// Whether the player is in the quadrant of the normal vector
        /// </summary>
        [HideInInspector] public bool isPlayerNormal;

        private new Collider collider;
        private Transform cameraTransform;
        private Vector3 cameraColliderCenter; // In world space

        void Start()
        {
            collider = GetComponent<Collider>();
            cameraTransform = cameraCollider.gameObject.transform;
            UpdateCameraCenter();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "MainCamera")
            {
                CheckIfNormal();
                associatedPortal._DPCHECK = true;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "MainCamera")
            {
                CheckIfNormal();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "MainCamera")
            {
                associatedPortal._DPCHECK = false;
                associatedPortal.isPreCollider = false;
            }
        }

        void UpdateCameraCenter()
        {
            cameraColliderCenter = cameraTransform.TransformPoint(cameraCollider.center);
            //Debug.Log(cameraColliderCenter);
        }

        void CheckIfNormal()
        {
            UpdateCameraCenter();

            Vector3 heading = cameraColliderCenter - associatedPortal.transform.position;
            //Debug.Log(heading);
            float dot = Vector3.Dot(
                heading, 
                associatedPortal.transform.TransformDirection(
                    associatedPortal.faceNormal));

            isPlayerNormal = (dot > 0);
            //Debug.Log(isPlayerNormal + " isPN | "+ dot);

            if (dot < 0)
            {
                //Debug.Log("dot<0");
                associatedPortal.IsInvisible(true);
                associatedPortal.isPreCollider = true;
            } 
            else
            {
                associatedPortal.isPreCollider = false;
            }
        }

    }
}
