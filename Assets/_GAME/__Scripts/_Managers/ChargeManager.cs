using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _GAME.__Scripts.Controller;
using _GAME.__Scripts.Liquid;
using MoreMountains.NiceVibrations;
using Rentire.Core;
using UnityEngine;

namespace _GAME.__Scripts._Managers
{
    public class ChargeManager : Singleton<ChargeManager>
    {
        public NodeController outputNode;
        public NodeController inputNode;
        
        public CubeController inputCube;
        public CubeController outputCube;

        public DirectionFlow inputDirectionFlow;
        public DirectionFlow outputDirectionFlow;
        
        public Pipe pipe;
        
        public int outputIndex;
        public int inputIndex;

        public bool isFlow;

        public int moveCount;

        public TypeColor currentType;
        public TypeColor changeType;
        
        private void Start()
        {
            UIManager.Instance.InitialMoveCount(moveCount);
            StartCoroutine(UIManager.Instance.ShowTotalMoveCount());
        }
        

        public void ChargeProcess()
        {
            foreach (var t in outputNode.cubeControllers.Where(t => t.isFull))
            {
                outputIndex++;
                if (outputIndex == 1)
                {
                    outputCube = t;
                    break;
                }
            }
            
            foreach (var t in inputNode.cubeControllers.Where(t => !t.isFull))
            {
                inputIndex++;
                if (inputIndex == 1)
                {
                    break;
                }
            }

            if (outputIndex < 1 || inputIndex < 1)
            {
                FeedbackManager.Instance.Vibrate(HapticTypes.Warning);
                FinishDischarge();
                return;
            }
            
            if(!isFlow) FeedbackManager.Instance.Vibrate(HapticTypes.Selection);

            isFlow = true;
            
            inputCube = inputNode.cubeControllers[0];
            
            outputCube.DischargeColor(outputDirectionFlow);
            pipe.ChangeClor(ColorController.Instance.GetColor(outputCube.typeColor).targetColor);
            pipe.EnableHosePump();
            inputCube.ChargeColor(ColorController.Instance.GetColor(outputCube.typeColor).liquidColor, outputCube.typeColor,inputDirectionFlow);
        }

        public IEnumerator ChargeProcessAsync()
        {
            yield return new WaitForSeconds(0.1f);
            ChargeProcess();
        }
        
        public void FinishDischarge()
        {
            outputIndex = 0;
            inputIndex = 0;
            outputNode.isTriggerTarget = false;
            outputNode.isDrag = false;
            pipe.ChangeClor(pipe.defaultColor);
            pipe.DisableHosePump();
            outputNode.StartInvokeFindInput();

            if (isFlow)
            {
                moveCount--;
                UIManager.Instance.ShowMoveCount(moveCount);
            }
            isFlow = false;

            if (moveCount >= 0)
            {
                StartCoroutine(TargetController.Instance.TargetControl());
            }

            if (moveCount <= 0)
            {
                Debug.Log("Fail");
                StartCoroutine(FailAsync());
            }

            //StartCoroutine(TutorialManager.Instance.ShowTutorialTwoStep());
        }

        IEnumerator FailAsync()
        {
            GameManager.Instance.isFinish = true;
            yield return new WaitForSeconds(1.1f);
            GameManager.Instance.SetGameFail();
        }


    }
    
   
}