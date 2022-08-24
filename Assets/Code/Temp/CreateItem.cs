using Assets.Code.Scripts.Items;
using Assets.Code.Scripts.Player;
using Assets.Code.Scripts.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Code.Temp {
    public class CreateItem : MonoBehaviour {
        public Player Player;

        private ItemSelectionPanel ItemSelectionPanelPrefab;
        private Item ItemPrefab;

        private UIActions UIActions;

        private void OnEnable() {
            this.UIActions = new UIActions();
            this.UIActions.Debug.Enable();

            this.UIActions.Debug.CreateItems.started += this.Run;
            this.UIActions.Debug.CreateItems.started += this.Run;
        }

        private void OnDisable() {
            this.UIActions.Debug.CreateItems.started -= this.Run;
            this.UIActions.Debug.CreateItems.started -= this.Run;

            this.UIActions.Debug.Enable();
        }

        public void Start() {
            this.ItemSelectionPanelPrefab = Resources.Load<ItemSelectionPanel>("Prefabs/ItemSelectionPanel");
            this.ItemPrefab = Resources.Load<Item>("Prefabs/Item");
        }

        public void Run(InputAction.CallbackContext _) {
            this.StartCoroutine(this.RunCoroutine());
        }

        public IEnumerator RunCoroutine() {
            this.ItemPrefab.Level = Random.Range(1, 21);
            this.ItemPrefab.Parent = this.transform.parent;
            Item newItem = Instantiate(this.ItemPrefab);
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

            ItemSelectionPanel itemSelectionPanel = Instantiate(this.ItemSelectionPanelPrefab);
            yield return new WaitUntil(() => itemSelectionPanel.IsInitialized);


            itemSelectionPanel.GetComponent<RectTransform>().localScale = new(1, 1, 1);
            itemSelectionPanel.GetComponent<RectTransform>().localPosition = new();
        }
    }
}