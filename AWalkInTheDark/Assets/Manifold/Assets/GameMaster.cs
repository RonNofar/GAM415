using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameMaster : MonoBehaviour {

    public bool __DEBUG__ = true;

    public GameObject playerObject;
    public float holdTimer = 50;

    private Transform playerTransform;
    private bool isHoldingPlayer = false;
    private Vector3 catchCords;

    private float time;

    private Vector3 initialCoords;

    // Use this for initialization
    void Start () {
        SetInitialReferences();
    }

    void SetInitialReferences ()
    {
        if (playerObject!=null) playerTransform = playerObject.transform;
        else Debug.Log("No playerObject found.");

        initialCoords = playerTransform.localPosition;


    }
	
	// Update is called once per frame
	void Update () {
        if (!isHoldingPlayer && Input.GetButtonUp("Submit") && (Time.time > time + holdTimer))
        {
            catchCords = playerTransform.position;
            isHoldingPlayer = true;

        }
        if (isHoldingPlayer)
        {
            if (__DEBUG__) Debug.Log("Holding player");
            HoldObject();//ref playerTransform);
            time = Time.time;
            if (Input.GetButtonDown("Submit")) isHoldingPlayer = false; // NOTE: Breaks whole function because of no time delay.
        }

        if (Input.GetKey(KeyCode.R)) ResetPosition();
        if (Input.GetKey(KeyCode.Alpha0)) SceneManager.LoadScene(0);
        if (Input.GetKey(KeyCode.Alpha1)) SceneManager.LoadScene(1);
        if (Input.GetKey(KeyCode.Alpha2)) SceneManager.LoadScene(2);
        if (Input.GetKey(KeyCode.Alpha3)) SceneManager.LoadScene(3);
        if (Input.GetKey(KeyCode.Alpha4)) SceneManager.LoadScene(4);
    }

    void HoldObject ()//ref Transform objTrans)
    {
        if (__DEBUG__) Debug.Log("Entered HoldObject");
        //Vector3 catchCords = playerTransform.position;
        playerTransform.position = catchCords;
    }

    IEnumerator WaitForSeconds(float sec)
    {
        yield return new WaitForSeconds(sec);
    }

    void ResetPosition()
    {
        playerTransform.localPosition = initialCoords;
    }
}
