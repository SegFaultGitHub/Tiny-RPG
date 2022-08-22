using Assets.Code.Scripts;
using System.Collections;
using UnityEngine;

namespace Assets.Code.Temp {
    public class CreateItem : MonoBehaviour {
        public Item Item;
        public Player Player;

        private ItemSelectionPanel ItemSelectionPanelPrefab;

        public void Start() {
            this.ItemSelectionPanelPrefab = Resources.Load<ItemSelectionPanel>("Prefabs/ItemSelectionPanel");
        }

        public void Run() {
            this.StartCoroutine(this.RunCoroutine());
        }

        public IEnumerator RunCoroutine() {
            this.Item.Level = Random.Range(1, 21);
            this.Item.Parent = this.transform.parent;
            Item newItem = Instantiate(this.Item);
            yield return new WaitUntil(() => newItem.IsInitialized);

            Item currentItem = null;
            if (newItem.ItemType == ItemType.Weapon) {
                currentItem = this.Player.Weapon;
            } else if (newItem.ItemType == ItemType.Shield) {
                currentItem = this.Player.Shield;
            }

            this.ItemSelectionPanelPrefab.Player = this.Player;
            this.ItemSelectionPanelPrefab.CurrentItem = currentItem;
            this.ItemSelectionPanelPrefab.NewItem = newItem;
            this.ItemSelectionPanelPrefab.Parent = this.transform.parent;

            Instantiate(this.ItemSelectionPanelPrefab);

            //this.ItemPanel.Item = newItem;
            //this.ItemPanel.Parent = this.transform.parent;

            //ItemPanel itemPanel1 = Instantiate(this.ItemPanel);
            //RectTransform rectTransform1 = itemPanel1.transform.GetComponent<RectTransform>();
            //rectTransform1.localPosition = new(0, 0);

            //ItemPanel itemPanel2 = Instantiate(this.ItemPanel);
            //RectTransform rectTransform2 = itemPanel2.transform.GetComponent<RectTransform>();
            //rectTransform2.localPosition = new(500, 0);
        }
    }
}