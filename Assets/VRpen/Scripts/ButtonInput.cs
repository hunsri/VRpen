namespace VRpen.Scripts
{
    using Logitech.Scripts;
    using UnityEngine;
    using Valve.VR;
    using System;

    /// <summary>
    /// Update the colour of a Stylus button when it is pressed.
    /// </summary>
    public class ButtonInput : MonoBehaviour
    {
        [Header("Input")]
        public bool GetInputSourceFromStylusDetection = true;
        [Tooltip("If not using UseStylusDetection, set the SteamVR input source manually")]
        public SteamVR_Input_Sources ManualSteamVRInputSource;
        [SerializeField]
        private SteamVR_Action_Boolean _input;


        public event Action OnDrawRequest = delegate { };
        public event Action OnCancelDrawRequest = delegate { };

        private void Update()
        {
            SteamVR_Input_Sources inputSource = ControllerManager.PrimaryInputSource;
            
            if (_input.GetStateDown(inputSource))
            {
                OnDrawRequest();
            }

            if (_input.GetStateUp(inputSource))
            {
                OnCancelDrawRequest();
            }
        }
    }
}
