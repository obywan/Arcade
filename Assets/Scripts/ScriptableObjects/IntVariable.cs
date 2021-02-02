using UnityEngine;

[CreateAssetMenu]
public class IntVariable : ScriptableObject
{
    [SerializeField] private GameEvent onChangeEvent;
    private int value;

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
}
