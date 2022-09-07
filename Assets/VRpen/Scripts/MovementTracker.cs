using UnityEngine;
using System;

namespace VRpen.Scripts
{
    public class MovementTracker : MonoBehaviour
    {
        [SerializeField]
        private Transform _tipRepresentation;

        private Vector3 _tipPosition = new Vector3(0,0, 0);
        
        public event Action<Vector3> OnControlerMove = delegate(Vector3 updatedPosition) { };

        private void Update()
        {
            if (_tipRepresentation.transform.position != _tipPosition)
            {
                _tipPosition = _tipRepresentation.transform.position;
                OnControlerMove(_tipPosition);
            }
        }
        
    }
}