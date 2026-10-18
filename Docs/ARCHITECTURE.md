# Architecture CleanKwat

## Flux principal

```text
Onboarding
  -> Connexion
  -> Inscription Ménage -> Quartier -> Configuration ménage -> Accueil ménage

Connexion Ménage -> Accueil ménage
Connexion Ramasseur -> Dashboard ramasseur -> Tournées -> Détail -> Confirmation
Connexion Admin -> Dashboard admin -> Quartiers / Collectes / Ramasseurs
```

## Parcours Ménage

```text
Accueil
├── Mon abonnement
├── Calendrier -> Détail collecte
├── Paiements -> Paiement simulé
├── Signaler -> Historique signalements
└── Notifications
```

## Parcours Ramasseur

```text
Connexion Ramasseur
-> Dashboard
-> Mes tournées
-> Détail tournée
-> Confirmer une collecte
```

## Parcours Admin

```text
Dashboard
├── Gestion des quartiers
├── Suivi des collectes
└── Gestion des ramasseurs
```

## Séparation des responsabilités

- **Views** : rendu XAML et cycle de vie.
- **ViewModels** : état, commandes, validation.
- **Services** : authentification, navigation, dialogues, connectivité et synchronisation.
- **Data/AppDatabase** : SQLite et seed.
- **Models** : entités persistées.
