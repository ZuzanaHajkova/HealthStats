namespace HealthStats.logic
{
    public enum Gender
    {
        Male,
        Female
    }

    public enum Activity
    { 
        Sedentary,
        LightlyActive,
        ModeratelyActive,
        VeryActive,
        ExtraActive
    }
    
    public class HealthCalculator
    {
        public void ValidateHeight(double height)
        {
            if (height < 0.5 || height > 3)
            {
                throw new ArgumentOutOfRangeException("Zadejte prosím kladnou platnou výšku v metrech(0-3 metry)");
            }
        }
        public void ValidateWeight(double weight)
        {
            if (weight <= 0)
            {
                throw new ArgumentOutOfRangeException("Váha musí být kladná");
            }
        }
        public void ValidateAge(double age)
        {
            if (age < 1)
            {
                throw new ArgumentOutOfRangeException("Věk musí být nad 1 rok.");
            }
        }
        public double CalculateBmi(double height, double weight)
        {
            ValidateHeight(height);
            ValidateWeight(weight);
            return weight/(height*height);
        }

        public string BmiCategory(double bmi)
        {
            if (bmi < 18.5) return "podváha";
            if (bmi >= 18.5 && bmi <= 24.9) return "zdravá váha";
            if (bmi >= 25 && bmi <= 29.9) return "nadváha";
            if (bmi >= 30 && bmi <= 34.9) return "obezita I. třídy";
            if (bmi < 40) return "Obezita II. třídy";
            return "Obezita III. třídy";
        }

        public double CalculateBmr(double height, double weight, double age, Gender gender)//výpočet podle Harris-Benedict rovnice
        {
            ValidateHeight(height);
            ValidateWeight(weight);
            ValidateAge(age);
            height = height * 100;//do vzorce chceme výšku v cm
            if (gender == Gender.Male)
            {
                return (weight*13.397)+(4.799*height)-(5.677*age)+88.362;
            }
            else
            {
                return (weight*9.247)+(3.098*height)-(4.330*age)+447.593;
            }
        }

        public double CalculateIdealWeight(double height, Gender gender) //B. J. Devine Formula
        {
            ValidateHeight(height);
            double heightover5 = (height * 100) - 152; //pro výpočet potřebujeme výšku nad pět stop
            if (heightover5 < 0)
            {
                heightover5 = 0; //ošetření pro malou výšku
            }
        
        if (gender == Gender.Male)
            {
                return 50 + 0.9*heightover5;
            }
            else
            {
                return 45.5 + 0.9*heightover5;
            }
        }
        public string IdealWeightMessage(double height, Gender gender)
        {
            double weight = CalculateIdealWeight(height, gender);
            string result = $"{weight:F1} kg";

            if (height < 1.524)
            {
                result += " (Pozor: Pro výšku pod 152 cm je výsledek pouze orientační)";
            }

            return result;
        }

        public double CalculateTdee(double bmr, Activity activity)
        {
            double multiplier = 1.2;
            switch (activity)
            {
                case Activity.Sedentary: multiplier = 1.2; break;
                case Activity.LightlyActive: multiplier = 1.375; break;
                case Activity.ModeratelyActive: multiplier = 1.550; break;
                case Activity.VeryActive: multiplier = 1.725; break;
                case Activity.ExtraActive: multiplier = 1.9; break;
            }

            return multiplier * bmr;
        }
    
    
    
    }
}
