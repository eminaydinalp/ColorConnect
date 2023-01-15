using System.Collections;
using System.Collections.Generic;

#if dUI_MANAGER
using Doozy.Runtime.UIManager.Containers;
#endif
using MEC;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseUIManager : RMonoBehaviour
{
#if dUI_MANAGER
    protected void ToggleView(UIView view, bool isOn, bool isInstant = false, double delay = 0.0)
    {
        if (delay != 0.0)
        {
            if (isOn)
                CallMethodWithDelay(() => view.Show(isInstant), (float)delay);
            else
                CallMethodWithDelay(() => view.Hide(isInstant), (float)delay);
        }
        else
        {
            if (isOn)
                view.Show(isInstant);
            else
                view.Hide(isInstant);
        }
    }
#endif

    public void RestartGame()
    {
        Timing.KillCoroutines();
        SceneManager.LoadScene("Game");
    }
}
