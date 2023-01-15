using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _GAME.__Scripts.Controller
{
    public class ChangeColorInLoop : MonoBehaviour
    {
        public List<WhichCube> WhichCubes = new List<WhichCube>();

        private CubeController[] _cubeControllers;

        private void Start()
        {
            AddCubeList();
            ChangeColor();
        }
        
        
        private void AddCubeList()
        {
            if (LevelManager.Instance.CurrentLevelNo <= LevelManager.Instance.TotalLevelCount) return;
            
            _cubeControllers = FindObjectsOfType<CubeController>();

            foreach (var cubeController in _cubeControllers)
            {
                if (cubeController.typeColor != TypeColor.Empty)
                {
                    var selectedCube = WhichCubes.FirstOrDefault(x => x.typeColor == cubeController.selfCube.typeColor);
                    selectedCube?.cubeControllers.Add(cubeController);
                }
            
                if (cubeController.targetTypeColor != TypeColor.Empty)
                {
                    var selectedCube = WhichCubes.FirstOrDefault(x => x.typeColor == cubeController.selfCube.targetTypeColor);
                    selectedCube?.cubeControllers.Add(cubeController);
                }
            }
            
        }

        private void ChangeColor()
        {
            if (LevelManager.Instance.CurrentLevelNo <= LevelManager.Instance.TotalLevelCount) return;

            var rnd = new System.Random();
            
            foreach (var whichCube in WhichCubes)
            {
                if(whichCube.cubeControllers.Count == 0) continue;

                int randomIndex = rnd.Next(1, 6);
                
                TypeColor newTypeColor = (TypeColor)randomIndex;
                
                Debug.Log("Random Type  : " + newTypeColor.ToString());


                foreach (var cubeController in whichCube.cubeControllers)
                {
                    if (cubeController.selfCube.typeColor != TypeColor.Empty)
                    {
                        cubeController.selfCube.typeColor = newTypeColor;
                    }

                    if (cubeController.selfCube.targetTypeColor != TypeColor.Empty)
                    {
                        cubeController.selfCube.targetTypeColor = newTypeColor;
                    }
                }
            }
        }
    
    }

    [System.Serializable]
    public class WhichCube
    {
        public TypeColor typeColor;

        public List<CubeController> cubeControllers;
    }
}