using Assets.Code.Classes.Stats;
using Assets.Code.Scripts.Items;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Scripts.Player {
    public class Player : MonoBehaviour {
        public Item Weapon, Shield;
        public PlayerStats Stats;

        public Item StartingWeapon, StartingShield;

        public SpriteRenderer WeaponSprite, ShieldSprite;

        public IEnumerator Start() {
            yield return new WaitUntil(() => this.StartingWeapon.IsInitialized && this.StartingShield.IsInitialized);
            this.Equip(this.StartingWeapon);
            this.Equip(this.StartingShield);
        }

        public void Equip(Item item) {
            if (item == null) { return; }
            switch (item.ItemType) {
                case ItemType.Weapon:
                    if (this.Weapon != null) {
                        this.Weapon.Stats.Attributes.ForEach(attribute => this.Stats.RemoveAttribute(attribute));
                    }
                    this.Weapon = item;
                    this.WeaponSprite.sprite = item.Sprite;
                    break;
                case ItemType.Shield:
                    if (this.Shield != null) {
                        this.Shield.Stats.Attributes.ForEach(attribute => this.Stats.RemoveAttribute(attribute));
                    }
                    this.Shield = item;
                    this.ShieldSprite.sprite = item.Sprite;
                    break;
                default:
                    throw new("['Player:Equip] ItemType " + item.ItemType + " is incorrect.");
            }

            item.Stats.Attributes.ForEach(attribute => this.Stats.AddAttribute(attribute));
        }

        public void PerformAttack() {
            Debug.Log("Perform attack");
        }
    }
}