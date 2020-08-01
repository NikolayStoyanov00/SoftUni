using System;
using System.Collections.Generic;
using System.Text;

namespace _04
{
    public static class DoughValidator
    {
        private static Dictionary<string, double> flourTypes;
        private static Dictionary<string, double> bakingTechniques;

        public static bool IsValidFlourType(string type)
        {
            if (flourTypes == null || bakingTechniques == null)
            {
                Initialize();
            }
            return flourTypes.ContainsKey(type);
        }

        public static double GetFlourModifier(string type) => flourTypes[type];
        public static double GetBakingTechniqueModifier(string type) => bakingTechniques[type];
        public static bool IsValidBakingTechnique(string type)
        {
            if (flourTypes == null || bakingTechniques == null)
            {
                Initialize();
            }

            return bakingTechniques.ContainsKey(type);
        }

        private static void Initialize()
        {
            flourTypes = new Dictionary<string, double>();
            bakingTechniques = new Dictionary<string, double>();

            flourTypes.Add("White", 1.5);
            flourTypes.Add("Wholerain", 1.1);

            bakingTechniques.Add("Crispy", 0.9);
            bakingTechniques.Add("Chewy", 1.1);
            bakingTechniques.Add("Homemade", 1);
        }
    }
}
