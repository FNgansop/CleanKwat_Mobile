# CleanKwat — Activité n°7 / Examen / Étape 4

## 1. Objet de l'audit

Cet audit prépare le projet CleanKwat pour l'évaluation de progression : validation de l'intégration des interfaces, historique Git par phases, publication GitHub, captures en mode simulation, PDF et vidéo de démonstration.

Le front-end .NET MAUI doit rester fidèle aux maquettes validées précédemment dans `Docs/CleanKwat_Maquettes_Reference.pdf`.

## 2. Contrôles statiques réalisés

- 26 fichiers XAML/XML analysés sans erreur de syntaxe XML.
- Toutes les références `StaticResource` utilisées dans les XAML correspondent à une clé déclarée.
- Tous les `x:Class` XAML disposent d'un code-behind correspondant.
- Aucun `TODO`, `FIXME` ou `NotImplementedException` n'est présent dans le projet.
- Tous les boutons visibles des écrans métier disposent désormais d'une action (`Command` ou `Clicked`).
- Les routes Shell principales sont enregistrées dans `AppShell.xaml.cs`.
- Les ViewModels sont enregistrés dans l'injection de dépendances de `MauiProgram.cs`.

## 3. Vérification des 13 interfaces de référence

| # | Interface | Fichier .NET MAUI | État statique |
|---|---|---|---|
| 1 | Onboarding / présentation | `Views/Auth/OnboardingPage.xaml` | Intégrée |
| 2 | Inscription | `Views/Auth/RegisterPage.xaml` | Intégrée |
| 3 | Connexion | `Views/Auth/LoginPage.xaml` | Intégrée |
| 4 | Choix du quartier | `Views/Household/NeighborhoodSelectionPage.xaml` | Intégrée |
| 5 | Configuration du ménage | `Views/Household/HouseholdSetupPage.xaml` | Intégrée |
| 6 | Accueil ménage | `Views/Household/HouseholdHomePage.xaml` | Intégrée |
| 7 | Calendrier des collectes | `Views/Household/CalendarPage.xaml` | Intégrée |
| 8 | Détail d'une collecte | `Views/Household/CollectionDetailPage.xaml` | Intégrée |
| 9 | Connexion ramasseur | `Views/Collector/CollectorLoginPage.xaml` | Intégrée |
| 10 | Tableau de bord ramasseur | `Views/Collector/CollectorDashboardPage.xaml` | Intégrée |
| 11 | Mes tournées | `Views/Collector/CollectorToursPage.xaml` | Intégrée |
| 12 | Gestion des quartiers Admin | `Views/Admin/AdminNeighborhoodsPage.xaml` | Intégrée |
| 13 | Suivi des collectes Admin | `Views/Admin/AdminCollectionsPage.xaml` | Intégrée |

## 4. Interactions complétées pour l'examen

Les boutons visibles sans action ont été reliés afin que la démonstration ne présente pas d'éléments inertes :

- filtres Tous / Actifs / Inactifs dans la gestion des quartiers Admin ;
- bouton Modifier du quartier dans la configuration ménage ;
- filtres Aujourd'hui / Demain / Cette semaine dans les tournées Ramasseur ;
- bouton Contacter l'administrateur sur la connexion Ramasseur ;
- boutons de connexion externe de la maquette reliés à un message de démonstration.

## 5. Limite de validation dans cette session

Le SDK .NET / workload MAUI n'est pas installé dans l'environnement de préparation utilisé ici. La compilation Android et l'exécution dans l'émulateur doivent donc être validées sur la machine Visual Studio de l'étudiant.

Commandes obligatoires :

```bash
dotnet restore CleanKwat.sln
dotnet build CleanKwat/CleanKwat.csproj -f net10.0-android
```

Puis exécuter l'application dans un émulateur Android ou un téléphone physique.

## 6. Validation visuelle obligatoire

Pour chaque écran de référence, comparer l'application exécutée avec la maquette PDF :

- structure ;
- textes ;
- couleurs ;
- proportions ;
- boutons ;
- cartes ;
- marges ;
- illustrations ;
- navigation.

Un écran n'est validé que si la compilation, le comportement et la fidélité visuelle sont satisfaisants.
