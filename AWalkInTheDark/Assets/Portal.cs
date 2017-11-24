using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manifold.LevelTransfer
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] bool test = true;
        [SerializeField] PlayerDirectionCheck _PDC;
        [SerializeField] Camera cam;
        [SerializeField] bool isRelativeTeleport = true; // the original
        [SerializeField] bool isOneSided = true;
        public Vector3 faceNormal = Vector3.forward;
        [SerializeField] Transform playerTransform;
        public Transform transferParent;
        [SerializeField] int nextScene;

        [SerializeField] bool isOutput = false;

        Vector3 relativeOrigin;

        [Header("Same Level Test")]
        [SerializeField]
        bool isSameLevelTest = false;
        [SerializeField] Portal otherPortal;

        public Transform otherTransform;

        private bool isCoolingDown;

        public float cooldownTime = 2f;
        public bool isSeeThrough;

        private Collider col;
        private new Renderer renderer;

        private bool isInvisible; // this is for the "Dan problem"
        public bool isPreCollider; // ^^
        public bool _DPCHECK = false; // whether to even check for ^^

        private void Awake()
        {
            //otherTransform = otherButton.gameObject.transform;
            col = GetComponent<Collider>();
            renderer = GetComponent<Renderer>();
        }

        // Use this for initialization
        void Start()
        {
            if (isOutput) // finish this part <<==
            {
                TransferPlayerInLevel(ref transferParent);
            }
            //relativeOrigin = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            //if (gameObject.name == "PortalA (2)") Debug.Log("1" + " | " + gameObject.name);
            if (isSeeThrough && cam != null)
            {
                //if (gameObject.name == "PortalA (2)") Debug.Log("2" + " | " + gameObject.name);
                Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
                if (!GeometryUtility.TestPlanesAABB(planes, col.bounds)/* && _DPCHECK*/)
                { // if portal collider bounds in camera planes
                    //if (gameObject.name == "PortalA (2)") Debug.Log("here? "+isInvisible+" | "+gameObject.name);
                    if (!isPreCollider)
                    {
                        if (isInvisible)
                            IsInvisible(false);
                    }
                }
                if (!_DPCHECK) isPreCollider = false;
            }
        }
        
        private void OnCollisionEnter(Collision other)
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

        private void OnTriggerEnter(Collider other)
        {
            // Debug.Log("In OnTriggerEnter in "+gameObject.name);
            if (!isCoolingDown && !isOutput && other.tag == "MainCamera")
            {
                //Debug.Log("ontriggerenter tag is maincamera");
                if (isOneSided)
                {
                    /*Debug.Log(faceNormal);
                    Vector3 heading = playerTransform.position - transform.position;
                    float dot = Vector3.Dot(heading, transform.TransformDirection(faceNormal));
                    Debug.Log(heading);
                    Debug.Log(transform.TransformDirection(faceNormal));
                    Debug.Log(dot);*/
                    if (_PDC.isPlayerNormal && !isInvisible)
                    {
                        StoreRelativeValues(ref playerTransform);

                        if (!isSameLevelTest) SceneManager.LoadScene(nextScene);
                        else TransferPlayerInLevel(ref otherTransform);
                    }
                    isPreCollider = false;
                }
                else
                {
                    StoreRelativeValues(ref playerTransform);

                    if (!isSameLevelTest) SceneManager.LoadScene(nextScene);
                    else TransferPlayerInLevel(ref otherTransform);
                }
            }
        }


        private void StoreRelativeValues(ref Transform tran)
        {
            if (isRelativeTeleport)
            {/*
                tran.SetParent(transferParent);
                //tran = transferParent.GetChild(0);
                LevelTransferHandler.Instance.HeldRelativePosition = tran.localPosition;
                LevelTransferHandler.Instance.HeldRelativeRotation = tran.localRotation;
                */
                LevelTransferHandler.Instance.HeldRelativePosition = transferParent.InverseTransformPoint(tran.position);
                //LevelTransferHandler.Instance.HeldRelativeForward = transferParent.InverseTransformDirection(tran.forward);

                //LevelTransferHandler.Instance.HeldCameraRelativeForward = transferParent.InverseTransformDirection(playerTransform.gameObject.GetComponent<)
            }
            else
            {
                LevelTransferHandler.Instance.HeldRelativePosition = tran.position - transferParent.position;//transferParent.InverseTransformPoint(tran.position);
            }

            //Debug.Log(LevelTransferHandler.Instance.HeldRelativePosition + " | " + LevelTransferHandler.Instance.HeldRelativeRotation);
        }

        private void UnParent(Transform tran)
        {
            tran.parent = null;
        }

        private void TransferPlayerInLevel(ref Transform location)
        {
            if (isRelativeTeleport)
            {
                /*
                //Debug.Log("WAT");
                UnParent(playerTransform);
                playerTransform.SetParent(location);

                playerTransform.localPosition = LevelTransferHandler.Instance.HeldRelativePosition;
                playerTransform.localRotation = LevelTransferHandler.Instance.HeldRelativeRotation;

                UnParent(playerTransform);
                */
                playerTransform.position = location.TransformPoint(LevelTransferHandler.Instance.HeldRelativePosition);
                //playerTransform.rotation = Quaternion.LookRotation(location.TransformDirection(LevelTransferHandler.Instance.HeldRelativeForward));
            } 
            else
            {
                //Debug.Log("location.position + LevelTransferHandler.Instance.HeldRelativePosition = " + (location.position + LevelTransferHandler.Instance.HeldRelativePosition) + " = " + location.position + " + " + LevelTransferHandler.Instance.HeldRelativePosition);
                playerTransform.position = location.position + LevelTransferHandler.Instance.HeldRelativePosition;
            }

            if (isSameLevelTest)
            {
                otherPortal.StartCoroutine(otherPortal.SetCoolDown(cooldownTime));
                if (isSeeThrough)
                {
                    otherPortal.IsCollider(false);
                    otherPortal.IsRenderer(true);
                    //otherPortal.meshRenderer.enabled = false;
                }
            }

        }

        public IEnumerator SetCoolDown(float time)
        {
            isCoolingDown = true;

            yield return new WaitForSeconds(time);

            isCoolingDown = false;
            if (isSeeThrough)
            {
                //Debug.Log("Cooldown = false, isCollider = false");
                IsCollider(true);
                IsRenderer(true);
                //meshRenderer.enabled = true;
            }
        }

        public void IsInvisible(bool check)
        {
            //Debug.Log("IsInvisible=" + check + " | name: " + gameObject.name);
            isInvisible = check;
            //IsCollider(!check);
            IsRenderer(!check);

            if (check)
                isPreCollider = true;
        }

        public void IsCollider(bool check)
        {
            //Debug.Log("IsCollider=" + check + " | name: " + gameObject.name);
            col.enabled = check;
            //Debug.Log("col.enabled=" + col.enabled + " | name: " + gameObject.name);
        }

        public void IsRenderer(bool check)
        {
            //Debug.Log("IsRenderer=" + check + " | name: " + gameObject.name);
            renderer.enabled = check;
            //Debug.Log("renderer.enabled=" + renderer.enabled+" | name: "+gameObject.name);
        }
    }
}
