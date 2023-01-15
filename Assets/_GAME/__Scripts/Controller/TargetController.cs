using System.Collections;
using System.Collections.Generic;
using _GAME.__Scripts._Managers;
using Rentire.Utils;
using UnityEngine;

namespace _GAME.__Scripts.Controller
{
    public class TargetController : MonoBehaviour
    {
        public static TargetController Instance;
        
        public List<CubeController> cubeControllers = new List<CubeController>();

        public int successCount;

        private void Awake()
        {
            Instance = this;
        }

        public IEnumerator TargetControl()
        {
            for (int i = 0; i < cubeControllers.Count; i++)
            {
                if (cubeControllers[i].typeColor == cubeControllers[i].targetTypeColor)
                {
                    successCount++;
                }
            }

            if (successCount == cubeControllers.Count)
            {
                GameManager.Instance.isFinish = true;
                Debug.Log("Win");
                //StartCoroutine(PunchCubes());
                yield return new WaitForSeconds(1f);
                UIManager.Instance.CloseHudUI();
                UIManager.Instance.ShowSuccessMoveLeft();
                UserPrefs.SetMoveLeft(ChargeManager.Instance.moveCount);
                StartCoroutine(UIManager.Instance.ShowTotalMoveCount());
                GameManager.Instance.SetGameSuccess();
                
            }
            else
            {
                successCount = 0;
            }
        }

        private IEnumerator PunchCubes()
        {
            for (int i = 0; i < cubeControllers.Count; i++)
            {
                cubeControllers[i].CubeAnim();
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
