using UnityEngine;
using Logitech.Scripts;

namespace VRpen.Scripts
{
    public class FollowPrimaryController : MonoBehaviour
    {
        private Transform _location;
        private GameObject _gameObject;

        private Vector3 _offset = new Vector3(0, 0, 0.1f);

        // Start is called before the first frame update
        void Start()
        {
            //initially setting the location to that of the GameObject the script is attached to
            _location = transform;

            _gameObject = gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            _location = PrimaryDeviceDetection.PrimaryDeviceBehaviourPose.transform;
            _location.Translate(_offset);
            gameObject.transform.position = _location.localPosition;
        }
    }
}