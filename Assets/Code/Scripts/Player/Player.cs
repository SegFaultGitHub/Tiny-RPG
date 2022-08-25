using Assets.Code.Classes.Stats;
using Assets.Code.Scripts.Items;
using Assets.Code.Scripts.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Code.Scripts.UI;

namespace Assets.Code.Scripts.Player {
    public class Player : MonoBehaviour {
        public Item Weapon, Shield;
        public PlayerStats Stats;
        public Item StartingWeapon, StartingShield;
        public SpriteRenderer WeaponSprite, ShieldSprite;

        public List<Enemy.Enemy> EnemiesInAttackRange;
        public List<Enemy.Enemy> EnemiesInCastZone;

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
            this.EnemiesInAttackRange.Where(enemy => this.Attack(enemy)).ToList().ForEach(enemy => enemy.Die());
        }

        public void PerformCast() {
            this.EnemiesInCastZone.Where(enemy => this.Cast(enemy)).ToList().ForEach(enemy => enemy.Die());
        }

        private bool Cast(Enemy.Enemy enemy) {
            return true;
        }

        private bool Attack(Enemy.Enemy enemy) {
            return true;
        }

        public void OnTriggerEnter2D(Collider2D collider) {
            if (collider.TryGetComponent(out DroppedItem droppedItem)) {
                Item item = droppedItem.Item;
                Destroy(droppedItem.gameObject);
                this.StartCoroutine(this.GenerateItemSelectionPanel(item));
            }
        }

        public IEnumerator GenerateItemSelectionPanel(Item item) {
            ItemSelectionPanel itemSelectionPanelPrefab = Resources.Load<ItemSelectionPanel>("Prefabs/ItemSelectionPanel");

            Item currentItem = null;
            if (item.ItemType == ItemType.Weapon) {
                currentItem = this.Weapon;
            } else if (item.ItemType == ItemType.Shield) {
                currentItem = this.Shield;
            }

            itemSelectionPanelPrefab.Player = this;
            itemSelectionPanelPrefab.CurrentItem = currentItem;
            itemSelectionPanelPrefab.NewItem = item;
            itemSelectionPanelPrefab.Parent = GameObject.FindGameObjectWithTag("Canvas").transform;

            ItemSelectionPanel itemSelectionPanel = Instantiate(itemSelectionPanelPrefab);
            yield return new WaitUntil(() => itemSelectionPanel.IsInitialized);


            itemSelectionPanel.GetComponent<RectTransform>().localScale = new(1, 1, 1);
            itemSelectionPanel.GetComponent<RectTransform>().localPosition = new();
        }
    }
}