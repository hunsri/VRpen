using Logitech.Scripts;
using UnityEngine;
using Valve.VR;
using System;

namespace VRpen.Scripts
{
    /// <summary>
    /// Visually shows the touch position of a TrackedDevice trackpad.
    /// </summary>
    public class TouchInput : MonoBehaviour
    {
        [Header("Input")]
        public bool GetInputSourceFromStylusDetection = true;
        [Tooltip("If not using UseStylusDetection, set the SteamVR input source manually")]
        public SteamVR_Input_Sources ManualSteamVRInputSource;
        [SerializeField]
        private SteamVR_Action_Boolean _touchInput;
        [SerializeField]
        private SteamVR_Action_Vector2 _touchPosition;
        
        public event Action OnSwipeIncrease = delegate { };
        public event Action OnSwipeDecrease = delegate { };
        
        private void Update()
        {
            SteamVR_Input_Sources inputSource = GetInputSourceFromStylusDetection
                ? PrimaryDeviceDetection.PrimaryDeviceBehaviourPose.inputSource
                : ManualSteamVRInputSource;
            
            if (_touchInput.GetStateDown(inputSource))
            {
                
            }
            
            if (_touchInput.GetState(inputSource))
            {
                if (_touchPosition.delta.y < 0)
                {
                    OnSwipeIncrease();
                }
                else if (_touchPosition.delta.y > 0)
                {
                    OnSwipeDecrease();
                }
            }

            if (_touchInput.GetStateUp(inputSource))
            {

            }
        }
    }
}