using System;

namespace Assets.Code.Classes.Stats {
    [Serializable]
    public class PlayerStats : Stats {
        // Players Stats handles attributes differently than Item Stats

        public void AddAttribute(Attribute attribute) {
            Attribute _attribute = this.GetAttribute(attribute.AttributeType);
            if (_attribute == null) {
                this.Attributes.Add(new(attribute.AttributeType, attribute.Value));
            } else {
                _attribute.Value += attribute.Value;
            }
        }

        public void RemoveAttribute(Attribute attribute) {
            Attribute _attribute = this.GetAttribute(attribute.AttributeType);
            if (_attribute == null) {
                this.Attributes.Add(new(attribute.AttributeType, -attribute.Value));
            } else {
                _attribute.Value -= attribute.Value;
            }
        }
    }
}