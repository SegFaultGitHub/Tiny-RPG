using UnityEngine;

namespace Assets.Code.Scripts.Items {
    public class DroppedItem : MonoBehaviour {
        public Item Item;
        [HideInInspector] public Vector2 Position;

        public void Start() {
            SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();
            sprite.sprite = this.Item.Sprite;
            this.transform.position = this.Position;
        }
    }
}