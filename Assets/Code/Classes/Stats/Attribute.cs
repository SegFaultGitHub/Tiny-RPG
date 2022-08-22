using Assets.Code.Scripts;
using System;
using System.Collections.Generic;

namespace Assets.Code.Classes.Stats {
    public enum AttributeType {
        HealthPoint,
        ManaPoint,
        Strength,
        Wisdom,
        Luck
    }

    [Serializable]
    public class Attribute {

        public AttributeType AttributeType;
        public float Value;
        public List<float> RawValues;
        public bool SuperAttribute;

        public Attribute(AttributeType attributeType, float value, bool superAttribute = false) {
            this.AttributeType = attributeType;
            this.Value = value;
            this.RawValues = new() { this.Value };
            this.SuperAttribute = superAttribute;
        }

        public void AddValue(float value) {
            this.RawValues.Add(value);
            this.RawValues.Sort();
            this.RawValues.Reverse();
            float newValue = 0;
            for (int i = 0; i < this.RawValues.Count; i++) {
                newValue += this.RawValues[i] / (i + 1);
            }
            this.Value = newValue;
        }

        public static Attribute GetAttribute(AttributeType attributeType, int level, Quality quality) {
            float value = AttributeGenerator.GetValue(attributeType, level, quality);
            return new(attributeType, value);
        }

        public override string ToString() {
            string attributeString = this.AttributeType switch {
                AttributeType.HealthPoint => "Health Points",
                AttributeType.ManaPoint => "Mana Points",
                AttributeType.Strength => "Strength",
                AttributeType.Wisdom => "Wisdom",
                AttributeType.Luck => "Luck",
                _ => throw new("[Attribute:ToString] Unable to get string for attribute " + this.AttributeType + ".")
            };
            return attributeString + ": " + this.Value;
        }
    }
}