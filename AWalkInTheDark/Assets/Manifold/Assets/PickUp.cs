using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

    public bool __DEBUG__ = true;

    public float holdDistance = 1;

    private RaycastHit hit;
    [SerializeField] private LayerMask detectionLayer;
    private Transform myTransform;
    private Transform hitTransform = null;
    private Camera myCamera;
    private Transform cameraTransform;

    private bool holding = false;
    private float range = 5;

	// Use this for initialization
	void Start () {
        SetInitialReferences();

    }

    void SetInitialReferences() {
        myTransform = transform;
        myCamera = GetComponentInChildren<Camera>();
        cameraTransform = myCamera.transform;
        detectionLayer = 1 << 8 | 1 << 7;
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButton("Fire1") && !holding)
        {
            if (__DEBUG__) Debug.Log("Shot fired");
            LookForObject(detectionLayer);
        } else if (Input.GetButton("Fire1") && holding)
        {
            holding = false;
        }
        if (holding)
        {
            if (__DEBUG__) Debug.Log("holding");
            if (hitTransform.CompareTag("PickUp"))
            {
                Debug.Log("PickedUp " + hitTransform.name);
                HoldObject(hitTransform);
            }
            else
            {
                Debug.Log("Not an pickup-able item.");
            }
        }
	}

    void LookForObject (LayerMask detectionLayer) {
        if (__DEBUG__) Debug.Log("Looking");
        if (Physics.Raycast (myTransform.position, myTransform.forward, out hit, range, detectionLayer))
        {
            if (__DEBUG__) Debug.Log("Found: " + hit.transform.name);
            hitTransform = hit.transform;
            holding = true;
        }
    }

    void HoldObject (Transform hitTransform) {
        //hitObjectTransform.position = normal vector from camera
        hitTransform.position = cameraTransform.position + cameraTransform.forward * holdDistance;
        hitTransform.rotation = new Quaternion(cameraTransform.rotation.x, cameraTransform.rotation.y, cameraTransform.rotation.z, cameraTransform.rotation.w);//new Quaternion(0.0f, cameraTransform.rotation.y, 0.0f, cameraTransform.rotation.w);
    }
}
