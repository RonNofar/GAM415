using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manifold.LevelTransfer
{
    public class LevelTransferHandler : MonoBehaviour
    {
        static public LevelTransferHandler Instance { get { return _instance; } }
        static protected LevelTransferHandler _instance;

        public void Awake()
        {
            if (_instance != null)
            {
                Debug.LogWarning("LevelTransferHandler is already in play. Deleting new!", gameObject);
                Destroy(gameObject);
            }
            else
            { _instance = this; }
        }

        public Vector3 HeldRelativePosition { get; set; }
        public Vector3 HeldRelativeForward { get; set; }
        public Quaternion HeldRelativeRotation { get; set; }

        public Vector3 HeldCameraRelativeForward { get; set; }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
