using Assets.Code.Scripts;
using System;
using Random = UnityEngine.Random;

namespace Assets.Code.Classes.Stats {
    public class AttributeGenerator {

        private static readonly int MAX_LEVEL = 20;

        public AttributeType attribute;
        public float variation;
        public float minValue;
        public float maxValue;
        public float ratioVariation;

        public AttributeGenerator(AttributeType attribute, float variation, float minValue, float maxValue, float ratioVariation) {
            this.attribute = attribute;
            this.variation = variation;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.ratioVariation = ratioVariation;
        }

        public static float GetValue(AttributeType attributeType, int level, Quality quality) {
            AttributeGenerator attributeGenerator = GetDefault(attributeType);
            float baseValue = (float) (Math.Pow(level, attributeGenerator.variation) / Math.Pow(MAX_LEVEL, attributeGenerator.variation)) * (attributeGenerator.maxValue - attributeGenerator.minValue) + attributeGenerator.minValue;
            float ratio = GetqualityRatio(quality) + Random.Range(-attributeGenerator.ratioVariation, attributeGenerator.ratioVariation);
            return baseValue * (1 + ratio);
        }

        public static float GetqualityRatio(Quality quality) {
            return quality switch {
                Quality.II => 0.1f,
                Quality.III => 0.15f,
                Quality.IV => 0.2f,
                _ => 0
            };
        }

        private static AttributeGenerator GetDefault(AttributeType attributeType) {
            return attributeType switch {
                AttributeType.Strength => new(attributeType, 2f, 5, 60, 0.15f),
                AttributeType.Wisdom => new(attributeType, 2f, 5, 60, 0.15f),
                AttributeType.HealthPoint => new(attributeType, 1f, 25, 500, 0.2f),
                AttributeType.ManaPoint => new(attributeType, 3f, 20, 300, 0.2f),
                AttributeType.Luck => new(attributeType, 4f, 5, 40, 0.3f),
                _ => throw new Exception("[AttributeGenerator:GetDefault] Unable to get default AttributeGenerator for Attribute " + attributeType + ".")
            };
        }
    }
}