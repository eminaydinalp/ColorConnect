using _GAME.__Scripts._Managers;
using _GAME.__Scripts.Controller;
using MoreMountains.NiceVibrations;
using TMPro;
using UnityEngine;

namespace _GAME.__Scripts.Target
{
    public class CheckTarget : MonoBehaviour
    {
        public CubeController cubeController;
        
        public SpriteRenderer checkIcon;
        public SpriteRenderer checkIconBackground;
        public TMP_Text checkText;

        public bool isOkay;

        private void Start()
        {
            ShowText();
        }

        private void Update()
        {
            if (cubeController.typeColor == cubeController.selfCube.targetTypeColor && !isOkay && !ChargeManager.Instance.isFlow)
            {
                isOkay = true;
                
                ShowTick();
            }
            
            if(cubeController.typeColor != cubeController.targetTypeColor && isOkay)
            {
                isOkay = false;
                ShowText();
            }

            // if (cubeController.typeColor != cubeController.targetTypeColor &&
            //     cubeController.typeColor != TypeColor.Empty)
            // {
            //     Debug.Log("Çarpı");
            // }
        }

        private void ShowTick()
        {
            checkText.gameObject.SetActive(false);
            checkIcon.gameObject.SetActive(true);
            checkIcon.color = ColorController.Instance.GetColor(cubeController.targetTypeColor).targetColor;
            checkIconBackground.gameObject.SetActive(true);
            cubeController.CubeAnim();
            FeedbackManager.Instance.Vibrate(HapticTypes.Success);
        }

        private void ShowText()
        {
            checkText.gameObject.SetActive(true);
            checkText.color = ColorController.Instance.GetColor(cubeController.targetTypeColor).textColor;
            //checkText.text = cubeController.selfCube.targetName;
            checkIcon.gameObject.SetActive(false);
            checkIconBackground.gameObject.SetActive(false);
        }
    }
}
