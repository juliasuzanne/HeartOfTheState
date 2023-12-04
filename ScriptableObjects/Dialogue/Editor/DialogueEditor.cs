using System.Collections;
using System.Collections.Generic;
using UnityEditor;

using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Callbacks;

namespace Dialogue.Editor
{

    public class DialogueEditor : EditorWindow
    {
        Dialogue selectedDialogue = null;
        [System.NonSerialized]
        Vector2 draggingOffset;
        [System.NonSerialized]
        GUIStyle nodeStyle;
        [System.NonSerialized]
        DialogueNode draggingNode = null;
        [System.NonSerialized]
        DialogueNode creatingNode = null;
        [System.NonSerialized]

        DialogueNode deletingNode = null;


        [MenuItem("Window/Dialogue Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }
        [OnOpenAssetAttribute(1)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            Dialogue selectedDialogue = Selection.activeObject as Dialogue;
            if (selectedDialogue != null)
            {
                ShowEditorWindow();
                return true;
            }
            return false;

        }

        private void OnEnable()
        {
            //selection changed event is a list of functions that something in unity will call this list of functions when this selection changes. Add a function to that list of things. 
            //allows us to have a nonstatic function

            Selection.selectionChanged += OnSelectionChanged;
            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            nodeStyle.padding = new RectOffset(20, 20, 20, 20);
            nodeStyle.border = new RectOffset(12, 12, 12, 12);
        }

        private void OnSelectionChanged()
        {
            Dialogue newDialogue = Selection.activeObject as Dialogue;
            if (newDialogue != null)
            {
                selectedDialogue = newDialogue;
                Repaint();
            }
            else
            {
                Debug.Log("Not a dialogue");
            }
            Debug.Log("On selection changed");
        }

        private void OnGUI()
        {
            if (selectedDialogue == null)
            {
                EditorGUILayout.LabelField("No Dialogue Selected");

            }
            else
            {
                ProcessEvents();
                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawConnections(node);

                }
                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawNode(node);

                }
                if (creatingNode != null)
                {
                    Undo.RecordObject(selectedDialogue, "Added Dialogue Node");
                    selectedDialogue.CreateNode(creatingNode);
                    creatingNode = null;
                }
                if (deletingNode != null)
                {
                    Undo.RecordObject(selectedDialogue, "Removed Dialogue Node");
                    selectedDialogue.DeleteNode(deletingNode);
                    deletingNode = null;
                }

            }


        }

        private void ProcessEvents()
        {
            if (Event.current.type == EventType.MouseDown && draggingNode == null)
            {
                draggingNode = GetNodeAtPoint(Event.current.mousePosition);
                if (draggingNode != null)
                {
                    draggingOffset = draggingNode.rect.position - Event.current.mousePosition;
                }
            }
            else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
            {
                Undo.RecordObject(selectedDialogue, "Update Node Pos");
                draggingNode.rect.position = Event.current.mousePosition + draggingOffset;
                GUI.changed = true;

            }
            else if (Event.current.type == EventType.MouseUp && draggingNode != null)
            {
                draggingNode = null;
            }
        }

        private DialogueNode GetNodeAtPoint(Vector2 point)
        {
            DialogueNode foundNode = null;
            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
                if (node.rect.Contains(point))
                {
                    foundNode = node;
                }

            }
            return foundNode;
        }

        private void DrawNode(DialogueNode node)
        {

            GUILayout.BeginArea(node.rect, nodeStyle);
            EditorGUI.BeginChangeCheck();
            string newText = EditorGUILayout.TextField(node.speech);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(selectedDialogue, "Update Dialogue Text");
                node.speech = newText;

            }

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("+"))
            {
                creatingNode = node;
            }

            if (GUILayout.Button("-"))
            {
                deletingNode = node;
            }
            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        private void DrawConnections(DialogueNode node)
        {
            Vector2 startPosition = new Vector2(node.rect.xMax, node.rect.center.y);


            foreach (DialogueNode childNode in selectedDialogue.GetAllChildren(node))
            {
                Vector2 endPosition = new Vector2(childNode.rect.xMin, childNode.rect.center.y);
                Vector2 controlPointOffset = endPosition - startPosition;
                controlPointOffset.y = 0;
                controlPointOffset.x *= 0.8f;

                Handles.DrawBezier(startPosition, endPosition,
                startPosition + controlPointOffset,
                endPosition - controlPointOffset,
                Color.white, null, 4f);
            }
        }
    }
}

