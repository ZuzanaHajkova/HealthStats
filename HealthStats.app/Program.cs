using System;
using HealthStats.logic;
using System.Globalization;

namespace HealthStats.app
{
    class Program
    {
        static HealthCalculator healthCalculator = new HealthCalculator();//instance kalkulačky, pro jedondušší používání v programu
        
        static void Main(string[] args)
        {
            Console.WriteLine("HEALTHSTATS KALKULAČKA");
            try
            {
                //bereme vahu, vysku, vek
                double weight = GetDouble("Zadejte vaši váhu v kg: ");
                double height = GetDouble("Zadejte vaši výšku v metrech: ");
                double age = GetDouble("Zadejte váš věk: ");

                Gender gender = GetGender();
                Activity activity = GetActivity();

                //aktivita a gender

                //vypocet
                double bmi = healthCalculator.CalculateBmi(height, weight);
                string bmiCat = healthCalculator.BmiCategory(bmi);
                double bmr = healthCalculator.CalculateBmr(height, weight, age, gender);
                double idealWeight = healthCalculator.CalculateIdealWeight(height, gender);
                double tdee = healthCalculator.CalculateTdee(bmr, activity);

                Results(bmi, bmiCat, bmr, idealWeight, tdee);
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nCHYBA VE VÝPOČTU!");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            catch (Exception)
            {
                Console.WriteLine("Nastala neočekávaná chyba.");
            }
          
            Console.WriteLine();
            Console.WriteLine("Stiskněte libovolnou klávesu pro ukončení...");
            Console.ReadKey();
        }

        static void Results(double bmi, string bmiCat, double bmr, double idealWeight, double tdee)
        {
            //vypis vysledku
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== VÝSLEDKY ===");
            Console.ResetColor();

            Console.WriteLine($"BMI:            {bmi:F2} ({bmiCat})");
            Console.WriteLine($"BMR (klid):     {bmr:F0} kcal/den");
            Console.WriteLine($"TDEE (aktivní): {tdee:F0} kcal/den");
            Console.WriteLine($"Ideální váha:   {idealWeight}");

        }
       
        static double GetDouble(string text)
        {
            double result;
            while (true)
            {
                Console.Write(text);
                string input = Console.ReadLine();
                if (double.TryParse(input,NumberStyles.Any, CultureInfo.CurrentCulture, out result))
                {
                    return result;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Chyba! Zadejte prosím platné číslo.(pozor na desetinnou tečku/čárku)");
                Console.ResetColor();
            }
        }

        static Gender GetGender()
        {
            Console.WriteLine("Vyberte pohlaví:");
            Console.WriteLine("1 - muž");
            Console.WriteLine("2 - žena");

            while (true)
            {
                Console.Write("vaše volba: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1": return Gender.Male;
                    case "2": return Gender.Female;
                    default: 
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Neplatná volba, zkuste to znovu."); 
                        Console.ResetColor();
                        break;
                }
            }
        }

        static Activity GetActivity()
        {
            Console.WriteLine("Vyberte míru vaší aktivity:");
            Console.WriteLine("1 - Sedavé (žádný sport)");
            Console.WriteLine("2 - Lehce aktivní (1-3x týdně)");
            Console.WriteLine("3 - Středně aktivní (3-5x týdně)");
            Console.WriteLine("4 - Velmi aktivní (6-7x týdně)");
            Console.WriteLine("5 - Extrémní (fyzická práce)");
            while (true)
            {
                Console.Write("Vyberte jednu možnost (1-5): ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": return Activity.Sedentary;
                    case "2": return Activity.LightlyActive;
                    case "3": return Activity.ModeratelyActive;
                    case "4": return Activity.VeryActive;
                    case "5": return Activity.ExtraActive;
                    default: 
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Neplatná volba, zkuste to znovu."); 
                        Console.ResetColor();
                        break;
                }
            }
        }
    }
}
