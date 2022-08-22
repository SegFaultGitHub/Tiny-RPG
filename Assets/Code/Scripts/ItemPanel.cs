using Assets.Code.Classes.Stats;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Scripts {
    public class ItemPanel : MonoBehaviour {
        public Item Item;

        [HideInInspector] public Transform Parent;

        [Header("UI Elements")]
        public TMP_Text ItemName;
        public Image ItemIcon;

        public TMP_Text HPText, HPValue;
        public TMP_Text MPText, MPValue;
        public TMP_Text StrengthText, StrengthValue;
        public TMP_Text WisdomText, WisdomValue;
        public TMP_Text LuckText, LuckValue;

        public GameObject SelectBox;

        [HideInInspector] public bool IsInitialized = false;
        private bool AnimationBlocked = false;

        // Must call `Open` to see the window
        public void Start() {
            this.transform.Find("Box").transform.localScale = new(0, 0, 1);
            this.name = "Frame: " + (this.Item == null ? "Empty" : this.Item.Name);
            if (this.transform.parent == null) { this.transform.SetParent(this.Parent); }

            if (this.Item == null) {
                this.gameObject.SetActive(false);
                this.IsInitialized = true;
                return;
            }
            this.ItemName.text = this.Item.Name;
            this.ItemIcon.sprite = this.Item.Sprite;

            int HP = this.Item.GetAttributeValue(AttributeType.HealthPoint);
            if (HP == 0) {
                this.HPText.color = Color.gray;
                this.HPValue.color = Color.gray;
            }
            this.HPValue.text = HP.ToString();

            int MP = this.Item.GetAttributeValue(AttributeType.ManaPoint);
            if (MP == 0) {
                this.MPText.color = Color.gray;
                this.MPValue.color = Color.gray;
            }
            this.MPValue.text = MP.ToString();

            int strength = this.Item.GetAttributeValue(AttributeType.Strength);
            if (strength == 0) {
                this.StrengthText.color = Color.gray;
                this.StrengthValue.color = Color.gray;
            }
            this.StrengthValue.text = strength.ToString();

            int wisdom = this.Item.GetAttributeValue(AttributeType.Wisdom);
            if (wisdom == 0) {
                this.WisdomText.color = Color.gray;
                this.WisdomValue.color = Color.gray;
            }
            this.WisdomValue.text = wisdom.ToString();

            int luck = this.Item.GetAttributeValue(AttributeType.Luck);
            if (luck == 0) {
                this.LuckText.color = Color.gray;
                this.LuckValue.color = Color.gray;
            }
            this.LuckValue.text = luck.ToString();

            if (this.transform.parent == null) { this.transform.SetParent(this.Parent); }

            this.IsInitialized = true;
        }

        public void AnimateSelectButton() {
            if (this.AnimationBlocked) { return; }
            this.AnimationBlocked = true;
            float y = this.SelectBox.transform.GetComponent<RectTransform>().localPosition.y;
            LeanTween.moveLocalY(this.SelectBox.gameObject, y - 4, 0.02f).setOnComplete(() => {
                LeanTween.moveLocalY(this.SelectBox.gameObject, y, 0.02f).setOnComplete(() => this.AnimationBlocked = false);
            });
        }

        public void SetAttributeValueColors(Item item) {
            int[] hp = {
                this.Item == null ? 0 : this.Item.GetAttributeValue(AttributeType.HealthPoint),
                item == null ? 0 : item.GetAttributeValue(AttributeType.HealthPoint),
            };
            int[] mp = {
                this.Item == null ? 0 : this.Item.GetAttributeValue(AttributeType.ManaPoint),
                item == null ? 0 : item.GetAttributeValue(AttributeType.ManaPoint),
            };
            int[] strength = {
                this.Item == null ? 0 : this.Item.GetAttributeValue(AttributeType.Strength),
                item == null ? 0 : item.GetAttributeValue(AttributeType.Strength),
            };
            int[] wisdom = {
                this.Item == null ? 0 : this.Item.GetAttributeValue(AttributeType.Wisdom),
                item == null ? 0 : item.GetAttributeValue(AttributeType.Wisdom),
            };
            int[] luck = {
                this.Item == null ? 0 : this.Item.GetAttributeValue(AttributeType.Luck),
                item == null ? 0 : item.GetAttributeValue(AttributeType.Luck),
            };
            int hpDiff = hp[0] - hp[1];
            int mpDiff = mp[0] - mp[1];
            int strengthDiff = strength[0] - strength[1];
            int wisdomDiff = wisdom[0] - wisdom[1];
            int luckDiff = luck[0] - luck [1];

            if (hpDiff > 0) {
                this.HPValue.color = new Color(0, 0.5f, 0);
            } else if (hpDiff < 0) {
                this.HPValue.color = new Color(0.5f, 0, 0);
            }
            if (mpDiff > 0) {
                this.MPValue.color = new Color(0, 0.5f, 0);
            } else if (mpDiff < 0) {
                this.MPValue.color = new Color(0.5f, 0, 0);
            }
            if (strengthDiff > 0) {
                this.StrengthValue.color = new Color(0, 0.5f, 0);
            } else if (strengthDiff < 0) {
                this.StrengthValue.color = new Color(0.5f, 0, 0);
            }
            if (wisdomDiff > 0) {
                this.WisdomValue.color = new Color(0, 0.5f, 0);
            } else if (wisdomDiff < 0) {
                this.WisdomValue.color = new Color(0.5f, 0, 0);
            }
            if (luckDiff > 0) {
                this.LuckValue.color = new Color(0, 0.5f, 0);
            } else if (luckDiff < 0) {
                this.LuckValue.color = new Color(0.5f, 0, 0);
            }
        }

        public LTDescr Close() {
            return this.transform.Find("Box").LeanScale(new(0, 0, 1), 0.3f).setEaseInBack();
        }

        public LTDescr Open() {
            return this.transform.Find("Box").LeanScale(new(1, 1, 1), 0.3f).setEaseOutBack();
        }
    }
}