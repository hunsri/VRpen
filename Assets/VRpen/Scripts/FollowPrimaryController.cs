using System.Linq;
using UnityEngine;
using Logitech.Scripts;

using Valve.VR;

namespace VRpen.Scripts
{
    public class FollowPrimaryController : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            gameObject.transform.position = ControllerManager.PrimaryBehaviourPose.transform.position;
        }
    }
}