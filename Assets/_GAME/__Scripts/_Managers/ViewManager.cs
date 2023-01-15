using System.Collections.Generic;
using System.Linq;
using Doozy.Runtime.UIManager.Containers;
using Rentire.Core;
using UnityEngine;

public class ViewManager : Singleton<ViewManager>
{
    public UIView View_GameHUD;
    public UIView View_GameSuccess;
    public UIView View_GameFail;
    public UIView View_TapToStart;

    [SerializeField]List<UIView> viewList = new();

    void Awake()
    {
        // All UI View fields in this Instance will be added to the list
        ReflectionHelpers<UIView>.CastFieldsIntoList(ref viewList, Instance);
    }

    public void ToggleView(UIView toggleView, bool isShow = true, params UIView[] otherThan)
    {
        foreach (var view in viewList)
        {
            if(view == null)
            {
                Log.Warning("VIEW IS NOT ASSIGNED!");
                continue;
            }
            if(isShow)
            {
                toggleView.Show();
                
            }
            else
            {
                Log.Info("Current not off view is : " + view.name);
                if (!otherThan.Contains(view))
                {
                    toggleView.Hide();
                    Log.Info("View - " + toggleView.name + " is hidden");
                }
            }
        }
    }

    public void Hide_View_Gift()
    {
        // View_Gift.HideBehavior.OnFinished.Event.AddListener(() =>
        // {
        //     ToggleView(View_GameSuccess, true, View_GameHUD);
        // });
        
        // View_Gift.Hide();
    }

    void View_Gift_Config()
    {
        // View_Gift.ShowBehavior.OnFinished.Event.AddListener(() =>
        // {
        //     GiftManager.Instance.IncreaseGiftPercentage();
        // });
    }

    private void GameStateChanged()
    {
        if (gameState == GameState.Running)
        {
            ToggleView(View_TapToStart,false, View_GameHUD);
        }
        else if (gameState == GameState.Success)
        {
            // if (GiftManager.Instance.IsGiftManagerActive && GiftManager.Instance.IsGiftAvailable())
            // {
            //     View_Gift_Config();
            //     ToggleView(View_Gift, true);
            // }
            // else
            ToggleView(View_GameSuccess, true, View_GameHUD);
        }
        else if (gameState == GameState.Fail)
        {
            ToggleView(View_GameFail);
        }
    }

    private void OnEnable()
    {
        if (gameManager)
            gameManager.GameStatus.OnValueChange += GameStateChanged;
    }

    private void OnDisable()
    {
        if (gameManager)
            gameManager.GameStatus.OnValueChange -= GameStateChanged;
    }
}
