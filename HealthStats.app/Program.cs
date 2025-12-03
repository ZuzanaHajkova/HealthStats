using System;
using HealthStats.logic;

namespace HealthStats.app
{
    class Program
    {
        static HealthCalculator healthCalculator = new HealthCalculator();//instance kalkulačky, pro jedondušší používání v programu
        
        static void Main(string[] args)
        {
            Console.WriteLine("HEALTHSTATS KALKULAČKA");
            
            //bereme vahu, vysku, vek
            double weight = GetDouble("Zadejte vaši váhu v kg: ");
            double height = GetHeight("Zadejte vaši výšku v metrech: ");
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
            
            //vypis vysledku
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== VÝSLEDKY ===");
            Console.ResetColor();

            Console.WriteLine($"BMI:            {bmi:F2} ({bmiCat})");
            Console.WriteLine($"BMR (klid):     {bmr:F0} kcal/den");
            Console.WriteLine($"TDEE (aktivní): {tdee:F0} kcal/den");
            Console.WriteLine($"Ideální váha:   {idealWeight}");

            Console.WriteLine();
            Console.WriteLine("Stiskni libovolnou klávesu pro ukončení...");
            Console.ReadKey();
        }

        static double GetDouble(string text)
        {
            double result;
            while (true)
            {
                Console.Write(text);
                string input = Console.ReadLine();
                if (double.TryParse(input, out result) && result > 0)
                {
                    return result;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Chyba! Zadej prosím platné kladné číslo.");
                Console.ResetColor();
            }
        }

        static double GetHeight(string text)
        {
            double result;
            while (true)
            {
                Console.Write(text);
                string input = Console.ReadLine();
                
                if (double.TryParse(input, out result) && result > 0)
                {
                    if (result > 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Prosím zadejte platnou výšku v metrech.");
                        Console.ResetColor();
                        continue;//vrátíme se na začátek smyčky
                    }
                    return result;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Chyba! Zadej prosím kladné číslo.");
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
                if (input == "1")
                {
                    return Gender.Male;
                }

                if (input == "2")
                {
                    return Gender.Female;
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
                    default: Console.WriteLine("Neplatná volba, zkuste to znovu.");
                        break;
                }
            }
        }
    }
}
