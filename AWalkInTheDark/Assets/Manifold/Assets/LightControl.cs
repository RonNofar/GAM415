using UnityEngine;
using System.Collections;

public class LightControl : MonoBehaviour {

    private static Color white = Color.white;

    [SerializeField]
    private Light[] lightsArr;
    public Color[] colorsArr = { white, white, white, white };

    private Color[] fullColorArr = { Color.blue, Color.cyan, Color.green, Color.magenta, Color.red, Color.yellow };


    public float lerpDurp = 1;

	// Use this for initialization
	void Start () {
        SetInitialReferences();
	}

    void SetInitialReferences ()
    {
        lightsArr = GetComponentsInChildren<Light>();
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < lightsArr.Length; ++i)
        {
            lightsArr[i].color = colorsArr[i];
        }
        if (Input.GetKey("z") )
        {
            Color[] randomColorArr = new Color[fullColorArr.Length]; // shoudl probably be initialiaed elsewhere.
            for (int i = 0; i < fullColorArr.Length; ++i)
            {
                colorsArr[i] = Color.Lerp(colorsArr[i], fullColorArr[Random.Range(0, fullColorArr.Length)], Mathf.PingPong(Time.time, 1));
            }
            //Color randomColor = fullColorArr[Random.Range(0, fullColorArr.Length)];
            //colorsArr[0] = Color.Lerp(colorsArr[0], randomColor, Mathf.PingPong(Time.time, 10));
        }

            
    }

}
