using System;
using System.Collections.Generic;
using System.Text;

namespace _04
{
    public class Dough
    {
        Dictionary<string, double> flourTypes;
        Dictionary<string, double> bakingTechniques;

        public Dough(string flourType, string bakingTechType, double weight)
        {
            this.FlourType = flourType;
            this.BakingTechnique = bakingTechType;
            this.Weight = weight;

        }

        private string flourType;
        public string FlourType {
            get
            {
                return flourType;
            }
            set
            {
                if (!DoughValidator.IsValidFlourType(value))
                {
                    throw new Exception("Invalid type of dough");
                }

                flourType = value;
            }
        }

        private string bakingTechnique;
        public string BakingTechnique { 
            get
            {
                return bakingTechnique;
            }
            set
            {
                if (!DoughValidator.IsValidBakingTechnique(value))
                {
                    throw new Exception("Invalid type of dough.");
                }

                bakingTechnique = value;
            }
        }

        private double weight;

        public double Weight
        {
            get 
            {
                return Weight;
            }
            set
            { 
                if (value < 1 || value > 200)
                {
                    throw new Exception("Dough weight should be in the range [1..200].");
                }
                Weight = value;
            }
        }

        public double GetTotalCalories()
        {
            return this.Weight 
                * 2 
                * DoughValidator.GetFlourModifier(this.flourType) 
                * DoughValidator.GetBakingTechniqueModifier(this.bakingTechnique);
        }
        // flour type
        //baking technique
        // (2 * 100) * 1.5 * 1.1 = 330.00


    }
}
