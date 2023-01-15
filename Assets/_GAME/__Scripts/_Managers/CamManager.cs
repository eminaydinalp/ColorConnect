using System.Collections;
using System.Collections.Generic;
using Rentire.Core;
using UnityEngine;

public class CamManager : Singleton<CamManager>
{
    public Camera UICamera;
    public Canvas MasterCanvas;
    // Start is called before the first frame update
    void Awake()
    {
        if (!MasterCanvas)
        {
            MasterCanvas = GameObject.FindWithTag("MasterCanvas").GetComponent<Canvas>();
        }

        if (!UICamera)
        {
            UICamera = GameObject.FindWithTag("UICamera").GetComponent<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
