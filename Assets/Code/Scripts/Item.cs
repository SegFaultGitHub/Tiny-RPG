using Assets.Code.Classes.Stats;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Scripts {
    public enum Quality {
        I,
        II,
        III,
        IV,
        None
    }
    public enum ItemType { 
        Weapon,
        Shield,
        None
    }

    public class Item : MonoBehaviour {
        [SerializeField] public ItemStats Stats;
        public string Name;

        [HideInInspector] public Sprite Sprite;

        [HideInInspector] public ItemType ItemType = ItemType.None;
        [HideInInspector] public Quality Quality = Quality.None;
        [HideInInspector] public int Level;
        [HideInInspector] public List<Sprite> WeaponSprites;
        [HideInInspector] public List<Sprite> ShieldSprites;
        [HideInInspector] public Transform Parent;
        [HideInInspector] public bool IsInitialized;

        private static readonly Dictionary<AttributeType, int> ShieldAllowedTypes = new Dictionary<AttributeType, int>(){
            { AttributeType.HealthPoint, 2 },
            { AttributeType.Strength, 1 },
            { AttributeType.ManaPoint, 2 },
            { AttributeType.Wisdom, 1 },
            { AttributeType.Luck, 1 },
        };
        private static readonly Dictionary<AttributeType, int> WeaponAllowedTypes = new Dictionary<AttributeType, int>(){
            { AttributeType.HealthPoint, 1 },
            { AttributeType.Strength, 2 },
            { AttributeType.ManaPoint, 1 },
            { AttributeType.Wisdom, 2 },
            { AttributeType.Luck, 1 },
        };

        public void Start() {
            if (this.ItemType == ItemType.None) {
                ItemType[] itemTypes = {
                    ItemType.Weapon,
                    ItemType.Shield
                };
                this.ItemType = itemTypes[Random.Range(0, itemTypes.Length)];
            }
            if (this.Quality == Quality.None) {
                Quality[] qualities = {
                    Quality.I,
                    Quality.II,
                    Quality.III,
                    Quality.IV,
                };
                this.Quality = qualities[Random.Range(0, qualities.Length)];
            }

            this.GenerateStats();
            this.name = this.Name;

            if (this.transform.parent == null) { this.transform.SetParent(this.Parent); }

            //GameObject imageObject = new(this.Sprite.name);
            //imageObject.transform.SetParent(this.transform);

            //RectTransform rectTransform = imageObject.AddComponent<RectTransform>();
            //rectTransform.localScale = new(1, 1);
            //rectTransform.localRotation = new();
            //rectTransform.anchoredPosition = new();
            //rectTransform.sizeDelta = new(64, 64);

            //Image image = imageObject.AddComponent<Image>();
            //image.sprite = this.Sprite;

            this.IsInitialized = true;
        }

        public int GetAttributeValue(AttributeType attributeType) {
            return this.Stats.GetAttributeValue(attributeType);
        }

        private void GenerateStats() {
            if (this.ItemType == ItemType.None) {
                throw new("[Item:GenerateStats] Unable to generate stats with ItemType None.");
            }
            if (this.Quality == Quality.None) {
                throw new("[Item:GenerateStats] Unable to generate stats with Quality None.");
            }

            int attributesCount = this.Quality switch {
                // 1, 2
                Quality.I => Random.Range(1, 3),
                // 2, 3
                Quality.II => Random.Range(2, 4),
                // 3, 4
                Quality.III => Random.Range(3, 5),
                // 4, 5
                Quality.IV => Random.Range(4, 6),
                _ => throw new("[Item:GenerateStats] Unable to generate stats with Quality " + this.Quality + "."),
            };
            this.Stats = new ItemStats();

            Dictionary<AttributeType, int> allowedTypes;
            switch (this.ItemType) {
                case ItemType.Weapon:
                    allowedTypes = WeaponAllowedTypes;
                    this.Name = "Weapon " + this.Quality.ToString();
                    this.Stats.AddAttribute(AttributeType.HealthPoint, this.Level, this.Quality);
                    this.Stats.AddAttribute(AttributeType.Strength, this.Level, this.Quality);
                    this.Stats.RoundAttributes();
                    this.Sprite = this.WeaponSprites[Random.Range(0, this.WeaponSprites.Count)];
                    break;
                case ItemType.Shield:
                    allowedTypes = ShieldAllowedTypes;
                    this.Name = "Shield " + this.Quality.ToString();
                    this.Stats.AddAttribute(AttributeType.ManaPoint, this.Level, this.Quality);
                    this.Stats.AddAttribute(AttributeType.Wisdom, this.Level, this.Quality);
                    this.Stats.RoundAttributes();
                    this.Sprite = this.ShieldSprites[Random.Range(0, this.ShieldSprites.Count)];
                    break;
                default:
                    throw new("[Item:GenerateStats] Unable to generate stats with ItemType " + this.Quality + ".");
            }

            List<AttributeType> attributeTypes = new();
            foreach (AttributeType attribute in allowedTypes.Keys)
                for (int i = 0; i < allowedTypes[attribute]; i++)
                    attributeTypes.Add(attribute);
            for (int i = 0; i < attributesCount && attributeTypes.Count > 0; i++) {
                int index = Random.Range(0, attributeTypes.Count);
                AttributeType attributeType = attributeTypes[index];
                attributeTypes.RemoveAt(index);

                this.Stats.AddAttribute(attributeType, this.Level, this.Quality);
            }
            this.Stats.RoundAttributes();
        }
    }
}