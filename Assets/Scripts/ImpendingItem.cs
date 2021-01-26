using Assets.Scripts.Heplers;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ImpendingItem : MonoBehaviour
    {
        [SerializeField] private FloatVariable speed;
        [SerializeField] private GameEvent gameoverEvent;

        private void FixedUpdate()
        {
            transform.position += Vector3.down * speed.Value * Time.fixedDeltaTime;
            if(transform.position.y < ScreenHepler.DeadlineY)
            {
                Debug.Log("GameOver");
            }
        }
    }
}