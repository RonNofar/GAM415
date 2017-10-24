using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (Rigidbody))]
    [RequireComponent(typeof (CapsuleCollider))]
    public class RigidbodyFirstPersonController : MonoBehaviour
    {
        public float maxSpeed = 100;
        [SerializeField] Manifold.ManifoldPlayerHandler MPHandler;

        [Serializable]
        public class MovementSettings
        {
            
            public float ForwardSpeed = 8.0f;   // Speed when walking forward
            public float BackwardSpeed = 4.0f;  // Speed when walking backwards
            public float StrafeSpeed = 4.0f;    // Speed when walking sideways
            public float WalkMultiplier = 1.5f;   // Speed when sprinting
            public float RunMultiplier = 2.0f;   // Speed when sprinting
	        public KeyCode RunKey = KeyCode.LeftShift;
            public float JumpForce = 30f;
            public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));
            [HideInInspector] public float CurrentTargetSpeed = 8f;

#if !MOBILE_INPUT
            private bool m_Running;
#endif

            public void UpdateDesiredTargetSpeed(Vector2 input)
            {
	            if (input == Vector2.zero) return;
				if (input.x > 0 || input.x < 0)
				{
					//strafe
					CurrentTargetSpeed = StrafeSpeed;
				}
				if (input.y < 0)
				{
					//backwards
					CurrentTargetSpeed = BackwardSpeed;
				}
				if (input.y > 0)
				{
					//forwards
					//handled last as if strafing and moving forward at the same time forwards speed should take precedence
					CurrentTargetSpeed = ForwardSpeed;
				}
#if !MOBILE_INPUT
	            if (Input.GetKey(RunKey))
	            {
		            CurrentTargetSpeed *= RunMultiplier;
		            m_Running = true;
	            }
	            else
	            {
		            m_Running = false;
	            }
#endif
            }

#if !MOBILE_INPUT
            public bool Running
            {
                get { return m_Running; }
            }
