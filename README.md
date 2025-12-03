## Řešení 
rozděleno do tří samostatných projektů

1.  **`HealthStats.logic`** (Class Library)
    * Obsahuje veškerou matematiku a algoritmy.
    * Nezávislá na platformě (použitelná kdekoliv).
    * Obsahuje výpočty pro BMI, BMR, TDEE a ideální váhu.

2.  **`HealthStats.app`** (Konzolová aplikace)
    * Jednoduché textové rozhraní pro rychlé výpočty.
    * Implementuje robustní validaci vstupů (ošetření chyb uživatele).

3.  **`HealthStats.gui`** (GUI Aplikace)
    * Moderní grafické rozhraní postavené na Avalonia UI.
    * Vizuální indikátor BMI (měnící se barva podle kategorie).
    * Responzivní design.
      
## Funkce
Aplikace počítá následující údaje na základě věku, váhy, výšky, pohlaví a aktivity:

* **BMI (Body Mass Index):** Včetně slovního hodnocení (např. Nadváha) a barevné indikace.
* **BMR (Bazální metabolismus):** Výpočet pomocí Harris-Benedictovy rovnice. Udává, kolik kalorií tělo spálí v klidovém režimu.
* **TDEE (Celkový denní energetický výdej):** Kalorická potřeba upravená o faktor fyzické aktivity.
* **Ideální váha:** Doporučené váhové rozmezí pro udržení zdravého BMI.

## Použité technologie

* **Jazyk:** C#
* **Framework:** .NET 8
* **GUI Framework:** Avalonia UI (XAML)
* **IDE:** JetBrains Rider

### Požadavky
* Nainstalované **.NET SDK** (verze 8.0 nebo novější).
