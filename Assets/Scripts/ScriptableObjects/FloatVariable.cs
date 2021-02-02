using UnityEngine;

[CreateAssetMenu]
public class FloatVariable : ScriptableObject, IHaveStringValue {

    public float value;

    public float Value { get => value; set => this.value = value; }

    public string GetStringValue => string.Format("{0:N2}", value);
}
