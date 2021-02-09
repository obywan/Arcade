using Assets.Scripts.Heplers;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemiesSpawner : MonoBehaviour
    {
        private const float brick_width = 0.5f;

        public float spawnDelay = 3f;
        public int healthIncreaseStep = 10;

        private float innerTimer = 0f;
        private int totalNumber;
        private int lastPlacedNumber = 0;

        private int maxHealth = 6;
        private int enemiesCOunter = 0;

        private bool postponeSpawning = false;

        public bool PostponeSpawning { get => postponeSpawning; set => postponeSpawning = value; }

        public void HideAllEnemies()
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            totalNumber = Mathf.FloorToInt((ScreenHepler.WorldLimits.width - ScreenHepler.WorldLimits.x) / brick_width);
        }

        private void Update()
        {
            if (postponeSpawning)
                return;

            innerTimer += Time.deltaTime;

            if (innerTimer >= spawnDelay)
            {
                innerTimer = 0f;
                SpawnNewEnemy();
            }
        }

        private void SpawnNewEnemy()
        {
            float xVal = GetNewEnemyPosition();
            GameObject go = ObjectPooler.SharedInstance.GetPooledObject(0);
            go.transform.SetParent(transform);
            go.transform.SetPositionAndRotation(new Vector2(xVal, ScreenHepler.WorldLimits.height - 0.1f), Quaternion.identity);
            int h = Random.Range(1, maxHealth + 1);
            go.GetComponent<Brick>().Health = h;
            go.SetActive(true);
            enemiesCOunter++;

            if (enemiesCOunter % healthIncreaseStep == 0)
                maxHealth = Mathf.Clamp(maxHealth + 1, 1, 23);
        }

        private float GetNewEnemyPosition()
        {
            int np = Random.Range(1, totalNumber) - totalNumber / 2;
            while(Mathf.Abs(np - lastPlacedNumber) < 2)
            {
                np = Random.Range(1, totalNumber) - totalNumber / 2;
            }
            lastPlacedNumber = np;
            return np * brick_width + Random.Range(-brick_width * 0.3f, brick_width * 0.3f);
        }
    }
}