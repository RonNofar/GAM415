using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Manifold
{
    public class ManifoldObjectHandler : MonoBehaviour
    {

        public bool __DEBUG__ = true;

        [SerializeField]
        private string[] maintainTags = { "PickUp" };

        private GameObject[] taggedObjects; // all objects tagged with maintainTags
        private int taggedObjectsLength;
        private Transform[] taggedTransforms; // all the transforms of the objects in taggedObjects
        private Rigidbody[] taggedRigidbodies;
        private Vector3[] objectOrigins;//private List<Vector3> objectOrigins = new List<Vector3>();
        private Transform[] maintainTransforms;//private List<Transform> maintainTransforms = new List<Transform>();

        private Manifold Manifold;

        private static readonly string[] axises = { "X", "Y", "Z" };
        private static readonly string[] directions = { "Pos", "Neg" };

        private bool isChecked = false; // refers to initial use of CheckForChange function

        // Use this for initialization
        void Start()
        {
            SetInitialReferences();
        }

        void SetInitialReferences()
        {
            if (taggedObjects == null)
            {
                foreach (string tag in maintainTags)
                {
                    taggedObjects = GameObject.FindGameObjectsWithTag(tag);
                }
                taggedObjectsLength = taggedObjects.Length;
                taggedTransforms = new Transform[taggedObjectsLength];
                for (int i = 0; i < taggedObjectsLength; ++i)
                {
                    taggedTransforms[i] = taggedObjects[i].transform;
                }
            }/*
            if (taggedObjects != null)
            {
                objectOrigins = new Vector3[taggedObjects.Length];
                maintainTransforms = new Transform[taggedObjects.Length];
                for (int i = 0; i < taggedObjects.Length; ++i)
                {
                    objectOrigins[i] = taggedTransforms[i].position;//taggedObjects[i].transform.position;
                }
            }*/
        }

        // Update is called once per frame
        void Update()
        {
            // if specific object is not at its original center
            //  |_then start check similar to player X,Y,Z
            
            if (taggedObjects != null && objectOrigins != null)
            {
                //CheckForChange(taggedTransforms, objectOrigins);
                //CheckForChangeMomentum(taggedObjects); <==
            }
            /*
            if (isChecked) // makes sure that maintainTransforms is not null
            {
                MaintainTransforms(maintainTransforms);
                bool called = false;
                if (!called) 
                {
                    //if (__DEBUG__) Debug.Log(maintainTransforms[0].name);
                    called = true;
                }
            }*/
            
        }

        void CheckForChange (Transform[] transformArrToCheck, Vector3[] originArrToCompare) // RETHINK HOW TO DO THIS EFFICIENTLY (dictionary, matrix)
        { // NOTE: This function interacts with maintainTransforms[] which is not passed in
            for (int i = 0; i < transformArrToCheck.Length; ++i)
            {
                if (transformArrToCheck[i].position != originArrToCompare[i])
                {
                    maintainTransforms[i] = transformArrToCheck[i];
                    //if (__DEBUG__) Debug.Log(maintainTransforms[i].gameObject); // WATCH OUT! THIS BREAKS GAME FOR SOME REASON
                    if (!isChecked) isChecked = true; // check for maintainTransforms being initialized
                }
            }
        }
        
        void CheckForChangeMomentum (Transform[] transformArrToCheck)
        {
            for (int i = 0; i < transformArrToCheck.Length; ++i)
            {/*
                if (transformArrToCheck.velocity.magnitude != 0)
                {
                    // something here
                }*/
            }
        }
        
        
        void MaintainTransforms (Transform[] transArr)
        {
            for (int i = 0; i < transArr.Length; ++i)
            {
                if (transArr[i] != null)
                {
                    //if (__DEBUG__) Debug.Log("Maintaining " + transArr[i].name);
                }
                
            }
            /*
            foreach (Transform tran in transArr)
            {
                if (__DEBUG__) Debug.Log("Maintaining " + tran.name);
                foreach (string axis in axises)
                {
                    //Manifold.CheckAndUpdateTransformPosition(tran, axis);
                }
            }*/
        }
    }
}
