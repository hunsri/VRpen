using System;
using System.Collections;
using UnityEngine;
using VRSketchingGeometry;
using VRSketchingGeometry.Commands;
using VRSketchingGeometry.Commands.Line;
using VRSketchingGeometry.SketchObjectManagement;
using VRSketchingGeometry.Commands.Group;

namespace VRpen.Scripts.Examples
{
    public class LineExample : MonoBehaviour
    {
        //A Default holds samples for all elements required to draw, such as the LineSketchObjectPrefab 
        [SerializeField]
        private DefaultReferences Defaults;
        
        private SketchWorld _sketchWorld;
        private CommandInvoker _invoker;
        
        void Start()
        {
            //Create a SketchWorld, many commands require a SketchWorld to be present
            _sketchWorld = Instantiate(Defaults.SketchWorldPrefab).GetComponent<SketchWorld>();

            //Creates a CommandInvoker that is necessary to execute commands.
            //A CommandInvoker memorizes all actions performed on it.
            _invoker = new CommandInvoker();

            CreateSnailLine(new Vector3(0, 0, 0));

            CreateFrameWithGroupedLines(new Vector3(4, 0, 4));
            CreateFrameWithContinuousLine(new Vector3(4, 0, 0));
        }

        /// <summary>
        /// Creates a simple line that uses the default values for LineSketchObject.
        /// </summary>
        private void CreateSnailLine(Vector3 position)
        {
            //Create a LineSketchObject
            LineSketchObject lineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            
            _invoker.ExecuteCommand(new AddObjectToSketchWorldRootCommand(lineSketchObject, _sketchWorld));
            _invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(0, 0, 0) + position));
            _invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(1, 0, 0) + position));
            _invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(1, 0, 1) + position));
            _invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(-1, 0, 1) + position));
            _invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(-1, 0, -1) + position));
            _invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(2, 0, -1) + position));
            _invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(2, 0, 2) + position));
            _invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(-2, 0, 2) + position));
        }
        
        /// <summary>
        /// Creates a simple line that uses the default values for LineSketchObject.
        /// Draws a square with a single line.
        /// </summary>
        private void CreateFrameWithContinuousLine(Vector3 position)
        {
            //Create a LineSketchObject
            LineSketchObject lineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            
            _invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(0, 0, 0) + position));
            _invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(0, 0, 2) + position));
            _invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(2, 0, 2) + position));
            _invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(2, 0, 0) + position));
            _invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(0, 0, 0) + position));
        }
        
        /// <summary>
        /// Draws a square with four individual lines that use the default values for LineSketchObject.
        /// All four lines get grouped together and form one SketchObjectGroup.
        /// </summary>
        private void CreateFrameWithGroupedLines(Vector3 position)
        {   
            //Create a new SketchObjectGroup
            //A Group can hold multiple SketchObjects, such as lines
            SketchObjectGroup sketchObjectGroup = Instantiate(Defaults.SketchObjectGroupPrefab).GetComponent<SketchObjectGroup>();
            
            //Create the first LineSketchObject...
            LineSketchObject lineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            //...and add it to the SketchObject Group
            _invoker.ExecuteCommand(new AddToGroupCommand(sketchObjectGroup, lineSketchObject));
            //Drawing the points, as usual
            _invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(0, 0, 0) + position));
            _invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(0, 0, 2) + position));
            
            //Create the second LineSketchObject, and add it to the group, just like the first one
            lineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            _invoker.ExecuteCommand(new AddToGroupCommand(sketchObjectGroup, lineSketchObject));
            _invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(0, 0, 2) + position));
            _invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(2, 0, 2) + position));
            
            //Create and add the third LineSketchObject
            lineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            _invoker.ExecuteCommand(new AddToGroupCommand(sketchObjectGroup, lineSketchObject));
            _invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(2, 0, 2) + position));
            _invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(2, 0, 0) + position));
            
            //And finally create and add the fourth LineSketchObject
            lineSketchObject = Instantiate(Defaults.LineSketchObjectPrefab).GetComponent<LineSketchObject>();
            _invoker.ExecuteCommand(new AddToGroupCommand(sketchObjectGroup, lineSketchObject));
            _invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(2, 0, 0) + position));
            _invoker.ExecuteCommand(new AddControlPointCommand(lineSketchObject, new Vector3(0, 0, 0) + position));

            //Adding the completed group with all its SketchObjects to the command list.
            //Undoing this command removes all structures within the group.
            _invoker.ExecuteCommand(new AddObjectToSketchWorldRootCommand(sketchObjectGroup, _sketchWorld));
        }
        
        /// <summary>
        /// Small helper method that removes a given amount of the latest actions invoked on _invoker
        /// <summary>
        public void UndoActions(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                _invoker.Undo();
            }
        }
        
        /// <summary>
        /// Small helper method that reverses the removal of a given amount of actions invoked on _invoker
        /// <summary>
        public void RedoActions(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                _invoker.Redo();
            }
        }
    }
}