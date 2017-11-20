using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manifold
{
    public class PortalParticleSystem : MonoBehaviour
    {
        [SerializeField] Transform playerTransform;

        private new Transform transform;

        [SerializeField] ParticleSystem rightParticleSystem;
        [SerializeField] ParticleSystem leftParticleSystem;
        private ParticleSystem.EmissionModule rightEmissionModule;
        private ParticleSystem.EmissionModule leftEmissionModule;


        [SerializeField] float maxDistance = 4f; // Particles min
        [SerializeField] float minDistance = 1f; // Particles max

        private float currentDistance;
        private float distanceRatio;

        [SerializeField] float maxEmissionRateOverTime = 10f;
        [SerializeField] float minEmissionRateOverTime = 0f;
        [SerializeField] float maxEmisionRatio = 0.95f;
        [SerializeField] float minEmisionRatio = 0.05f;
        [SerializeField] float emissionRatioDampner = 2f;

        private float emissionRateOverTimeRatio;
        private float emissionRateOverTime;


        private void Start()
        {
            transform = GetComponent<Transform>();
            rightEmissionModule = rightParticleSystem.emission;
            leftEmissionModule = leftParticleSystem.emission;

        }

        private void FixedUpdate()
        {
            currentDistance = Vector3.Distance(transform.position, playerTransform.position);
            distanceRatio = Mathf.Clamp01((currentDistance - minDistance) / (maxDistance - minDistance));

            //Debug.Log("currentDistance: " + currentDistance + " | distanceRatio: " + distanceRatio);

            if (distanceRatio == 0)
            {
                emissionRateOverTimeRatio = 1;
            }
            else if (distanceRatio == 1)
            {
                emissionRateOverTimeRatio = 0;
            }
            else
            {
                emissionRateOverTimeRatio = (1 / (Mathf.Pow(10, emissionRatioDampner))) / distanceRatio;
                if (emissionRateOverTimeRatio >= maxEmisionRatio)
                    emissionRateOverTimeRatio = 1;
                else if (emissionRateOverTimeRatio <= minEmisionRatio)
                    emissionRateOverTimeRatio = 0;
            }

            //Debug.Log(emissionRateOverTimeRatio + " Ratio");

            emissionRateOverTime = (emissionRateOverTimeRatio * (maxEmissionRateOverTime - minEmissionRateOverTime)) + minEmissionRateOverTime;

            //Debug.Log(emissionRateOverTime);

            rightEmissionModule.rateOverTime = emissionRateOverTime;
            leftEmissionModule.rateOverTime = emissionRateOverTime;


        }
    }
}
