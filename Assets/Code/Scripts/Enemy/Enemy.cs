using Assets.Code.Scripts.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Scripts.Enemy {
    public class Enemy : MonoBehaviour {
        [Serializable]
        public class LootEntry {
            public float Weigth;
            public Quality Quality;
        }

        public float LootChance;
        public int Level;
        [SerializeField] public List<LootEntry> LootTable;

        private Player.Player Player;

        private static readonly int MAX_ESTIMATED_LUCK = 128;
        private static readonly int MAX_LUCK_FACTOR = 10;
        private static readonly float LUCK_IMPACT_FACTOR = 0.67f;

        public void Start() {
            this.Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player.Player>();
        }

        public void OnTriggerEnter2D(Collider2D collider) {
            if (collider.CompareTag("AttackTarget")) {
                this.Player.EnemiesInRange.Add(this);
            }
        }

        public void OnTriggerExit2D(Collider2D collider) {
            if (collider.CompareTag("AttackTarget")) {
                this.Player.EnemiesInRange.Remove(this);
            }
        }

        public Item GenerateLoot() {
            if (UnityEngine.Random.Range(0f, 1f) > this.LootChance) { return null; }

            float increasedLootRate = this.GetIncreasedLootRate();
            List<(float, Quality)> lootRepartition = new();

            this.LootTable.ForEach(lootEntry => {
                switch (lootEntry.Quality) {
                    case Quality.I:
                        lootRepartition.Add(new(lootEntry.Weigth * (1 + increasedLootRate / 8), lootEntry.Quality));
                        break;
                    case Quality.II:
                        lootRepartition.Add(new(lootEntry.Weigth * (1 + increasedLootRate / 5), lootEntry.Quality));
                        break;
                    case Quality.III:
                        lootRepartition.Add(new(lootEntry.Weigth * (1 + increasedLootRate / 2), lootEntry.Quality));
                        break;
                    case Quality.IV:
                        lootRepartition.Add(new(lootEntry.Weigth * (1 + increasedLootRate), lootEntry.Quality));
                        break;
                    case Quality.None:
                        throw new("[Enemy:GenerateLoot] Unexpected Quality in LootTable.");
                };
            });

            Quality quality = Utils.RandomRange(lootRepartition);
            int level = Math.Clamp(this.Level + UnityEngine.Random.Range(-2, 3), 1, 20);

            Item prefab = Resources.Load<Item>("Prefabs/Item");
            prefab.Quality = quality;
            prefab.Level = level;
            Item item = Instantiate(prefab);
            item.name = "Dropped: " + item.Name;
            return item;
        }

        private float GetIncreasedLootRate() {
            int luck = this.Player.Stats.GetAttributeValue(Classes.Stats.AttributeType.Luck);
            return (float) (Math.Pow(luck, LUCK_IMPACT_FACTOR) / Math.Pow(MAX_ESTIMATED_LUCK, LUCK_IMPACT_FACTOR)) * (MAX_LUCK_FACTOR);
        }
    }
}