using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class ListLoaderCE : EditorWindow
{
    [SerializeField]private List<GameObject> _selectedGameObjects = new List<GameObject>();
    //private List<GameObject> _currentSelectedGameObjects;

    private GameObject[] _selectedGameObjectsArray = new GameObject[] { };
    private GameObject[] _currentSelectedGameObjects;

    public Object source;
    public List<Object> sources = new List<Object>();
    public int _index = 1;
    private bool _isActive;

    [MenuItem("Automation/Physical Simulator")]
    public static void OpenSimulatorWindow()
    {
        GetWindow(typeof(ListLoaderCE));
    }
/*
    int newCount = Mathf.Max(0, EditorGUILayout.IntField("size", ListLoaderCE._selectedGameObjects.Count));
 while (newCount<_selectedGameObjects.Count)
     _selectedGameObjects.RemoveAt(_selectedGameObjects.Count - 1 );
 while (newCount > _selectedGameObjects.Count)
     _selectedGameObjects.Add(null);
 
 for(int i = 0; i<_selectedGameObjects.Count; i++)
 {
     _selectedGameObjects[i] = (_selectedGameObjects) EditorGUILayout.ObjectField(_selectedGameObjects[i], typeof(_selectedGameObjects));
}
*/
private void OnGUI()
    {
        GUILayout.Label("Please select Game Objects to simulate");
        GUILayout.Space(10f);

        _index = EditorGUILayout.IntSlider(_index, 1, 20);
        GUILayout.Space(10f);

        source = EditorGUILayout.ObjectField(source, typeof(GameObject), false);


        //this is what I want to below, I tried it in two different ways
        //the first way, if you drag a gameobject it basically drags it in all the fields automatically
        //the second way doesn't allow you to drag anything
        /*
         for (var i = 1; i <= _index; i++)
         {
             source = EditorGUILayout.ObjectField(source, typeof(GameObject), true);
             GUILayout.Space(2f);
         }
      for (var i = 1; i <= _index; i++)
         {
             sources[i] = EditorGUILayout.ObjectField(sources[i], typeof(Object), true);
             _selectedGameObjects.Add(sources[i] as GameObject);
             GUILayout.Space(2f);
         }
  */

        GUILayout.Space(30f);

        if (GUILayout.Button("AddToList"))
        {
            _isActive = true;

            _selectedGameObjects.Add((GameObject)source);

            Debug.Log("Components added successfully");
        }

        GUILayout.Space(30f);

        if (GUILayout.Button("Run Simulation"))
        {
            _isActive = true;

            foreach (var obj in _selectedGameObjects)
            {
                obj.AddComponent<Rigidbody>();
                obj.AddComponent<MeshRenderer>();
            }

            Debug.Log("Components added successfully");
        }

        GUILayout.Space(30f);

        if (GUILayout.Button("Stop Simulation"))
        {
            _isActive = false;

            foreach (var obj in _selectedGameObjects)
            {
                var rB = obj.GetComponent<Rigidbody>();
                var mesh = obj.GetComponent<MeshRenderer>();

                if (Application.isEditor)
                {
                    DestroyImmediate(rB);
                    DestroyImmediate(mesh);
                }
                else
                {
                    DestroyImmediate(rB);
                    DestroyImmediate(mesh);
                }
            }
        }

        GUILayout.Space(30f);

        GUILayout.Label(_isActive ? "Simulation Activated!" : "Simulation Deactivated!", EditorStyles.boldLabel);
    }

}//class
