using UnityEngine;
using System;

namespace VRpen.Scripts
{
    public class MovementTracker : MonoBehaviour
    {
        [SerializeField]
        private Transform _tipRepresentation;
        
        //Amount of movement before actions gets delegated
        [SerializeField]
        private float _deltaThreshold = 0.01f;
        
        private Vector3 _tipPosition = new Vector3(0,0, 0);

        public event Action<Vector3> OnControlerMove = delegate(Vector3 updatedPosition) { };

        private void Update()
        {
            if (Vector3.Distance(_tipRepresentation.transform.position, _tipPosition) > _deltaThreshold)
            {
                _tipPosition = _tipRepresentation.transform.position;
                OnControlerMove(_tipPosition);
            }
        }
        
    }
}