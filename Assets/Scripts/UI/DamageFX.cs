using UnityEngine;

public class DamageFX : OnScreenVFX
{

    public void SpawnFX(Vector2 screenPosition)
    {
        RectTransform dmgFXGo = ObjectPooler.SharedInstance.GetPooledObject(1).GetComponent<RectTransform>();
        dmgFXGo.anchoredPosition = screenPosition;
        dmgFXGo.gameObject.SetActive(true);
        
    }
}