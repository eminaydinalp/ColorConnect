#if DOTWEEN_API
using DG.Tweening;
#endif
using Lean.Pool;
using Rentire.Core;
using Rentire.Utils;
using UnityEngine;
using UnityEngine.UI;

public class CollectionUpdater : Singleton<CollectionUpdater>
{
    [Header("Vibration")]
    [Tooltip("Needs MM Nice Vibrations")]
    public bool IncludeVibration = false;

    [Header("Screen Animation")]
    public RectTransform CollectibleImageTransform;
    public GameObject CollectionItemPrefab;
    public float AnimationDuration;

    [Header("Text Update")]
    public Text CoinText;
    public float PunchAnimationAmount = 0.1f;

    private int totalAmount = 0;
#if DOTWEEN_API
    private Tweener punchTweener;
#endif

    private void Start()
    {
        totalAmount = UserPrefs.GetTotalCollection();
        if (CoinText)
            CoinText.text = totalAmount.ToString();
    }

    public void Collect(int amount = 1)
    {
        CollectionComplete(amount);
    }


    /// <summary>
    /// To use this, Cam Manager should have a Canvas named MasterCanvas, and UI Camera must be same size with Main Camera and must be the child of
    /// Main Camera
    /// </summary>
    /// <param name="collectiblePosition"></param>
    /// <param name="amount"></param>
    [Sirenix.OdinInspector.Button("Collect With Animation")]
    public void CollectWithAnimation(Vector3 collectiblePosition, int amount = 1)
    {
        RectTransform clone = LeanPool.Spawn(CollectionItemPrefab, CollectibleImageTransform.transform, false).GetComponent<RectTransform>();
        var point = WorldPointToCanvasLocalRectTransformPoint(collectiblePosition, Camera.main, CamManager.Instance.MasterCanvas,
            CollectibleImageTransform);
        clone.localPosition = point;

#if MOREMOUNTAINS_NICEVIBRATIONS
        if (IncludeVibration)
        {
            MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.RigidImpact);
        }
#endif

#if DOTWEEN_API
        clone.DOAnchorPos(Vector3.zero, AnimationDuration).OnComplete(() =>
        {
            CollectionComplete(amount);
            LeanPool.Despawn(clone);
        });
#endif
    }

    public Vector2 WorldPointToCanvasLocalRectTransformPoint(Vector3 worldPoint,
        Camera camera, Canvas canvas, RectTransform parentRect)
    {
        var screenPoint = camera.WorldToScreenPoint(worldPoint);
        // Translate screen point to local point of a parent rect transform
        // If canvas render mode is ScreenSpace-Overlay, camera param should be null
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect, screenPoint,
            canvas.renderMode == RenderMode.ScreenSpaceCamera ? camera : null,
            out var result);
        return result;
    }

    public static Rect RectTransformToScreenSpace(RectTransform transform, Camera cam, bool cutDecimals = false)
    {
        var worldCorners = new Vector3[4];
        var screenCorners = new Vector3[4];

        transform.GetWorldCorners(worldCorners);

        for (int i = 0; i < 4; i++)
        {
            screenCorners[i] = cam.WorldToScreenPoint(worldCorners[i]);
            if (cutDecimals)
            {
                screenCorners[i].x = (int)screenCorners[i].x;
                screenCorners[i].y = (int)screenCorners[i].y;
            }
        }

        return new Rect(screenCorners[0].x,
            screenCorners[0].y,
            screenCorners[2].x - screenCorners[0].x,
            screenCorners[2].y - screenCorners[0].y);
    }

    void CollectionComplete(int amount)
    {
#if DOTWEEN_API
        if (punchTweener == null || !punchTweener.IsPlaying())
            punchTweener = CollectibleImageTransform.DOPunchScale(Vector3.one * PunchAnimationAmount, 0.15f);
#endif
        totalAmount += amount;
        if (CoinText)
            CoinText.text = totalAmount.ToString();
        UserPrefs.IncreaseCollection(amount);


    }

}
