//By Donovan Colen
using UnityEngine;
using System.Collections;
// https://answers.unity.com/questions/1350081/xbox-one-controller-mapping-solved.html

/// <summary>
/// simple script that handles input from xbox controllers and Oculus rift
/// </summary>
public class xboxCtl : MonoBehaviour
{
    // private variables
    private float dpadLR;
    private float dpadUD;

    private float lStickHorz;
    private float lStickVert;

    private float rStickHorz;
    private float rStickVert;

    private Vector2 rStick;
    private Vector2 lStick;
    private float handTriggerL;
    private float handTriggerR;

    private float triggerL;
    private float triggerR;
    private float triggerBoth;

    private int dPadXCooler;
    private int dPadYCooler;

    // to switch between using the xbox and the Oculus rift controllers due to them using the same bindings in Unity
    public bool m_useXbox;      
    public float m_tolerance = 0.1f;

    private RC_Tank m_rcRef;
    
    // simple references to player's and RC's path tips
    [SerializeField] GameObject m_playerPath;
    [SerializeField] GameObject m_RCPath;

    // to show and hide the path tips
    private bool m_showPath = false;

    // Use this for initialization
    void Start ()
    {
        m_rcRef = GetComponent<RC_Tank>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!m_useXbox)
        {
            /////////////////////////////////////////////////////////////////////////
            //OCULUS CONTROLS
            /////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////
            //JOYSTICKS
            /////////////////////////////////////////////////////////////////////////

            rStick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);

            // if the input is less than or equal to tolerance set it to zero
            if (Mathf.Abs(rStick.x) <= m_tolerance)
            {
                rStick.x = 0;
            }
            if (Mathf.Abs(rStick.y) <= m_tolerance)
            {
                rStick.y = 0;
            }

            if (rStick != Vector2.zero )
            {
                //Debug.Log("right " + rStick);
                m_rcRef.SetTouchAxisTwo(rStick);
            }
            else if (rStick == Vector2.zero)
            {
                m_rcRef.SetTouchAxisTwo(Vector2.zero);
            }

