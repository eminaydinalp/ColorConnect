using System;
using UnityEngine;

namespace _GAME.__Scripts.Controller
{
    public class CameraSizeController : MonoBehaviour
    {
        private CubeController[] _cubeControllers;

        public GameObject rightObject;
        public GameObject leftObject;

        public float ofset;
        
        public float minXPos;
        public float maxXPos;
        private void Start()
        {
            _cubeControllers = FindObjectsOfType<CubeController>();


            for (int i = 0; i < _cubeControllers.Length; i++)
            {

                if (_cubeControllers[i].CalculateXPos() >= maxXPos)
                {
                    maxXPos = _cubeControllers[i].CalculateXPos();
                }

                if (_cubeControllers[i].CalculateXPos() <= minXPos)
                {
                    minXPos = _cubeControllers[i].CalculateXPos();
                }
            }
            
            
            
            rightObject.transform.AddWorldX(maxXPos - ofset);
            leftObject.transform.AddWorldX(minXPos + ofset);
            
            
        }
    }
}
