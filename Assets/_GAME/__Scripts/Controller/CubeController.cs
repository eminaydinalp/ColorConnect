using System;
using _GAME.__Scripts._Managers;
using _GAME.__Scripts.Liquid;
using _GAME.__Scripts.OdinInspector;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _GAME.__Scripts.Controller
{
    public class CubeController : MonoBehaviour
    {
        public TypeColor typeColor;
        public TypeColor targetTypeColor;
        
        public LiquidProcess liquidProcess;
        
        public CubeController rightCube;
        public CubeController leftCube;

        public DirectionFlow directionFlow;

        public int rowCount;
        
        public Color color;
        public Color targetColor;
    
        public bool isFull;

        public TMP_Text perfectText;

        public GameObject nodeDown;
        public GameObject nodeDown2;
        public GameObject nodeUp;
        public GameObject nodeUp2;

        public GameObject targetObject;
        public Renderer targetMesh;

        public CubeCreator.Cube selfCube;
        
        public Pipe rightPipe;
        public Pipe leftPipe;

        private void Start()
        {
            SetInitialValues();
            
            SetTargetMesh();
        }
        

        private void SetTargetMesh()
        {
            targetMesh.material.color = targetColor;
        }
        
        private void SetTargetMeshEditor()
        {
            targetMesh.sharedMaterial.color = targetColor;
        }

        public float CalculateXPos()
        {
            var transform1 = transform;
            float xPos = transform1.localPosition.x + transform1.parent.transform.position.x;
           return xPos;
        }

        public void ChargeColor(Color targetColor, TypeColor colorName,DirectionFlow directionFlowValue)
        {
            directionFlow = directionFlowValue;
            liquidProcess.ChargeProcess(targetColor, colorName);
        }
    
        public void DischargeColor(DirectionFlow directionFlowValue)
        {
            directionFlow = directionFlowValue;
            liquidProcess.DisChargeProcess();
        }

        public void CubeAnim()
        {
            //perfectText.gameObject.SetActive(true);
            perfectText.transform.DOPunchScale(Vector3.one * 0.2f, 0.5f);
            transform.DOPunchScale(Vector3.one * 0.2f, 0.5f)
                .OnComplete((() => perfectText.gameObject.SetActive(false)));
        }

        public void CubeInitialValues(CubeCreator.Cube cube)
        {
            selfCube = cube;
            
            SetInitialValues();
        }

        private void SetInitialValues()
        {
            rowCount = selfCube.rowCount;
            
            typeColor = selfCube.typeColor;
            targetTypeColor = selfCube.targetTypeColor;
            
            color = ColorController.Instance.GetColor(typeColor).liquidColor;
            
            isFull = selfCube.isFull;


            nodeDown.SetActive(selfCube.isNodeDown);
            nodeDown2.SetActive(selfCube.isNodeDown);
            nodeUp.SetActive(selfCube.isNodeUp);
            nodeUp2.SetActive(selfCube.isNodeUp);
            
            SetInitialColor();
            
            targetObject.SetActive(selfCube.isTarget);
            targetColor = ColorController.Instance.GetColor(targetTypeColor).targetColor;

            
        }

        private void SetInitialColor()
        {
            liquidProcess.InitialColor(color);
        }

        private void OnValidate()
        {
            if (nodeDown != null)
            {
                SetInitialValues();
                SetInitialColor();
                SetTargetMeshEditor();
            }
            
        }
        
    }
}
