using System;
using _GAME.__Scripts._Managers;
using DG.Tweening;
using Dreamteck.Splines.Primitives;
using TMPro;
using UnityEngine;

namespace _GAME.__Scripts.Uis
{
    public class GameCanvas : MonoBehaviour
    {
        public static GameCanvas Instance;
        
        public TMP_Text flowCount;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            flowCount.text = "Flow Count : " + ChargeManager.Instance.moveCount;
        }

        public void ShowFlowCount(int flowCountValue)
        {
            Debug.Log("Flow count");
            flowCount.text = "Flow Count : " + flowCountValue;
            
            flowCount.DOColor(Color.red, 0.5f)
                .OnComplete(() => flowCount.DOColor(Color.black, 0.5f));

            flowCount.transform.DOScale(Vector3.one * 1.25f, 0.5f)
                .OnComplete((() => flowCount.transform.DOScale(Vector3.one, 0.5f)));
        }
    }
}
