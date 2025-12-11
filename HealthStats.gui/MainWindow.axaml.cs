using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media; 
using HealthStats.logic; 
using System;
using System.Globalization;
using System.Threading.Tasks;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia.Dto;
using Avalonia.Controls;
using Avalonia.Styling;

namespace HealthStats.gui;

public partial class MainWindow : Window
{
    private readonly HealthCalculator _calculator = new HealthCalculator();//readonly jako pojistka proti náhodnému přepsání
    public MainWindow()// zavolané při každém novém spuštění aplikace 
    {
        InitializeComponent(); //načte axaml (gui)
        HintsComboBoxes();//pomocná metoda pro nápovědy v combo boxech
        
    }

    private void HintsComboBoxes()
    {
        ComboGender.ItemsSource = Enum.GetValues(typeof(Gender));
        ComboGender.SelectedIndex = 0;//jako příklad zvolí první možnost
        
        ComboActivity.ItemsSource = Enum.GetValues(typeof(Activity));
        ComboActivity.SelectedIndex = 0;
    }

    public async void CalculateBtn_Click(object sender, RoutedEventArgs args)
    {
        if (!double.TryParse(InputWeight.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double weight))
        {
            ShowErrorDialog("Chybný vstup","Zadejte prosím platnou váhu. ");
            return;
        }

        if (!double.TryParse(InputHeight.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double height))
        {
            ShowErrorDialog("Chybný vstup","Zadejte prosím platnou výšku. ");
            return;
        }

        if (!double.TryParse(InputAge.Text, NumberStyles.Any,CultureInfo.CurrentCulture, out double age))
        {
            ShowErrorDialog("Chybný vstup","Zadejte prosím platný věk. ");
            return;
        }
        var gender = (Gender)ComboGender.SelectedItem!;
        var activity = (Activity)ComboActivity.SelectedItem!;
        
        try
        {
            //výpočet
            double bmi = _calculator.CalculateBmi(height, weight);
            string bmiCat = _calculator.BmiCategory(bmi);
            double bmr = _calculator.CalculateBmr(height, weight, age, gender);
            string idealWeight = _calculator.IdealWeightMessage(height, gender);
            double tdee = _calculator.CalculateTdee(bmr, activity);

           Results(bmi, bmiCat, bmr, idealWeight, tdee);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            await ShowErrorDialog("Chyba výpočtu", ex.Message);
        }
        catch (Exception)
        {
            await ShowErrorDialog("Chyba","Nastala neočekávaná chyba.");
        }
    }

    private async Task ShowErrorDialog(string title, string message)//metoda na zobrazení vyskakovacího okna
    {
        var box = MessageBoxManager.GetMessageBoxStandard(new MessageBoxStandardParams
        {
            ContentTitle = title,
            ContentMessage = message,
            ButtonDefinitions = ButtonEnum.Ok,
            Icon = MsBox.Avalonia.Enums.Icon.Error,
            
            MaxWidth = 500,
            SizeToContent = SizeToContent.Height,
            ShowInCenter = true,
        });
        await box.ShowWindowDialogAsync(this);//dokud uzivatel neodklikne ok, nemuze dale pracovat s aplikaci
    }
    private void Results(double bmi, string bmiCat, double bmr, string idealWeight, double tdee)
    {
        //zobrazení výsledků
        TextBmiValue.Text = bmi.ToString("F1");//zaokrouhlujeme na 1 místo
        TextBmiCategory.Text = bmiCat;
        TextBmr.Text = $"{bmr:F0} kcal";
        TextIdealWeight.Text = idealWeight;
        TextTdee.Text = $"{tdee:F0} kcal";
        
        //barva indikatoru
        if (bmi < 18.5)
        {
            BmiIndicatorBorder.Background = new SolidColorBrush(Color.Parse("#3498DB"));
        }
        else if (bmi < 25)
        {
            BmiIndicatorBorder.Background = new SolidColorBrush(Color.Parse("#2ECC71"));
        }
        else if (bmi < 30)
        {
            BmiIndicatorBorder.Background = new SolidColorBrush(Color.Parse("#F39C12"));
        }
        else
        {
            BmiIndicatorBorder.Background = new SolidColorBrush(Color.Parse("#E74C3C"));
        }
    }
}