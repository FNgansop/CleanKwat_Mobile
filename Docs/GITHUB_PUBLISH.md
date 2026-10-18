# Publication GitHub — CleanKwat

## Prérequis

Créer un dépôt GitHub **public** nommé par exemple `CleanKwat-Mobile`, sans ajouter de README/.gitignore depuis GitHub si le dépôt local existe déjà.

## Publication depuis le terminal Visual Studio / PowerShell

Depuis la racine du projet :

```bash
git status
git log --oneline --graph --all
git remote add origin https://github.com/VOTRE_COMPTE/CleanKwat-Mobile.git
git push -u origin main
```

Si `origin` existe déjà :

```bash
git remote -v
git remote set-url origin https://github.com/VOTRE_COMPTE/CleanKwat-Mobile.git
git push -u origin main
```

## Vérifications sur GitHub

Le dépôt public doit afficher un historique similaire à :

```text
Docs - Preparation des livrables de examen Etape 4
Feature - Activation des interactions visibles et filtres de demonstration
Phase C - Integration des espaces Ramasseur et Administrateur
Phase B - Integration du parcours Menage
Phase A - Initialisation du projet CleanKwat et architecture MAUI
```

Tester ensuite l'URL du dépôt dans une fenêtre privée.

## Important

Les commits créés pour l'examen utilisent la date réelle de préparation. Ne pas antidater les commits.
