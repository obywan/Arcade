using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ImpendingItem : MonoBehaviour
    {
        [SerializeField] private FloatVariable speed;

        private void FixedUpdate()
        {
            transform.position += Vector3.down * speed.Value * Time.fixedDeltaTime;
        }
    }
}