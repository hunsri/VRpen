using UnityEngine;

using VRSketchingGeometry.SketchObjectManagement;
using VRSketchingGeometry;
using VRSketchingGeometry.Commands;
using VRSketchingGeometry.Commands.Line;
using VRSketchingGeometry.Serialization;
using VRSketchingGeometry.Meshing;

namespace VRpen.Scripts
{
    public class SpawnObject : MonoBehaviour
    {
        public DefaultReferences Defaults;
        private LineSketchObject LineSketchObject;
        private SketchWorld SketchWorld;
        private CommandInvoker Invoker;

        private LineBrush Brush;

        private bool _drawingEnabled;

        [SerializeField]
        private float _brushScale = 0.25f;
        
        const float maxBrushScale = 0.5f;
        const float minBrushScale = 0.01f;
        private const float brushScaleChangeAbsolute = 0.01f;

        private void Start()
        {
            SketchWorld = Instantiate(Defaults.SketchWorldPrefab).GetComponent<SketchWorld>();
            Invoker = new CommandInvoker();
        }
        
        private void Awake()
        {
            GetComponent<ButtonInput>().OnDrawRequest += HandleDrawRequest;
            GetComponent<ButtonInput>().OnCancelDrawRequest += HandleCancelDrawRequest;
            GetComponent<MovementTracker>().OnControlerMove += HandleMovement;

            GetComponent<TouchInput>().OnSwipeIncrease += HandleBrushScaleIncrease;
            GetComponent<TouchInput>().OnSwipeDecrease += HandleBrushScaleDecrease;
        }

        private void HandleDrawRequest()
        {
            LineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            
            Brush = LineSketchObject.GetBrush() as LineBrush;
            Brush.CrossSectionVertices = CircularCrossSection.GenerateVertices(5);
            Brush.CrossSectionNormals = CircularCrossSection.GenerateVertices(5, 1);
            Brush.CrossSectionScale = _brushScale;
            Invoker.ExecuteCommand(new SetBrushCommand(this.LineSketchObject, Brush));
            
            _drawingEnabled = true;
        }

        private void HandleCancelDrawRequest()
        {
            _drawingEnabled = false;
        }

        private void HandleMovement(Vector3 updatedPosition)
        {
            if (_drawingEnabled)
            {
                DrawObject(updatedPosition);
            }
        }

        private void HandleBrushScaleIncrease()
        {
            if (_brushScale + brushScaleChangeAbsolute > maxBrushScale)
            {
                _brushScale = maxBrushScale;
            }
            else
            {
                _brushScale += brushScaleChangeAbsolute;
            }
        }

        private void HandleBrushScaleDecrease()
        {
            if (_brushScale - brushScaleChangeAbsolute  < minBrushScale)
            {
                _brushScale = minBrushScale;
            }
            else
            {
                _brushScale -= brushScaleChangeAbsolute;
            }
        }

        private void DrawObject(Vector3 position)
        {
            Invoker.ExecuteCommand(new AddControlPointCommand(this.LineSketchObject, position));
        }
    }
}