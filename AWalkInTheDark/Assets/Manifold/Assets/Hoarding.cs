using UnityEngine;
using System.Collections;

public class Hoarding : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /*
    void CreateToward_PosX () {
        if (__DEBUG__) { Debug.Log("I entered PosX, "+boundsCenter); }
        if (boundsCenter.x <= drawDistance)
        {
            Vector3 transformVec = new Vector3(boundsSize_X * 1.5f, 0, 0);
            Vector3 spawnVec = boundsCenter + transformVec;
            if (Physics.CheckSphere (spawnVec, boundsSize_X * 0.5f)) { if (__DEBUG__) Debug.Log("Found object at: " + spawnVec);// Do nothing
            } else {
                Instantiate(gameObject, spawnVec, myOriginalRotation);
            }
        }
    }

    void CreateToward_NegX()
    {
        if (__DEBUG__) { Debug.Log("I entered NegX, " + boundsCenter); }
        if (boundsCenter.x >= -drawDistance)
        {
            Vector3 transformVec = new Vector3(boundsSize_X * 1.5f, 0, 0);
            Vector3 spawnVec = boundsCenter - transformVec;
            if (Physics.CheckSphere(spawnVec, boundsSize_X * 0.5f)) { if (__DEBUG__) Debug.Log("Found object at: " + spawnVec);// Do nothing
            } else {
                Instantiate(gameObject, spawnVec, myOriginalRotation);
            }
        }
    }

    void CreateToward_PosY()
    {
        if (__DEBUG__) { Debug.Log("I entered PosY, " + boundsCenter); }
        if (boundsCenter.y <= drawDistance)
        {
            Vector3 transformVec = new Vector3(0, boundsSize_Y * 1.5f, 0);
            Vector3 spawnVec = boundsCenter + transformVec;
            if (Physics.CheckSphere(spawnVec, boundsSize_Y * 0.5f)) { if (__DEBUG__) Debug.Log("Found object at: " + spawnVec);// Do nothing
            } else {
                Instantiate(gameObject, spawnVec, myOriginalRotation);
            }
        }
    }

    void CreateToward_NegY()
    {
        if (__DEBUG__) { Debug.Log("I entered NegY, " + boundsCenter); }
        if (boundsCenter.y >= -drawDistance)
        {
            Vector3 transformVec = new Vector3(0, boundsSize_Y * 1.5f, 0);
            Vector3 spawnVec = boundsCenter - transformVec;
            if (Physics.CheckSphere(spawnVec, boundsSize_Y * 0.5f)) { if (__DEBUG__) Debug.Log("Found object at: "+spawnVec);// Do nothing
            } else {
                Instantiate(gameObject, spawnVec, myOriginalRotation);
            }
        }
    }

    void CreateToward_PosZ()
    {
        if (__DEBUG__) { Debug.Log("I entered PosZ, " + boundsCenter); }
        if (boundsCenter.z <= drawDistance)
        {
            Vector3 transformVec = new Vector3(0, 0, boundsSize_Z * 1.5f);
            Vector3 spawnVec = boundsCenter + transformVec;
            if (Physics.CheckSphere(spawnVec, boundsSize_Z * 0.5f)) { if (__DEBUG__) Debug.Log("Found object at: " + spawnVec);
                Debug.Log("POW");
            } else {
                Instantiate(gameObject, spawnVec, myOriginalRotation);
            }
        }
    }

    void CreateToward_NegZ()
    {
        if (__DEBUG__) { Debug.Log("I entered NegZ, " + boundsCenter); }
        if (boundsCenter.z >= -drawDistance)
        {
            Vector3 transformVec = new Vector3(0, 0, boundsSize_Z * 1.5f);
            Vector3 spawnVec = boundsCenter - transformVec;
            if (Physics.CheckSphere(spawnVec, boundsSize_Z * 0.5f)) { if (__DEBUG__) Debug.Log("Found object at: " + spawnVec);// Do nothing
            } else {
                Instantiate(gameObject, spawnVec, myOriginalRotation);
            }
        }
    }*/
}

/*
if (playerTransform.position.y <= boundsCenter.y - (boundsSize_Y * 0.5f))
{
    playerTransform.position = new Vector3(playerTransform.position.x, boundsCenter.y + (boundsSize_Y * 0.5f), playerTransform.position.z);
}
else if (playerTransform.position.y >= boundsCenter.y + (boundsSize_Y * 0.5))
{
    playerTransform.position = new Vector3(playerTransform.position.x, boundsCenter.y - (boundsSize_Y * 0.5f), playerTransform.position.z);
}
if (playerTransform.position.x <= boundsCenter.x - (boundsSize_X * 0.5f))
{
    playerTransform.position = new Vector3(boundsCenter.x + (boundsSize_X * 0.5f), playerTransform.position.y, playerTransform.position.z);
}*/
