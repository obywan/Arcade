using UnityEngine;

public class DamageFX : IOnScreenVFX
{

    public void SpawnFX(Vector2 screenPosition)
    {
        RectTransform dmgFXGo = ObjectPooler.SharedInstance.GetPooledObject(1).GetComponent<RectTransform>();
        dmgFXGo.anchoredPosition = screenPosition;
        dmgFXGo.gameObject.SetActive(true);
        
    }
}