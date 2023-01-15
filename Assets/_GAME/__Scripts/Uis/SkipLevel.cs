using Rentire.Utils;
using UnityEngine;

namespace _GAME.__Scripts.Uis
{
    public class SkipLevel : MonoBehaviour
    {
        public int needStar;
        
        public void SkipLevelButton()
        {
            
            Debug.Log("Skipp");
            if (UserPrefs.GetMoveLeft() >= needStar)
            {
                UserPrefs.SetMoveLeft(-needStar);
                LevelManager.Instance.IncreaseLevelNo();
                LevelManager.Instance.NextLevel();
            }    
        }
    }
}
