using Assets.Code.Classes.Stats;
using UnityEngine;
using System.Linq;

namespace Assets.Code.Scripts {
    public class Player : MonoBehaviour {
        public Item Weapon, Shield;
        public PlayerStats Stats;

        public void Start() {
            this.Stats = new();
            this.Stats.AddAttribute(new(AttributeType.HealthPoint, 25));
            this.Stats.AddAttribute(new(AttributeType.ManaPoint, 20));
            this.Stats.AddAttribute(new(AttributeType.Strength, 5));
            this.Stats.AddAttribute(new(AttributeType.Wisdom, 5));
            this.Stats.AddAttribute(new(AttributeType.Luck, 0));
        }

        public void Equip(Item item) {
            if (item == null) { return; }
            if (item.ItemType == ItemType.Weapon) {
                if (this.Weapon != null) {
                    this.Weapon.Stats.Attributes.ForEach(attribute => this.Stats.RemoveAttribute(attribute));
                }
                this.Weapon = item;
            } else if (item.ItemType == ItemType.Shield) {
                if (this.Shield != null) {
                    this.Shield.Stats.Attributes.ForEach(attribute => this.Stats.RemoveAttribute(attribute));
                }
                this.Shield = item;
            }

            item.Stats.Attributes.ForEach(attribute => this.Stats.AddAttribute(attribute));
        }
    }
}