using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manifold.LevelTransfer
{
    public class LevelTranferButton : MonoBehaviour
    {
        [SerializeField] Transform playerTransform;
        [SerializeField] Transform transferParent;
        [SerializeField] int nextScene;

        [SerializeField] bool isOutput = false;

        Vector3 relativeOrigin;

        [Header("Same Level Test")]
        [SerializeField] bool isSameLevelTest = false;
        [SerializeField] LevelTranferButton otherButton;

        public Transform otherTransform;

        private bool isCoolingDown;

        private void Awake()
        {
            //otherTransform = otherButton.gameObject.transform;
            if (isOutput) // finish this part <<==
            {
                TransferPlayerInLevel(ref transferParent);
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
            //LevelTransferHandler.Instance.HeldRelativeRotation = playerTransform.rotation;
            /*Debug.Log("Before: "+playerTransform.localPosition);
            playerTransform.SetParent(transform);
            Debug.Log("After: " + playerTransform.localPosition);
            LevelTransferHandler.Instance.HeldRelativePosition = playerTransform.localPosition;
            LevelTransferHandler.Instance.HeldRelativeRotation = playerTransform.localRotation;
            */
            if (!isCoolingDown && !isOutput)
            {
                StoreRelativeValues(ref playerTransform);

                if (!isSameLevelTest) SceneManager.LoadScene(nextScene);
                else TransferPlayerInLevel(ref otherTransform);
            }


        }

        private void StoreRelativeValues(ref Transform tran)
        {
            tran.SetParent(transferParent);
            //tran = transferParent.GetChild(0);
            LevelTransferHandler.Instance.HeldRelativePosition = tran.localPosition;
            LevelTransferHandler.Instance.HeldRelativeRotation = tran.localRotation;
            
            Debug.Log(LevelTransferHandler.Instance.HeldRelativePosition + " | " + LevelTransferHandler.Instance.HeldRelativeRotation);
        }

        private void UnParent(Transform tran)
        {
            tran.parent = null;
        }

        private void TransferPlayerInLevel(ref Transform location)
        {
            //Debug.Log("WAT");
            UnParent(playerTransform);
            playerTransform.SetParent(location);

            playerTransform.localPosition = LevelTransferHandler.Instance.HeldRelativePosition;
            playerTransform.localRotation = LevelTransferHandler.Instance.HeldRelativeRotation;
            
            //UnParent(playerTransform);

            if (isSameLevelTest)
                otherButton.StartCoroutine(otherButton.SetCoolDown(1f));
        }

        public IEnumerator SetCoolDown(float time)
        {
            isCoolingDown = true;

            yield return new WaitForSeconds(3);

            isCoolingDown = false;
        }
    }
}
