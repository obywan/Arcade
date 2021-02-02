using Assets.Scripts.Heplers;
using UnityEngine;

public class ProtectiveGlass : MonoBehaviour
{
    void Start()
    {
        SetUpImageRect();
    }

    private void SetUpImageRect()
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, Screen.height - ScreenHepler.InputLimits.height);
    }
}
