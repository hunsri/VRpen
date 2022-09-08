using UnityEngine;
using System;

namespace VRpen.Scripts
{
    public class ScaleIndicator : MonoBehaviour
    {
        [SerializeField]
        private GameObject _indicator;

        [SerializeField]
        private GameObject _boundTo;

        private void Awake()
        {
            GetComponent<SpawnObject>().OnChangedBrushScale += HandleBrushScaleChange;
        }

        private void Start()
        {
            _indicator = Instantiate(_indicator, _boundTo.transform.position, Quaternion.identity);
        }

        private void Update()
        {
            _indicator.transform.position = _boundTo.transform.position;
        }

        private void HandleBrushScaleChange(float updatedBrushScale)
        {
            _indicator.transform.localScale = new Vector3(updatedBrushScale, updatedBrushScale, updatedBrushScale);
        }
        
    }
}