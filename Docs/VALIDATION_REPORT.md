# Validation statique du projet

Contrôles effectués avant livraison :

- 26 fichiers XAML/XML analysés : **0 erreur de structure XML**.
- 79 fichiers C# présents.
- Tous les `x:Class` XAML disposent de leur code-behind correspondant.
- Toutes les images référencées dans le XAML existent dans `Resources/Images`.
- Les noms de ressources Android ont été normalisés : lettres minuscules, chiffres et `_`, sans commencer par un chiffre.
- Toutes les pages déclarées dans `AppShell.xaml.cs` existent.
- Vérification simple d'équilibrage des accolades C# : **0 anomalie détectée**.

## Limite de la session

Le conteneur utilisé pour générer le projet ne contient pas le SDK `dotnet` ni le workload .NET MAUI. Une compilation réelle `dotnet build -f net10.0-android` n'a donc pas pu être exécutée ici.

La validation finale doit être faite dans Visual Studio avec le workload **.NET Multi-platform App UI development** et le SDK .NET 10 installés.