#endif
        }


        [Serializable]
        public class AdvancedSettings
        {
            public float groundCheckDistance = 0.01f; // distance for checking if the controller is grounded ( 0.01f seems to work best for this )
            public float stickToGroundHelperDistance = 0.5f; // stops the character
            public float slowDownRate = 1000f; // rate at which the controller comes to a stop when there is no input
            public bool airControl; // can the user control the direction that is being moved in the air
            [Tooltip("set it to 0.1 or more if you get stuck in wall")]
            public float shellOffset; //reduce the radius by that ratio to avoid getting stuck in wall (a value of 0.1f is nice)
        }


        public Camera cam;
        public MovementSettings movementSettings = new MovementSettings();
        public MouseLook mouseLook = new MouseLook();
        public AdvancedSettings advancedSettings = new AdvancedSettings();


        private Rigidbody m_RigidBody;
        private CapsuleCollider m_Capsule;
        private float m_YRotation;
        private Vector3 m_GroundContactNormal;
        private bool m_Jump, m_PreviouslyGrounded, m_Jumping, m_IsGrounded;

        private bool m_reversedGravity = false;
        private Vector3 m_GravityForce;

        public float m_AirControlScaler = 0.5f;

        public Transform slerpCamera;
        [SerializeField] float slerpTime = 1f;

        int tapCount = 0;
        float endTime = 0;
        [SerializeField] float dashForce = 100f;
        [SerializeField] float coolDownTimer = 0.5f;
        

        public Vector3 Velocity
        {
            get { return m_RigidBody.velocity; }
        }

        public bool Grounded
        {
            get { return m_IsGrounded; }
        }

        public bool Jumping
        {
            get { return m_Jumping; }
        }

        public bool Running
        {
            get
            {
 #if !MOBILE_INPUT
				return movementSettings.Running;
#else
	            return false;
#endif
            }
        }


        private void Start()
        {
            m_RigidBody = GetComponent<Rigidbody>();
            m_Capsule = GetComponent<CapsuleCollider>();
            mouseLook.Init (transform, cam.transform);

            m_GravityForce = Physics.gravity;

            if (MPHandler == null)
            {
                MPHandler = new Manifold.ManifoldPlayerHandler();
            }
        }


        private void Update()
        {
            if (MPHandler.isIntro)
            {
                if (MPHandler.isCameraRotation)
                {
                    //Debug.Log("Velocity: "+m_RigidBody.velocity.y);
                    RotateView();
                }
            }
            else
            { 
                RotateView();
            }

            if (CrossPlatformInputManager.GetButtonDown("Jump") && !m_Jump)
            {
                m_Jump = true;
            }
            if (!m_IsGrounded)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    tapCount += 1;
                    endTime = Time.time + coolDownTimer;

                    if (tapCount == 2) // DOUBLE TAP
                    {
                        //Debug.Log("BOOP");
                        m_RigidBody.AddForce(cam.transform.forward * dashForce, ForceMode.Impulse);
                        tapCount = 0;
                    }
                    //Debug.Log(tapCount);
                }
                if (endTime < Time.time)
                {
                    tapCount = 0;
                }
            }

        }


        private void FixedUpdate()
        {
            Vector2 input = GetInput();
            //Debug.Log((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon) && (advancedSettings.airControl || m_IsGrounded));
            //Debug.Log(m_IsGrounded);
            if (!MPHandler.isIntro)
            {
                GroundCheck();
                /*Vector2 */input = GetInput();

                if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon) && (advancedSettings.airControl || m_IsGrounded))
                {
                    // always move along the camera forward as it is the direction that it being aimed at
                    Vector3 desiredMove = cam.transform.forward * input.y + cam.transform.right * input.x;
                    desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal).normalized;

                    desiredMove.x = (desiredMove.x * movementSettings.CurrentTargetSpeed) * (m_IsGrounded ? 1f : m_AirControlScaler);
                    desiredMove.z = (desiredMove.z * movementSettings.CurrentTargetSpeed) * (m_IsGrounded ? 1f : m_AirControlScaler);
                    desiredMove.y = (desiredMove.y * movementSettings.CurrentTargetSpeed) * (m_IsGrounded ? 1f : m_AirControlScaler);
                    if (m_RigidBody.velocity.sqrMagnitude <
                        (movementSettings.CurrentTargetSpeed * movementSettings.CurrentTargetSpeed))
                    {
                        //Debug.Log(SlopeMultiplier());
                        m_RigidBody.AddForce(desiredMove * SlopeMultiplier(), ForceMode.Impulse);
                    }
                    
                }

                if (m_IsGrounded)
                {
                    m_RigidBody.drag = 5f;
                    movementSettings.CurrentTargetSpeed *= movementSettings.WalkMultiplier;

                    if (m_Jump)
                    {
                        m_RigidBody.drag = 0f;
                        m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, 0f, m_RigidBody.velocity.z);
                        m_RigidBody.AddForce(new Vector3(0f, (m_reversedGravity ? -1f : 1f) * movementSettings.JumpForce, 0f), ForceMode.Impulse);
                        m_Jumping = true;
                    }

                    if (!m_Jumping && Mathf.Abs(input.x) < float.Epsilon && Mathf.Abs(input.y) < float.Epsilon && m_RigidBody.velocity.magnitude < 1f)
                    {
                        m_RigidBody.Sleep();
                    }
                }
                else
                {
                    m_RigidBody.drag = 0f;
                    movementSettings.CurrentTargetSpeed /= movementSettings.WalkMultiplier;

                    if (Mathf.Abs(input.x) < float.Epsilon && Mathf.Abs(input.y) < float.Epsilon)
                    {
                        m_RigidBody.drag = advancedSettings.slowDownRate;
                    }
                    if (m_PreviouslyGrounded && !m_Jumping)
                    {
                        StickToGroundHelper();
                    }
                    //Debug.Log("input: "+input);

                    //float hoverSpeed = 100f;
                    //if (m_Jump) m_RigidBody.AddForce(Vector3.up * hoverSpeed);
                    /*m_RigidBody.AddRelativeForce(
                        new Vector3(
                            input.x * hoverSpeed,
                            m_Jump ? hoverSpeed : 0f,
                            input.y * hoverSpeed),
                            ForceMode.Force);*/
                }
                m_Jump = false;

                if (m_RigidBody.velocity.magnitude > maxSpeed)
                {
                    m_RigidBody.velocity = m_RigidBody.velocity.normalized * maxSpeed;
                }
            }

            if (m_reversedGravity)
                ApplyReversedGravity();
        }


        private float SlopeMultiplier()
        {
            float angle = Vector3.Angle(m_GroundContactNormal, m_reversedGravity ? Vector3.down : Vector3.up);
            return movementSettings.SlopeCurveModifier.Evaluate(angle);
        }


        private void StickToGroundHelper()
        {
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), m_reversedGravity ? Vector3.up : Vector3.down, out hitInfo,
                                   ((m_Capsule.height/2f) - m_Capsule.radius) +
                                   advancedSettings.stickToGroundHelperDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                if (Mathf.Abs(Vector3.Angle(hitInfo.normal, m_reversedGravity ? Vector3.down : Vector3.up)) < 85f)
                {
                    m_RigidBody.velocity = Vector3.ProjectOnPlane(m_RigidBody.velocity, hitInfo.normal);
                }
            }
        }


        private Vector2 GetInput()
        {
            
            Vector2 input = new Vector2
                {
                    x = CrossPlatformInputManager.GetAxis("Horizontal"),
                    y = CrossPlatformInputManager.GetAxis("Vertical")
                };
			movementSettings.UpdateDesiredTargetSpeed(input);
            return input;
        }


        private void RotateView()
        {
            //avoids the mouse looking if the game is effectively paused
            if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

            // get the rotation before it's changed
            float oldYRotation = transform.eulerAngles.y;

            mouseLook.LookRotation (transform, cam.transform, m_reversedGravity);

            if (m_IsGrounded || advancedSettings.airControl)
            {
                // Rotate the rigidbody velocity to match the new direction that the character is looking
                Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
                m_RigidBody.velocity = velRotation*m_RigidBody.velocity;
            }
        }

        /// sphere cast down just beyond the bottom of the capsule to see if the capsule is colliding round the bottom
        private void GroundCheck()
        {
            m_PreviouslyGrounded = m_IsGrounded;
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position + m_Capsule.center, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), m_reversedGravity ? Vector3.up : Vector3.down, out hitInfo,
                                   ((m_Capsule.height/2f) - m_Capsule.radius) + advancedSettings.groundCheckDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                m_IsGrounded = true;
                m_GroundContactNormal = hitInfo.normal;
            }
            else
            {
                m_IsGrounded = false;
                m_GroundContactNormal = m_reversedGravity ? Vector3.down : Vector3.up;
            }
            if (!m_PreviouslyGrounded && m_IsGrounded && m_Jumping)
            {
                m_Jumping = false;
            }
        }

        #region Gravity
        public void ReverseGravity()
        {
            StartCoroutine(SlerpCamera());
            if (m_reversedGravity) // TURN OFF
            {
                m_reversedGravity = false;
                m_RigidBody.useGravity = true;
                m_Capsule.center = new Vector3(0, 0, 0);
            }
            else                   // TURN ON
            {
                m_reversedGravity = true;
                m_RigidBody.useGravity = false;
                m_Capsule.center = new Vector3(0, 1, 0);
            }
        }

        private void ApplyReversedGravity()
        {
            m_RigidBody.AddForce(-m_GravityForce, ForceMode.Acceleration);
        }

        IEnumerator SlerpCamera()
        {
            Vector3 camera = slerpCamera.localEulerAngles;
            float startTime = Time.time;
            float i = 0;
            bool firstHalf = !m_reversedGravity;
            while (i < 1)
            {
                camera = slerpCamera.localEulerAngles;
                i = (Time.time - startTime) / slerpTime;
                float temp = firstHalf ? ClampedCos(i, 0, 180) : ClampedCos(i, 180, 360);
                slerpCamera.localEulerAngles = new Vector3(
                    slerpCamera.localEulerAngles.x,
                    slerpCamera.localEulerAngles.y,
                    temp);
                yield return null;
            }

            if (firstHalf)
                slerpCamera.localEulerAngles = new Vector3(
                    slerpCamera.localEulerAngles.x,
                    slerpCamera.localEulerAngles.y,
                    180);
            else
                slerpCamera.localEulerAngles = new Vector3(
                    slerpCamera.localEulerAngles.x,
                    slerpCamera.localEulerAngles.y,
                    0);
        }

        public float ClampedCos(float timeRatio, float startValue, float endValue)
        {
            float deltaValue = endValue - startValue;
            return deltaValue / 2 + Mathf.Cos(timeRatio * Mathf.PI) * (-deltaValue / 2) + startValue;
        }
        #endregion

        private IEnumerator CoolDownAction(float time, UnityAction action)
        {
            yield return new WaitForSeconds(time);
            action.Invoke();
        }
    }
}
