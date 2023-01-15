using _GAME.__Scripts._Managers;
using _GAME.__Scripts.Controller;
using DG.Tweening;
using LiquidVolumeFX;
using UnityEngine;

namespace _GAME.__Scripts.Liquid
{
    public class LiquidProcess : MonoBehaviour
    {
        public CubeController cubeController;
        
        [SerializeField] private LiquidVolume _liquidVolume;

        private void Update()
        {
            _liquidVolume.UpdateLayers(true);
        }

        public void InitialColor(Color targetColor) 
        {
            _liquidVolume.liquidLayers[1].color = targetColor;
            _liquidVolume.liquidLayers[1].murkColor = targetColor;
            
            _liquidVolume.liquidLayers[1].amount = 1;
        }

        public void ChargeProcess(Color targetColor, TypeColor colorName)
        {
            if (cubeController.isFull)
            {
                ChargeProcessNewColor(targetColor, colorName);
                OtherCubeCharge();
            }
            else
            {
                Charge(targetColor, colorName);
            }
        }

        private void OtherCubeCharge()
        {
            if (cubeController.directionFlow == DirectionFlow.Right)
            {
                cubeController.rightCube.ChargeColor(cubeController.color, cubeController.typeColor, cubeController.directionFlow);
                
                cubeController.rightCube.rightPipe.ChangeClor(ColorController.Instance.GetColor(cubeController.typeColor).targetColor);
                cubeController.rightCube.rightPipe.hosePump.isRight = true;
                cubeController.rightCube.rightPipe.EnableHosePump();
            }
            else if (cubeController.directionFlow == DirectionFlow.Left)
            {
                cubeController.leftCube.ChargeColor(cubeController.color, cubeController.typeColor, cubeController.directionFlow);
                
                cubeController.leftCube.leftPipe.ChangeClor(ColorController.Instance.GetColor(cubeController.typeColor).targetColor);
                cubeController.leftCube.leftPipe.hosePump.isRight = false;
                cubeController.leftCube.leftPipe.EnableHosePump();
            }
            else
            {
                if (!cubeController.leftCube.isFull)
                {
                    cubeController.leftCube.ChargeColor(ColorController.Instance.GetColor(cubeController.typeColor).targetColor, cubeController.typeColor, cubeController.directionFlow);
                }
                else if(!cubeController.rightCube.isFull)
                {
                    cubeController.rightCube.ChargeColor(ColorController.Instance.GetColor(cubeController.typeColor).targetColor, cubeController.typeColor, cubeController.directionFlow);
                }
            }
        }
        private void ChargeProcessNewColor(Color targetColor, TypeColor colorName)
        {
            _liquidVolume.liquidLayers[0].color = targetColor;
            _liquidVolume.liquidLayers[0].murkColor = targetColor;

        
            DOVirtual.Float(0, 1, 2,
                value => _liquidVolume.liquidLayers[0].amount = value)
                .OnComplete((() =>
                {
                    _liquidVolume.liquidLayers[1].color = targetColor;
                    _liquidVolume.liquidLayers[1].murkColor = targetColor;
                    _liquidVolume.liquidLayers[1].amount = 1;
                    _liquidVolume.liquidLayers[0].amount = 0;


                    if (cubeController.directionFlow == DirectionFlow.Right && cubeController.rightCube != null)
                    {
                        cubeController.leftPipe.ChangeClor(cubeController.leftPipe.defaultColor);
                        cubeController.leftPipe.DisableHosePump();
                    }
                    
                    if (cubeController.directionFlow == DirectionFlow.Left && cubeController.leftCube != null)
                    {
                        cubeController.rightPipe.ChangeClor(cubeController.rightPipe.defaultColor);
                        cubeController.rightPipe.DisableHosePump();
                    }
                    
                    ChargeFinish(targetColor, colorName);
                }));
        }

        private void Charge(Color targetColor, TypeColor colorName)
        {

            _liquidVolume.liquidLayers[1].color = targetColor;
            _liquidVolume.liquidLayers[1].murkColor = targetColor;

        
            DOVirtual.Float(0, 1, 2,
                value => _liquidVolume.liquidLayers[1].amount = value)
                .OnComplete((() =>
                {
                    ChargeFinish(targetColor, colorName);
                }));
        }

        public void DisChargeProcess()
        {
            if (cubeController.directionFlow == DirectionFlow.Right && cubeController.rightCube != null)
            {
                if (cubeController.rightCube.isFull)
                {
                    ChargeProcessNewColor(cubeController.rightCube.color, cubeController.rightCube.typeColor);
                    cubeController.rightCube.DischargeColor(cubeController.directionFlow);
                    cubeController.leftPipe.ChangeClor(ColorController.Instance.GetColor(cubeController.rightCube.typeColor).targetColor);
                    cubeController.leftPipe.hosePump.isRight = false;
                    cubeController.leftPipe.EnableHosePump();
                }
                else
                {
                    DisCharge();
                }
                
            }
            else if (cubeController.directionFlow == DirectionFlow.Left && cubeController.leftCube != null)
            {
                if (cubeController.leftCube.isFull)
                {
                    ChargeProcessNewColor(cubeController.leftCube.color, cubeController.leftCube.typeColor);
                    cubeController.leftCube.DischargeColor(cubeController.directionFlow);
                    cubeController.rightPipe.ChangeClor(ColorController.Instance.GetColor(cubeController.leftCube.typeColor).targetColor);
                    cubeController.rightPipe.hosePump.isRight = true;
                    cubeController.rightPipe.EnableHosePump();
                }
                else
                {
                    DisCharge();
                }
            }
            else
            {
                DisCharge();
            }
        }

        public void DisCharge()
        {
            if(!cubeController.isFull) return;
            DOVirtual.Float(1, 0, 2,
                value => _liquidVolume.liquidLayers[1].amount = value)
                .OnComplete(DisChargeFinish);

            cubeController.isFull = false;
        }

        private void ChargeFinish(Color targetColor, TypeColor colorName)
        {
            cubeController.isFull = true;
            cubeController.color = targetColor;
            cubeController.typeColor = colorName;
            
            if (cubeController.directionFlow == DirectionFlow.Right && cubeController.rightPipe != null)
            {
                cubeController.rightPipe.ChangeClor(cubeController.rightPipe.defaultColor);
                cubeController.rightPipe.DisableHosePump();
            }

            if (cubeController.directionFlow == DirectionFlow.Left && cubeController.leftPipe != null)
            {
                cubeController.leftPipe.ChangeClor(cubeController.leftPipe.defaultColor);
                cubeController.leftPipe.DisableHosePump();
            }

        }

        private void DisChargeFinish()
        {
            ChargeManager.Instance.outputIndex = 0;
            ChargeManager.Instance.inputIndex = 0;
            cubeController.typeColor = TypeColor.Empty;
            cubeController.color = ColorController.Instance.GetColor(cubeController.typeColor).liquidColor;
            StartCoroutine(ChargeManager.Instance.ChargeProcessAsync());
        }
    }
}