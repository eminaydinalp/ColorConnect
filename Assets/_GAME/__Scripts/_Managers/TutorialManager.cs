using System.Collections;
using UnityEngine;

namespace _GAME.__Scripts._Managers
{
    public class TutorialManager : MonoBehaviour
    {
        public static TutorialManager Instance;

        public GameObject[] tutorialImages;

        public GameObject twoStepThreeLevelTutorial;
        public GameObject twoStepFourLevelTutorial;

        public GameObject tutorialText;

        public GameObject uıHud;

        public GameObject moveLeft;

        public bool isTwoStep;
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            if (LevelManager.Instance.CurrentLevelNo < 5)
            {
                Debug.Log("Tutorial");
                uıHud.SetActive(false);
                moveLeft.SetActive(false);
                ShowTutorial();
            }
        }

        public void ShowTutorial()
        {
            tutorialImages[LevelManager.Instance.CurrentLevelNo - 1].SetActive(true);
            tutorialText.SetActive(true);
        }

        public void CloseTutorial()
        {
            if(LevelManager.Instance.CurrentLevelNo >= 5) return;

            if (isTwoStep)
            {
                CloseTutorialTwoStep();
            }
            else
            {
                tutorialImages[LevelManager.Instance.CurrentLevelNo - 1].SetActive(false);
                tutorialText.SetActive(false);
            }
            
        }

        public IEnumerator ShowTutorialTwoStep()
        {
            yield return new WaitForSeconds(0.5f);
            
            if(GameManager.Instance.isFinish) yield break;

            isTwoStep = true;
            
            if (LevelManager.Instance.CurrentLevelNo == 3)
            {
                twoStepThreeLevelTutorial.SetActive(true);
            }
            
            if (LevelManager.Instance.CurrentLevelNo == 4)
            {
                twoStepFourLevelTutorial.SetActive(true);
            }
            
        }
        
        public void CloseTutorialTwoStep()
        {
            if (LevelManager.Instance.CurrentLevelNo == 3)
            {
                twoStepThreeLevelTutorial.SetActive(false);
            }
            
            if (LevelManager.Instance.CurrentLevelNo == 4)
            {
                twoStepFourLevelTutorial.SetActive(false);
            }
        }
    }
}
