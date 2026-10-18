# CleanKwat — .NET MAUI / C#

Application mobile de gestion des abonnements et tournées de collecte des déchets, développée en **.NET 10 MAUI**, **C#**, **XAML**, **MVVM**, **SQLite** et pensée pour Android dans Visual Studio.

## Prérequis

- Ubuntu
- Visual Studio récent avec la charge de travail **.NET Multi-platform App UI development**
- SDK .NET 10
- Android SDK / émulateur ou téléphone Android avec débogage USB

## Ouvrir et lancer

1. Décompresser l'archive.
2. Ouvrir `CleanKwat.sln` dans Visual Studio.
3. Attendre la restauration NuGet.
4. Sélectionner un émulateur Android ou un appareil physique.
5. Démarrer en Debug.

En ligne de commande, si l'environnement MAUI est installé :

```bash
dotnet restore CleanKwat.sln
dotnet build CleanKwat/CleanKwat.csproj -f net10.0-android
```

## Comptes de démonstration

### Ménage
- Identifiant : `+237677777777`
- Mot de passe : `Clean123!`

### Ramasseur
- Identifiant : `collector@cleankwat.local`
- Mot de passe : `Collector123!`

### Administrateur
- Identifiant : `admin@cleankwat.local`
- Mot de passe : `Admin123!`

Ces identifiants sont uniquement destinés à la démonstration universitaire.

## Architecture

```text
CleanKwat/
├── Models/                  Entités SQLite et états métier
├── ViewModels/              Logique MVVM et commandes
├── Views/
│   ├── Auth/                Onboarding, inscription, connexion
│   ├── Household/           Parcours ménage
│   ├── Collector/           Parcours ramasseur
│   └── Admin/               Parcours administrateur
├── Data/                    Base SQLite, création et seed
├── Services/
│   ├── Interfaces/
│   └── Implementations/
├── Controls/                Composants UI réutilisables
├── Constants/               Routes
├── Helpers/                 DI et hashing
├── Resources/
│   ├── Styles/              Design system XAML
│   ├── Images/              SVG + fond d'écran de lancement
│   ├── AppIcon/
│   └── Splash/
└── Platforms/Android/
```

## Design system

Palette principale :

- Vert : `#19803D`
- Vert vif : `#22C55E`
- Accent : `#10B981`
- Fond : `#0F172A`
- Surface : `#1E293B`
- Surface élevée : `#334155`
- Texte : `#FFFFFF`
- Texte secondaire : `#94A3B8`
- Erreur : `#EF4444`
- Avertissement : `#F59E0B`

Rayons : 8 / 12 / 16 / 24 px.

Les styles sont centralisés dans `Resources/Styles/Colors.xaml` et `Resources/Styles/Styles.xaml`.

## Typographie

Les maquettes utilisent Inter. Pour respecter les contraintes de distribution, les fichiers de police ne sont pas inclus dans cette archive. Les styles référencent la famille `Inter`; Android utilisera une police de remplacement si elle n'est pas installée. Pour une fidélité parfaite dans votre propre environnement, ajoutez localement votre copie autorisée d'Inter dans `Resources/Fonts` puis enregistrez-la dans `MauiProgram.cs`.

## SQLite et mode hors ligne

La base locale est créée dans `FileSystem.AppDataDirectory/cleankwat.db3`.

Les données Seed comprennent :
- comptes de démonstration ;
- quartiers de Douala et Yaoundé ;
- abonnement ménage ;
- collectes de mai 2024 et octobre 2026 ;
- paiements ;
- tournées du ramasseur ;
- ménages d'une tournée ;
- notification de démonstration.

Le service `SyncService` simule une synchronisation différée. Les données SQLite restent disponibles sans Internet.

## Écoconception

- illustrations SVG légères ;
- pas d'animation décorative permanente ;
- lecture locale SQLite ;
- pas de rafraîchissement réseau continu ;
- synchronisation explicite ;
- interface sombre et simple ;
- composants réutilisables ;
- photos uniquement pour les signalements / preuves si l'utilisateur les choisit.

## Note de validation

Le projet a été généré dans un environnement où le SDK `dotnet` n'est pas disponible, donc l'archive ne peut pas être compilée dans cette session. Les fichiers XAML/XML sont contrôlés syntaxiquement avant livraison. La validation finale doit être faite dans Visual Studio avec le workload MAUI installé.

## Activité n°7 — Examen / Étape 4

Le dépôt est organisé pour montrer la progression demandée :

- **Phase A** — initialisation du projet, architecture MAUI, design system, modèles, services et données ;
- **Phase B** — intégration du parcours Ménage ;
- **Phase C** — intégration des espaces Ramasseur et Administrateur ;
- **Fonctionnalités ajoutées** — activation des interactions visibles et filtres nécessaires à la démonstration ;
- **Documentation examen** — audit, captures de simulation, PDF et scénario vidéo.

Le front-end .NET MAUI doit rester fidèle aux 13 interfaces précédemment validées. La référence est `Docs/CleanKwat_Maquettes_Reference.pdf`.

### Validation locale

Sous Windows / Visual Studio avec le workload MAUI :

```powershell
.\scripts\validate-exam.ps1
```

Ou :

```bash
dotnet restore CleanKwat.sln
dotnet build CleanKwat/CleanKwat.csproj -f net10.0-android
```

Les captures d'écran de l'examen doivent être réalisées depuis l'application en exécution, et non depuis la maquette.
