using System;
using Obi;
using UnityEngine;

namespace _GAME.__Scripts._Managers
{
    public class ObiManager : MonoBehaviour
    {
        private ObiRope[] _obiRopes;
        
        private void Start()
        {
            _obiRopes = FindObjectsOfType<ObiRope>();


            for (int i = 0; i < _obiRopes.Length; i++)
            {
                _obiRopes[i].gameObject.transform.parent = transform;
            }
        }
    }
}
