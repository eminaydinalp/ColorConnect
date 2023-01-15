using System;
using UnityEngine.UI;

public class LevelManager : BaseLevelManager
{
    public static LevelManager Instance;

    public Text levelNoText;
    protected override void Awake()
    {
        base.Awake();
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        levelNoText.text = "Level " + LevelManager.Instance.CurrentLevelNo;
    }
}
