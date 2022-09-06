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

        [SerializeField] 
        private GameObject TipRepresentation;

        private void Awake()
        {
            GetComponent<ButtonInput>().OnSpawnObject += HandleSpawnObject;
        }

        private void HandleSpawnObject()
        {
            Vector3 pos = TipRepresentation.transform.position;
            
            SketchWorld = Instantiate(Defaults.SketchWorldPrefab).GetComponent<SketchWorld>();
            LineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            Invoker = new CommandInvoker();
            Invoker.ExecuteCommand(new AddObjectToSketchWorldRootCommand(LineSketchObject, SketchWorld));
            Invoker.ExecuteCommand(new AddControlPointCommand(this.LineSketchObject, new Vector3(pos.x, pos.y, pos.z)));
            //calling this twice, crash with less than 3 points
            Invoker.ExecuteCommand(new AddControlPointCommand(this.LineSketchObject, new Vector3(0, 0, 0)));
            Invoker.ExecuteCommand(new AddControlPointCommand(this.LineSketchObject, new Vector3(0, 0, 0)));
            Invoker.Undo();
        }
    }
}