using UnityEngine;

[CreateAssetMenu]
public class IntVariable : ScriptableObject, IHaveStringValue
{
    [SerializeField] private GameEvent onChangeEvent;
    public int value;

    public int Value
    {
        get
        {
            return value;
        }
        set 
        { 
            this.value = value;
            if (onChangeEvent)
                onChangeEvent.Raise();
        }
    }

    public string GetStringValue => value.ToString();
}
