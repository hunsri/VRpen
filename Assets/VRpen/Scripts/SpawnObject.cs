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

        private void Awake()
        {
            GetComponent<ButtonInput>().OnSpawnObject += HandleSpawnObject;
        }

        private void HandleSpawnObject()
        {
            Debug.Log("Spawning!");
            SketchWorld = Instantiate(Defaults.SketchWorldPrefab).GetComponent<SketchWorld>();
            LineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            Invoker = new CommandInvoker();
            Invoker.ExecuteCommand(new AddObjectToSketchWorldRootCommand(LineSketchObject, SketchWorld));
            Invoker.ExecuteCommand(new AddControlPointCommand(this.LineSketchObject, new Vector3(1, 1, 1)));
            Invoker.ExecuteCommand(new AddControlPointCommand(this.LineSketchObject, new Vector3(0, 1, 0)));
            Invoker.ExecuteCommand(new AddControlPointCommand(this.LineSketchObject, new Vector3(0, 1, -1)));
            Invoker.ExecuteCommand(new AddControlPointCommand(this.LineSketchObject, new Vector3(1, 1, 1)));
            Invoker.Undo();
            
        }
    }
}