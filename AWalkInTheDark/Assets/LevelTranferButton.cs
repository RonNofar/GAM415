using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manifold.LevelTransfer
{
    public class LevelTranferButton : MonoBehaviour
    {
        [SerializeField] Transform playerTransform;
        [SerializeField] int nextScene;

        [SerializeField] bool isOutput = false;

        Vector3 relativeOrigin;

        private void Awake()
        {
            if (isOutput)
            {
                playerTransform.position = transform.position + LevelTransferHandler.Instance.HeldRelativePosition;
                playerTransform.rotation = LevelTransferHandler.Instance.HeldRelativeRotation;
            }
        }

        // Use this for initialization
        void Start()
        {
            relativeOrigin = transform.position;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            LevelTransferHandler.Instance.HeldRelativeRotation = playerTransform.rotation;
            Debug.Log("Before: "+playerTransform.localPosition);
            playerTransform.SetParent(transform);
            Debug.Log("After: " + playerTransform.localPosition);
            LevelTransferHandler.Instance.HeldRelativePosition = playerTransform.localPosition;
            //LevelTransferHandler.Instance.HeldRelativeRotation = playerTransform.localRotation;

            SceneManager.LoadScene(nextScene);
            
        }
    }
}
