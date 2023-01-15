using System.Collections.Generic;
using _GAME.__Scripts.Controller;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace _GAME.__Scripts.OdinInspector
{
    public class CubeCreator : MonoBehaviour
    {
        private TargetController targetController;
        public GameObject[] cubesPrefabs;

        public GameObject prefab;

        public List<Cube> cubes;

        [System.Serializable]
        public class Cube
        {
            public int rowCount;
            public bool isNodeUp;
            public bool isNodeDown;
            public bool isFull;
            public TypeColor typeColor;

            public bool isTarget;
            public TypeColor targetTypeColor;

            public string targetName;
        }

        [Button("Create Cubes")]
        public void CreateCubes()
        {
#if UNITY_EDITOR
            if (cubes.Count <= 0) return;

            prefab = cubesPrefabs[cubes.Count - 1];

            prefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

            CubeInitial();
#endif
        }

        [Button("Reset")]
        public void ResetCube()
        {
            cubes = new List<Cube>();
            prefab = null;
        }

        private void CubeInitial()
        {
            Debug.Log(prefab.transform.childCount);

            for (int i = 0; i < prefab.transform.childCount; i++)
            {
                if (cubes[i].isTarget && targetController == null)
                {
                    targetController = prefab.AddComponent<TargetController>();
                }

                if (targetController != null)
                {
                    targetController.cubeControllers.Add(prefab.transform.GetChild(i).GetComponent<CubeController>());
                }

                prefab.transform.GetChild(i).GetComponent<CubeController>().CubeInitialValues(cubes[i]);
            }
        }
    }
}