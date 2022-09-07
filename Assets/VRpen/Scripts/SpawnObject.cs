using UnityEngine;

using VRSketchingGeometry.SketchObjectManagement;
using VRSketchingGeometry;
using VRSketchingGeometry.Commands;
using VRSketchingGeometry.Commands.Line;

namespace VRpen.Scripts
{
    public class SpawnObject : MonoBehaviour
    {
        public DefaultReferences Defaults;
        private LineSketchObject LineSketchObject;
        private SketchWorld SketchWorld;
        private CommandInvoker Invoker;

        private bool _drawingEnabled;

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
        }

        private void HandleDrawRequest()
        {
            LineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
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

        private void DrawObject(Vector3 position)
        {
            Invoker.ExecuteCommand(new AddControlPointCommand(this.LineSketchObject, position));
        }
    }
}