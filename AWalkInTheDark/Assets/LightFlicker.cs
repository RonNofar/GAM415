using UnityEngine;
using System.Collections;

public class LightFlickering : MonoBehaviour
{ // PLANS FOR FUTURE::: public list of gameobjects to change metarial, public metarial for new one (on light/or off), private curr metarial

    [AddComponentMenu("Light Scripts")]

    public GameObject lightObject;
    public bool isFlickering = true;
    public bool isRandom = true;
    public float timeOn = 1;
    public float timeOff = 3;
    public float minimumTime = 0.1f;

    private Light flickeringLight;
    private float flickerTime = 0;

    public bool isFlickeringMaterials = true;
    public GameObject[] objectsToFlicker;
    public Material[] flickerToMaterials;
    private Material[] originalMaterials;
    private Renderer[] rendererToFlicker;

    // Use this for initialization
    void Start()
    {
        SetInitialReferences();
    }

    void SetInitialReferences()
    {
        if (lightObject != null) flickeringLight = lightObject.GetComponent<Light>(); flickeringLight.enabled = true;
        if (objectsToFlicker != null)
        {
            int len = objectsToFlicker.Length;
            originalMaterials = new Material[len];
            rendererToFlicker = new Renderer[len];
            for (int i = 0; i < len; ++i)
            {
                rendererToFlicker[i] = objectsToFlicker[i].GetComponent<Renderer>();
                originalMaterials[i] = rendererToFlicker[i].material;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlickering)
        {
            if (Time.time > flickerTime)
            {
                flickeringLight.enabled = !flickeringLight.enabled;
                if (flickeringLight.enabled)
                {
                    if (isFlickeringMaterials)
                    {
                        for (int i = 0; i < objectsToFlicker.Length; ++i) rendererToFlicker[i].material = flickerToMaterials[i];
                    }
                    float on = timeOn;
                    if (isRandom) on = Random.Range(minimumTime, timeOn);
                    flickerTime = Time.time + on;
                }
                else
                {
                    if (isFlickeringMaterials)
                    {
                        for (int i = 0; i < objectsToFlicker.Length; ++i) rendererToFlicker[i].material = originalMaterials[i];
                    }
                    float off = timeOff;
                    if (isRandom) off = Random.Range(minimumTime, timeOff);
                    flickerTime = Time.time + off;
                }
            }
        }
    }
}
