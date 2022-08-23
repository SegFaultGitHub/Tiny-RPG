using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using UnityEngine.InputSystem;

namespace Assets.Code.Scripts {
    public class ItemSelectionPanel : MonoBehaviour {
        public Player Player;
        public Item CurrentItem, NewItem;

        public ItemPanel ItemPanelPrefab;

        [HideInInspector] public Transform Parent;
        [HideInInspector] public bool IsInitialized = false;

        private ItemPanel CurrentItemPanel, NewItemPanel;
        private UIActions UIActions;

        #region Input
        private void EnableActions() {
            this.UIActions = new UIActions();
            this.UIActions.ItemSelection.Enable();

            this.UIActions.ItemSelection.KeepItem.started += this.KeepItemInput;
            this.UIActions.ItemSelection.TakeItem.started += this.TakeItemInput;
        }

        private void OnDisable() {
            this.DisableActions();
        }

        private void DisableActions() {
            this.UIActions.ItemSelection.KeepItem.started -= this.KeepItemInput;
            this.UIActions.ItemSelection.TakeItem.started -= this.TakeItemInput;

            this.UIActions.ItemSelection.Disable();
        }

        private void KeepItemInput(InputAction.CallbackContext _) {
            this.KeepItem();
        }

        private void TakeItemInput(InputAction.CallbackContext _) {
            this.TakeItem();
        }
        #endregion

        public IEnumerator Start() {
            if (this.transform.parent == null) { this.transform.SetParent(this.Parent); }

            this.ItemPanelPrefab.Parent = this.transform;

            this.CurrentItemPanel = null;
            this.NewItemPanel = null;


            this.ItemPanelPrefab.Item = this.CurrentItem;
            this.ItemPanelPrefab.KeepItemButton.SetActive(true);
            this.ItemPanelPrefab.TakeItemButton.SetActive(false);
            this.CurrentItemPanel = Instantiate(this.ItemPanelPrefab);

            this.ItemPanelPrefab.Item = this.NewItem;
            this.ItemPanelPrefab.KeepItemButton.SetActive(false);
            this.ItemPanelPrefab.TakeItemButton.SetActive(true);
            this.NewItemPanel = Instantiate(this.ItemPanelPrefab);

            yield return new WaitUntil(() => this.CurrentItemPanel.IsInitialized && this.NewItemPanel.IsInitialized);

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
            this.EnableActions();
        }

        private void KeepItem() {
            this.DisableActions();
            Destroy(this.NewItem.gameObject);
            this.CurrentItemPanel.AnimateSelectButton()
                .setOnComplete(() => {
                    this.CloseWindows(this.CurrentItemPanel, this.NewItemPanel);
                });
        }

        private void TakeItem() {
            this.DisableActions();
            this.Player.Equip(this.NewItem);
            if (this.CurrentItem != null) { Destroy(this.CurrentItem.gameObject); }
            this.NewItemPanel.AnimateSelectButton()
                .setOnComplete(() => {
                    this.CloseWindows(this.NewItemPanel, this.CurrentItemPanel);
                });
        }

        #region Animation
        private LTDescr OpenWindows() {
            const float animationDuration = 0.3f;

            if (!this.CurrentItemPanel.enabled) {
                return this.NewItemPanel.Open();
            } else {
                return this.CurrentItemPanel.Open()
                    .setOnStart(() => {
                        this.NewItemPanel.Open()
                            .setDelay(animationDuration / 2);
                        });
            }
        }

        private LTDescr CloseWindows(ItemPanel first, ItemPanel second) {
            const float animationDuration = 0.3f;

            if (!first.enabled) {
                return second.Close()
                    .setOnComplete(() => {
                        Destroy(this.gameObject);
                    });
            } else if (!second.enabled) {
                return first.Close()
                    .setOnComplete(() => {
                        Destroy(this.gameObject);
                    });
            } else {
                return first.Close()
                    .setOnStart(() => {
                        second.Close()
                            .setDelay(animationDuration / 2)
                            .setOnComplete(() => {
                                Destroy(this.gameObject);
                            });
                    });
            }
        }
        #endregion Animation
    }
}