            lStick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);

            // if the input is less than or equal to tolerance set it to zero
            if (Mathf.Abs(lStick.x) <= m_tolerance)
            {
                lStick.x = 0;
            }
            if (Mathf.Abs(lStick.y) <= m_tolerance)
            {
                lStick.y = 0;
            }

            if (lStick != Vector2.zero)
            {
                //Debug.Log("left " + lStick);
                m_rcRef.SetTouchAxis(lStick);
            }
            else if (lStick == Vector2.zero)
            {
                m_rcRef.SetTouchAxis(Vector2.zero);
            }


            if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch))
            {
                //Debug.Log("RS press");
            }

            if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.LTouch))
            {
                //Debug.Log("LS press");
            }


            /////////////////////////////////////////////////////////////////////////
            //BUTTONS
            /////////////////////////////////////////////////////////////////////////


            if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch))
            {
                //Debug.Log("A");
                m_rcRef.ResetCar();
            }

            if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
            {
                //Debug.Log("B");
                m_rcRef.SpawnMarker();
            }

            if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.LTouch))
            {
                //Debug.Log("X");
            }

            if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch))
            {
                //Debug.Log("Y");
                m_showPath = !m_showPath;
                m_playerPath.SetActive(m_showPath);
                m_RCPath.SetActive(m_showPath);

            }

            /////////////////////////////////////////////////////////////////////////
            //TRIGGERS
            /////////////////////////////////////////////////////////////////////////
            triggerL = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
            if (triggerL != 0)
            {
                //Debug.Log("LTrigger " + triggerL);
            }

            triggerR = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
            if (triggerR != 0)
            {
                //Debug.Log("RTrigger " + triggerR);
                m_rcRef.SetTriggerAxis(triggerR);
            }
            else if (triggerR == 0)
            {
                m_rcRef.SetTriggerAxis(triggerR);
            }

            handTriggerL = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
            if (handTriggerL != 0)
            {
                //Debug.Log("LHandTrigger " + handTriggerL);
            }

            handTriggerR = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);
            if (handTriggerR != 0)
            {
                //Debug.Log("RHandTrigger " + handTriggerR);
            }
        }
        else
        {
            /////////////////////////////////////////////////////////////////////////
            //XBOX CONTROLS
            /////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////
            //THE FOUR BUTTONS
            /////////////////////////////////////////////////////////////////////////

            if (Input.GetButtonDown("A_Button"))
            {
                //Debug.Log("A button pressed");
                m_rcRef.ResetCar();
            }

            if (Input.GetButtonDown("B_Button"))
            {
                //Debug.Log("B button pressed");
                m_rcRef.SpawnMarker();
            }

            if (Input.GetButtonDown("X_Button"))
            {
                //Debug.Log("X button pressed");

            }

            if (Input.GetButtonDown("Y_Button"))
            {
                //Debug.Log("Y button pressed");
                m_showPath = !m_showPath;
                m_playerPath.SetActive(m_showPath);
                m_RCPath.SetActive(m_showPath);

            }

            if (Input.GetButtonDown("Back_1"))
            {
                //Debug.Log("Back button pressed");

            }


            if (Input.GetButtonDown("Start_1"))
            {
                //Debug.Log("Start button pressed");

            }

            /////////////////////////////////////////////////////////////////////////
            //THE D PAD
            /////////////////////////////////////////////////////////////////////////
            if ((Input.GetAxis("DPad_XAxis_1") != 0) && (dPadXCooler == 0))
            {
                dPadXCooler = 1;
                dpadLR = Input.GetAxis("DPad_XAxis_1");
                //Debug.Log("Dpad Left Right " + dpadLR);
            }

            if (Input.GetAxis("DPad_XAxis_1") == 0)
            {
                dPadXCooler = 0;

            }

            if ((Input.GetAxis("DPad_YAxis_1") != 0) && (dPadYCooler == 0))
            {
                dPadYCooler = 1;
                dpadUD = Input.GetAxis("DPad_YAxis_1");
                //Debug.Log("Dpad Up down " + dpadUD);
            }

            if (Input.GetAxis("DPad_YAxis_1") == 0)
            {
                dPadYCooler = 0;

            }

            /////////////////////////////////////////////////////////////////////////
            //"SHOULDER" TRIGGERS
            /////////////////////////////////////////////////////////////////////////

            if (Input.GetButtonDown("LB_1"))
            {
                //Debug.Log("Left Top Shoulder Button");
            }

            if (Input.GetButtonDown("RB_1"))
            {
                //Debug.Log("Right Top Shoulder Button");
            }

            /////////////////////////////////////////////////////////////////////////
            //ANALOG TRIGGERS
            /////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////
            //note about triggers - they work together - left goes to one, right goes to negative one, but you can seprate them out 
            //in the input settings buy setting on up to be on axis 9 and the other on axis 10
            /////////////////////////////////////////////////////////////////////////

            // Left Trigger
            if (Input.GetAxis("Triggers_1") != 0)
            {
                triggerL = Input.GetAxis("Triggers_1");

                //Debug.Log("left trigger " + triggerL);
            }
            else if (Input.GetAxis("Triggers_1") == 0)
            {
                triggerL = Input.GetAxis("Triggers_1");
            }

            // Right Trigger
            if (Input.GetAxis("Triggers_2") != 0)
            {
                triggerR = Input.GetAxis("Triggers_2");

                //Debug.Log("Right trigger" + triggerR);
                m_rcRef.SetTriggerAxis(triggerR);
            }
            else if (Input.GetAxis("Triggers_2") == 0)
            {
                triggerR = Input.GetAxis("Triggers_2");
                m_rcRef.SetTriggerAxis(triggerR);
            }

            // Both Triggers
            if (Input.GetAxis("Triggers_shared") != 0)
            {
                triggerBoth = Input.GetAxis("Triggers_shared");
                //Debug.Log("Both Triggers" + triggerBoth);
            }
            else if (Input.GetAxis("Triggers_shared") == 0)
            {
                triggerBoth = Input.GetAxis("Triggers_shared");

            }

            /////////////////////////////////////////////////////////////////////////
            //LEFT JOYSTICK
            /////////////////////////////////////////////////////////////////////////
            if (Input.GetButtonDown("LS_1"))
            {
                //Debug.Log("Left joystick press");
            }

            if (Input.GetAxis("L_XAxis_1") != 0)
            {
                lStickHorz = Input.GetAxis("L_XAxis_1");
                //Debug.Log("left stick horizontal " + lStickHorz);
                m_rcRef.SetTouchAxis(new Vector2(lStickHorz, lStickVert));
            }
            else if (Input.GetAxis("L_XAxis_1") == 0)
            {
                lStickHorz = Input.GetAxis("L_XAxis_1");
                m_rcRef.SetTouchAxis(new Vector2(lStickHorz, lStickVert));
            }

            if (Input.GetAxis("L_YAxis_1") != 0)
            {
                lStickVert = Input.GetAxis("L_YAxis_1");
                //Debug.Log("left stick vertical  " + lStickVert);
                m_rcRef.SetTouchAxis(new Vector2(lStickHorz, lStickVert));
            }
            else if (Input.GetAxis("L_YAxis_1") == 0)
            {
                lStickVert = Input.GetAxis("L_YAxis_1");
                m_rcRef.SetTouchAxis(new Vector2(lStickHorz, lStickVert));
            }

            /////////////////////////////////////////////////////////////////////////
            //RIGHT JOYSTICK
            /////////////////////////////////////////////////////////////////////////

            if (Input.GetButtonDown("RS_1"))
            {
                //Debug.Log("Right joystick press");
            }

            if (Input.GetAxis("R_XAxis_1") != 0)
            {
                rStickHorz = Input.GetAxis("R_XAxis_1");
                //Debug.Log("R stick horizontal " + rStickHorz);
                m_rcRef.SetTouchAxisTwo(new Vector2(rStickHorz, rStickVert));

            }
            else if (Input.GetAxis("R_XAxis_1") == 0)
            {
                rStickHorz = Input.GetAxis("R_XAxis_1");
                m_rcRef.SetTouchAxisTwo(new Vector2(rStickHorz, rStickVert));
            }

            if (Input.GetAxis("R_YAxis_1") != 0)
            {
                rStickVert = Input.GetAxis("R_YAxis_1");
                //Debug.Log("R stick vertical " + rStickVert);
                m_rcRef.SetTouchAxisTwo(new Vector2(rStickHorz, rStickVert));
            }
            else if (Input.GetAxis("R_YAxis_1") == 0)
            {
                rStickVert = Input.GetAxis("R_YAxis_1");
                m_rcRef.SetTouchAxisTwo(new Vector2(rStickHorz, rStickVert));
            }
        }
    }
}
