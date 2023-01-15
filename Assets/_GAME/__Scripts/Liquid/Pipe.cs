using System;
using Dreamteck.Splines.Primitives;
using UnityEngine;

namespace _GAME.__Scripts.Liquid
{
    public class Pipe : MonoBehaviour
    {
        public HosePump hosePump;
        public MeshRenderer meshRenderer;
        
        public Color defaultColor;


        public void ChangeClor(Color newColor)
        {
            meshRenderer.material.color = newColor;
        }

        public void EnableHosePump()
        {
            if(!hosePump.enabled) hosePump.enabled = true;
            hosePump.StartPumping();
        }
        
        public void DisableHosePump()
        {
            hosePump.ResetHose();
        }
        
    }
}