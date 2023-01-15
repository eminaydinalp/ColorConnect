using _GAME.__Scripts._Managers;
using _GAME.__Scripts.Controller;
using Lean.Touch;
using MoreMountains.NiceVibrations;
using UnityEngine;

namespace _GAME.__Scripts.Inputs
{
    public class InputReader : MonoBehaviour
    {
        public static InputReader Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            LeanTouch.OnFingerDown += FingerDown;
        }
        
        private void OnDisable()
        {
            LeanTouch.OnFingerDown -= FingerDown;
        }
        
        public void FingerDown(LeanFinger obj)
        {
            Ray ray = Camera.main.ScreenPointToRay(obj.ScreenPosition);

            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit, 10, LayerMask.GetMask("InputTarget")))
            {
                NodeController nodeController = raycastHit.collider.GetComponent<NodeController>();
                
                if (nodeController != null && !ChargeManager.Instance.isFlow)
                {
                    FeedbackManager.Instance.Vibrate(HapticTypes.Selection);
                    
                    //TutorialManager.Instance.CloseTutorial();
                    
                    nodeController.isTriggerSelf = true;
                    nodeController.isTriggerTarget = false;
                    
                }
            }
        }

    }
}