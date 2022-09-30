using System;
using UnityEngine;
using Valve.VR;
using System.Collections.Generic;
using System.Linq;
using Logitech.Scripts;

namespace VRpen.Scripts.Input
{
    public class ControllerManager : MonoBehaviour
    {
        //the full name of the supported VR pen
        private static string DeviceNameOfPen = "logitech_stylus_v4.0";
        
        public static SteamVR_Input_Sources PrimaryInputSource {private set; get;}
        public static SteamVR_Input_Sources SecondaryInputSource {private set; get;}
        public static SteamVR_Behaviour_Pose PrimaryBehaviourPose {private set; get;}
        
        [SerializeField]
        private bool primaryIsLeft = false;
        [SerializeField]
        private bool preferVRPenAsPrimary = true;

        private void Update()
        {
            RefreshPrimaryController();
            RefreshPrimaryBehaviourPose();
        }

        private void RefreshPrimaryController()
        {
            if (preferVRPenAsPrimary && IsVRPenPresent())
            {
                PrimaryInputSource = PrimaryDeviceDetection.PrimaryDeviceBehaviourPose.inputSource;

                SecondaryInputSource = PrimaryDeviceDetection.NonDominantDeviceBehaviourPose.inputSource;

                //in case that a pen and another controller get bound to one hand, resolve this conflict
                if (SecondaryInputSource == PrimaryInputSource)
                {
                    SecondaryInputSource = (PrimaryInputSource == SteamVR_Input_Sources.RightHand)
                        ? SteamVR_Input_Sources.LeftHand
                        : SteamVR_Input_Sources.RightHand;
                }
            }
            else
            {
                PrimaryInputSource = primaryIsLeft
                    ? SteamVR_Input_Sources.LeftHand
                    : SteamVR_Input_Sources.RightHand;

                SecondaryInputSource = primaryIsLeft
                    ? SteamVR_Input_Sources.RightHand
                    : SteamVR_Input_Sources.LeftHand;
            }
        }

        private void RefreshPrimaryBehaviourPose()
        {
            SteamVR_Input_Sources inputSource = PrimaryInputSource;
            
            PrimaryBehaviourPose = FindObjectsOfType<SteamVR_Behaviour_Pose>()
                .First(x => x.inputSource == inputSource);
        }

        private static bool IsVRPenPresent()
        {
            return IsDeviceVRPen(PrimaryBehaviourPose);
        }
        
        private static bool IsDeviceVRPen(SteamVR_Behaviour_Pose device)
        {
            if (device != null)
            {
                string deviceName = PrimaryDeviceDetection.GetControllerProperty(device.GetDeviceIndex(),
                    ETrackedDeviceProperty.Prop_ModelNumber_String).ToLower();

                if (deviceName == DeviceNameOfPen)
                {
                    return true;
                }
            }

            return false;
        }
    }
}