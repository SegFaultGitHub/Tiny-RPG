using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;

namespace Assets.Code.Scripts {
    public class ItemSelectionPanel : MonoBehaviour {
        public Player Player;
        public Item CurrentItem, NewItem;

        public ItemPanel ItemPanelPrefab;

        [HideInInspector] public Transform Parent;
        [HideInInspector] public bool IsInitialized = false;

        private ItemPanel CurrentItemPanel, NewItemPanel;

        public IEnumerator Start() {
            if (this.transform.parent == null) { this.transform.SetParent(this.Parent); }

            this.ItemPanelPrefab.Parent = this.transform;

            this.CurrentItemPanel = null;
            this.NewItemPanel = null;


            this.ItemPanelPrefab.Item = this.CurrentItem;
            this.ItemPanelPrefab.KeepItemButton.SetActive(true);
            this.ItemPanelPrefab.TakeItemButton.SetActive(false);
            this.CurrentItemPanel = Instantiate(this.ItemPanelPrefab);
            this.CurrentItemPanel.SelectBox.GetComponent<EventTrigger>()
                .triggers.Find(trigger => trigger.eventID.Equals(EventTriggerType.PointerClick))
                .callback.AddListener((_) => {
                    this.DisableButtons();
                    // Pass
                    Destroy(this.NewItem.gameObject);
                    this.CloseWindows(this.CurrentItemPanel, this.NewItemPanel);
                });

            this.ItemPanelPrefab.Item = this.NewItem;
            this.ItemPanelPrefab.KeepItemButton.SetActive(false);
            this.ItemPanelPrefab.TakeItemButton.SetActive(true);
            this.NewItemPanel = Instantiate(this.ItemPanelPrefab);
            this.NewItemPanel.SelectBox.GetComponent<EventTrigger>()
                .triggers.Find(trigger => trigger.eventID.Equals(EventTriggerType.PointerClick))
                .callback.AddListener((_) => {
                    this.DisableButtons();
                    this.Player.Equip(this.NewItem);
                    if (this.CurrentItem != null) { Destroy(this.CurrentItem.gameObject); }
                    this.CloseWindows(this.NewItemPanel, this.CurrentItemPanel);
                });

            yield return new WaitUntil(() => (this.CurrentItemPanel == null || this.CurrentItemPanel.IsInitialized) && this.NewItemPanel.IsInitialized);

            this.CurrentItemPanel.SetAttributeValueColors(this.NewItem);
            this.NewItemPanel.SetAttributeValueColors(this.CurrentItem);

            RectTransform rectTransform = this.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new(
                this.CurrentItemPanel.GetComponent<RectTransform>().sizeDelta.x + 100 + this.NewItemPanel.GetComponent<RectTransform>().sizeDelta.x,
                Math.Max(this.CurrentItemPanel.GetComponent<RectTransform>().sizeDelta.y, this.NewItemPanel.GetComponent<RectTransform>().sizeDelta.y)
            );

            this.CurrentItemPanel.GetComponent<RectTransform>().anchoredPosition = new(0, 0);
            this.NewItemPanel.GetComponent<RectTransform>().anchoredPosition = new(this.CurrentItemPanel.GetComponent<RectTransform>().sizeDelta.x + 100, 0);

            this.OpenWindows();
            this.IsInitialized = true;
        }

        private void DisableButtons() {
            this.CurrentItemPanel.SelectBox.GetComponent<EventTrigger>()
                .triggers.Find(trigger => trigger.eventID.Equals(EventTriggerType.PointerClick))
                .callback.RemoveAllListeners();
            this.NewItemPanel.SelectBox.GetComponent<EventTrigger>()
                .triggers.Find(trigger => trigger.eventID.Equals(EventTriggerType.PointerClick))
                .callback.RemoveAllListeners();
        }

        private void OpenWindows() {
            const float animationDuration = 0.3f;

            if (this.CurrentItem == null) {
                this.NewItemPanel.Open();
            } else {
                this.CurrentItemPanel.Open().setOnStart(() => {
                    this.NewItemPanel.Open().setDelay(animationDuration / 2);
                });
            }
        }

        private void CloseWindows(ItemPanel first, ItemPanel second) {
            const float animationDuration = 0.3f;

            if (first.Item == null) {
                second.Close().setOnComplete(() => Destroy(this.gameObject));
            } else if (second.Item == null) {
                first.Close().setOnComplete(() => Destroy(this.gameObject));
            } else {
                first.Close().setOnStart(() => {
                    second.Close().setDelay(animationDuration / 2).setOnComplete(() => Destroy(this.gameObject));
                });
            }
        }
    }
}