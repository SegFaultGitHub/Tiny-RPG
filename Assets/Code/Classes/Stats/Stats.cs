using System.Collections.Generic;

namespace Assets.Code.Classes.Stats {
    public abstract class Stats {
        public List<Attribute> Attributes;

        public Stats(List<Attribute> attributes) {
            this.Attributes = attributes;
        }

        public Stats() {
            this.Attributes = new List<Attribute>();
        }

        public Attribute GetAttribute(AttributeType attributeType) {
            return this.Attributes.Find(attribute => attribute.AttributeType == attributeType);
        }

        public int GetAttributeValue(AttributeType attributeType) {
            return (int) (this.GetAttribute(attributeType)?.Value ?? 0);
        }

        public override string ToString() {
            string result = "";
            this.Attributes.ForEach(attribute => {
                result += attribute.ToString() + "\n";
            });

            return result;
        }
    }
}