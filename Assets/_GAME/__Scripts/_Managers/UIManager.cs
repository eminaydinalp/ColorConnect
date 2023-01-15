using System;
using System.Collections;
using _GAME.__Scripts._Managers;
using DG.Tweening;
using Rentire.Core;
using Rentire.Utils;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Text moveCount;
    public Text totalMoveCount;
    public Text moveLeftSuccess;

    public GameObject successImage;

    public int limitRedColor;

    private void OnEnable()
    {
        
    }

    public void ShowMoveCount(int moveCountValue)
    {
        Debug.Log("Move Count");
        if(moveCountValue <= limitRedColor) moveCount.color = Color.red;
        moveCount.text = "Moves : " + moveCountValue;

        moveCount.transform.DOScale(Vector3.one * 1.1f, 0.5f)
            .OnComplete((() => moveCount.transform.DOScale(Vector3.one, 0.5f)));
    }

    public void InitialMoveCount(int moveCount)
    {
        Debug.Log("Move Count :" + moveCount);
        this.moveCount.text = "Moves : " + moveCount;
    }

    public void ShowSuccessMoveLeft()
    {
        moveLeftSuccess.text = ChargeManager.Instance.moveCount.ToString();
        successImage.SetActive(true);
    }

    public IEnumerator ShowTotalMoveCount()
    {
        yield return null;
        totalMoveCount.text = UserPrefs.GetMoveLeft().ToString();
    }

    public void CloseHudUI()
    {
        //TutorialManager.Instance.uıHud.SetActive(false);
    }
    
}
