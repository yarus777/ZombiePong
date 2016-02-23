using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Timers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Currency
{
    class CurrencySpawner : MonoBehaviour
    {
        public SpawnTimer timer;
        public GameObject coinPrefab;


        private float angle;
        private float radius;
        public  CircleCollider2D colliderRadius;

        private void Awake()
        {
            timer.OnSpawnTimerTick += CoinAppear;
        }

        private void CoinAppear()
        {
            angle = Random.Range(0, 360);
            radius = Random.Range(0, colliderRadius.radius);
            var obj = Instantiate(coinPrefab);
            obj.transform.parent = transform;
            var x = radius * Mathf.Cos(angle);
            var y = radius * Mathf.Sin(angle);
            obj.transform.localPosition = new Vector2(x, y);
            obj.transform.localScale = Vector3.one;
            obj.GetComponent<SpawnedCoin>().Collected += OnCollect;
        }

        private void OnCollect(SpawnedCoin obj)
        {
            Destroy(obj.gameObject);
            Debug.Log("Object" + obj);
        }
    
    }
}
