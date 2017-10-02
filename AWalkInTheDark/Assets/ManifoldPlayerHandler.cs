using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manifold
{
    public class ManifoldPlayerHandler : MonoBehaviour
    {
        [SerializeField] GameObject playerObject;
        [SerializeField] GameObject boundsGameObject;

        private Transform playerTransform;

        private Renderer boundsRenderer;
        private Bounds bounds;
        private Vector3 boundsCenter;
        private float boundsSize_X;
        private float boundsSize_Y;
        private float boundsSize_Z;

        private Transform myTransform;
        private Quaternion myOriginalRotation;

        private static readonly string[] axises = { "X", "Y", "Z" };
        private static readonly string[] directions = { "Pos", "Neg" };

        [SerializeField] bool _DEBUG = true;

        private void Awake()
        {
            SetInitialReferences();
        }
        void Start()
        {
            CheckVariables();
        }

        void SetInitialReferences()
        {
            myTransform = transform;
            myOriginalRotation = transform.rotation;
            try
            {
                playerTransform = playerObject.transform;
            }
            catch
            {
                Debug.Log("ERROR: No playerObject found.");
            }
            try
            {
                boundsRenderer = boundsGameObject.GetComponent<Renderer>();
                bounds = boundsRenderer.bounds;
                boundsCenter = bounds.center;
                boundsSize_X = bounds.size.x;
                boundsSize_Y = bounds.size.y;
                boundsSize_Z = bounds.size.z;
            }
            catch
            {
                Debug.Log("ERROR: No renderer found.");
            }
        }

        void CheckVariables()
        {
            if (boundsCenter == new Vector3(0, 0, 0))
            {
                if (boundsSize_X != boundsSize_Y || boundsSize_X != boundsSize_Z || boundsSize_Y != boundsSize_Z)
                {
                    Debug.Log("POSSIBLE FATAL ERROR: containment object is not a cube.");
                }
            }
        }

        void Update()
        {
            CheckAndUpdateTransformPosition(playerTransform, axises[0]);
            CheckAndUpdateTransformPosition(playerTransform, axises[1]);
            CheckAndUpdateTransformPosition(playerTransform, axises[2]);
        }

        public void CheckAndUpdateTransformPosition(Transform objectTransform, string axis)
        {
            if (_DEBUG) Debug.Log(boundsCenter + " | " + "I am checking " + objectTransform.gameObject + "'s position on " + axis);
            string boundsCase = axis;
            float objectPosition_specific = 0;
            float boundsCenter_specific = 0;
            float boundsSize_specific = 0;
            float boundsReleaseCords_specific = 0;
            Vector3 transformVec_Pos = new Vector3(0, 0, 0);
            Vector3 transformVec_Neg = new Vector3(0, 0, 0);
            switch (boundsCase)
            {
                case "X":
                    if (_DEBUG) Debug.Log(axis);
                    objectPosition_specific = objectTransform.position.x;
                    boundsCenter_specific = boundsCenter.x;
                    //boundsCatchCords_specific = playerPosition_specific - boundsCenter_specific;
                    boundsSize_specific = boundsSize_X;
                    boundsReleaseCords_specific = Mathf.Abs(Mathf.Abs(boundsSize_specific) - Mathf.Abs(objectPosition_specific));
                    transformVec_Neg = new Vector3(boundsCenter_specific - boundsReleaseCords_specific, objectTransform.position.y, objectTransform.position.z);
                    transformVec_Pos = new Vector3(boundsCenter_specific + boundsReleaseCords_specific, objectTransform.position.y, objectTransform.position.z);
                    break;
                case "Y":
                    if (_DEBUG) Debug.Log(axis);
                    objectPosition_specific = objectTransform.position.y;
                    boundsCenter_specific = boundsCenter.y;
                    boundsSize_specific = boundsSize_Y;
                    boundsReleaseCords_specific = Mathf.Abs(Mathf.Abs(boundsSize_specific) - Mathf.Abs(objectPosition_specific));
                    transformVec_Neg = new Vector3(objectTransform.position.x, boundsCenter_specific - boundsReleaseCords_specific, objectTransform.position.z);
                    transformVec_Pos = new Vector3(objectTransform.position.x, boundsCenter_specific + boundsReleaseCords_specific, objectTransform.position.z);
                    break;
                case "Z":
                    if (_DEBUG) Debug.Log(axis);
                    objectPosition_specific = objectTransform.position.z;
                    boundsCenter_specific = boundsCenter.z;
                    boundsSize_specific = boundsSize_Z;
                    boundsReleaseCords_specific = Mathf.Abs(Mathf.Abs(boundsSize_specific) - Mathf.Abs(objectPosition_specific));
                    transformVec_Neg = new Vector3(objectTransform.position.x, objectTransform.position.y, boundsCenter_specific - boundsReleaseCords_specific);//boundsCenter.z - (boundsSize_Z * 0.5f));
                    transformVec_Pos = new Vector3(objectTransform.position.x, objectTransform.position.y, boundsCenter_specific + boundsReleaseCords_specific);
                    break;
                default:
                    if (_DEBUG) Debug.Log(boundsCenter + " | " + "ERROR: Invalid case statement.");
                    break;
            }
            if (objectPosition_specific <= boundsCenter_specific - (boundsSize_specific * 0.5f))
            {
                if (_DEBUG) Debug.Log("Pos");
                objectTransform.position = transformVec_Pos;
            }
            else if (objectPosition_specific >= boundsCenter_specific + (boundsSize_specific * 0.5f))
            {
                if (_DEBUG) Debug.Log("Neg");
                objectTransform.position = transformVec_Neg;
            }
        }

        void CheckAndUpdatePlayerPosition_MethodTwo(string axis)
        {
            Debug.Log(boundsCenter + " | " + "I am checking player position on " + axis);
            string boundsCase = axis;
            float playerPosition_specific = 0;
            float boundsCenter_specific = 0;
            float boundsSize_specific = 0;
            Vector3 transformVec_Pos = new Vector3(0, 0, 0);
            Vector3 transformVec_Neg = new Vector3(0, 0, 0);
            switch (boundsCase)
            {
                case "X":
                    if (_DEBUG) Debug.Log(axis);
                    playerPosition_specific = playerTransform.position.x;
                    boundsCenter_specific = boundsCenter.x;
                    //boundsCatchCords_specific = playerPosition_specific - boundsCenter_specific;
                    boundsSize_specific = boundsSize_X;
                    transformVec_Neg = new Vector3(boundsCenter_specific - (boundsSize_specific * 0.5f), playerTransform.position.y, playerTransform.position.z);
                    transformVec_Pos = new Vector3(boundsCenter_specific + (boundsSize_specific * 0.5f), playerTransform.position.y, playerTransform.position.z);
                    break;
                case "Y":
                    if (_DEBUG) Debug.Log(axis);
                    playerPosition_specific = playerTransform.position.y;
                    boundsCenter_specific = boundsCenter.y;
                    boundsSize_specific = boundsSize_Y;
                    transformVec_Neg = new Vector3(playerTransform.position.x, boundsCenter_specific - (boundsSize_specific * 0.5f), playerTransform.position.z);
                    transformVec_Pos = new Vector3(playerTransform.position.x, boundsCenter_specific + (boundsSize_specific * 0.5f), playerTransform.position.z);
                    break;
                case "Z":
                    if (_DEBUG) Debug.Log(axis);
                    playerPosition_specific = playerTransform.position.z;
                    boundsCenter_specific = boundsCenter.z;
                    boundsSize_specific = boundsSize_Z;
                    transformVec_Neg = new Vector3(playerTransform.position.x, playerTransform.position.y, boundsCenter_specific - (boundsSize_specific * 0.5f));
                    transformVec_Pos = new Vector3(playerTransform.position.x, playerTransform.position.y, boundsCenter_specific + (boundsSize_specific * 0.5f));
                    break;
                default:
                    if (_DEBUG) Debug.Log(boundsCenter + " | " + "ERROR: Invalid case statement.");
                    break;
            }
            if (playerPosition_specific <= boundsCenter_specific - (boundsSize_specific * 0.5f))
            {
                if (_DEBUG) Debug.Log("Pos");
                playerTransform.position = transformVec_Pos;
            }
            else if (playerPosition_specific >= boundsCenter_specific + (boundsSize_specific * 0.5f))
            {
                if (_DEBUG) Debug.Log("Neg");
                playerTransform.position = transformVec_Neg;
            }
        }
    }
}
