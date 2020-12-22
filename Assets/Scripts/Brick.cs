using UnityEngine;
using System.Collections;
using System;

public class Brick : MonoBehaviour
{
    public const string BRICK_TAG = "Brick";

    [SerializeField] private TMPro.TextMeshProUGUI text;

    private int health = 5;

    public int Health 
    { 
        get => health; 
    
        set 
        { 
            health = value;
            SetHealthText();
            SetColor(Assets.Scripts.Heplers.EnemyColorHelper.GetColor(health));
        } 
    }

    public void SetColor(Color c)
    {
        GetComponent<SpriteRenderer>().color = c;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        //TODO: add sound and visuals

        if(Health <= 0)
        {
            Dispose();
        }
    }

    private void SetHealthText()
    {
        text.text = health.ToString("0.");
    }

    private void Dispose()
    {
        gameObject.SetActive(false);
        //TODO: add some fancy effects
    }

    private void OnEnable()
    {
        SetHealthText();
    }
}
