using Assets.Code.Scripts;
using System;

namespace Assets.Code.Classes.Stats {
    [Serializable]
    public class ItemStats : Stats {
        // Item Stats handles attributes differently than Player Stats

        public void AddAttribute(AttributeType attributeType, int level, Quality quality) {
            Attribute newAttribute = Attribute.GetAttribute(attributeType, level, quality);
            this.AddAttribute(newAttribute);
        }

        public void AddAttribute(Attribute newAttribute) {
            Attribute attribute = this.GetAttribute(newAttribute.AttributeType);
            if (attribute is null) {
                this.Attributes.Add(newAttribute);
            } else {
                attribute.AddValue(newAttribute.Value);
            }
        }

        public void RoundAttributes() {
            this.Attributes.ForEach(attribute => attribute.Value = (float) Math.Round(attribute.Value));
            this.Attributes.Sort((attribute1, attribute2) => (int) attribute1.AttributeType - (int) attribute2.AttributeType);
        }
    }
}