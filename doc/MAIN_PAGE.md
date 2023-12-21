 # GoodDoc - T3 Projet

 Ceci est un projet réaliser par les étudiants de deuxiéme année de BUT Informatique Robert Schuman, dans le câdre du projet T3.

 Pour plus d'information aller au lien suivant : [lien vers le README](https://git.unistra.fr/T3GoodDoc/t3GoodDoc/-/blob/main/README.md?ref_type=heads)

 ## Table des matières

 * [Prérequis](#prérequis)
    * [Complement Linux](#linux)
    * [Complement Windows et MacOS](#windows-et-macos)
 * [Récuperation des fichiers](#récupération-des-fichiers)
 * [Ouverture des fichiers](#ouverture-des-fichiers)
    * [Liste d'IDE C#](#liste-dide-net-c)
 * [Explication de la structure des fichiers](#explication-de-la-structure-des-fichiers)

 ## Prérequis

 Vous devez impérative installer .Net (dotnet) : [.Net Installer](https://dotnet.microsoft.com/en-us/download) 
 
 Mais aussi Godot 4 et supérieur en version .Net : [Godot 4](https://godotengine.org/download/)

 Mais aussi de 'git'.

 ### Linux
  Vous devez aussi installer la librairie sqlite ou sqlite3.

  ```sudo apt install libsqlite3-dev```

 ### Windows et MacOS
  Vous n'avez besoin de rien d'autre.

 ## Récupération des fichiers

 Il ne vous suffi seulement de rentrer la commande suivante.

 ```git clone https://git.unistra.fr/T3GoodDoc/t3GoodDoc.git```

 ## Ouverture des fichiers
 
 * Ouvrir le fichier 'project.godot' avec Godot
 * Ouvrir le fichier 'T 3 Projet.csproj' avec votre IDE C# préféré.
 
 ### Liste d'IDE .Net (C#, ...)
 * Rider : [rider](https://www.jetbrains.com/fr-fr/rider/)
 * Visual Studio : [visual studio](https://visualstudio.microsoft.com/fr/)

 ## Explication de la structure des fichiers

Le dossier Scripts contient l'ensemble des fichiers de code sauf 'GamePlay.cs'

Le fichier 'GamePlay.cs' contient la logique de jeu.

Dans le dossier Scripts/Database, une version de la base de données est contenue.

Le dossier GameCinematics contient l'ensemble des fichiers pour la cinématique du jeu.

Le dossier PicturesPatients contient les images des patients du jeu.

Le dossier Resources contient les autres fichiers de ressource nécessaires au jeu.

Le dossier Config contient un fichier 'gameconfig.cfg', le fichier de config pour le jeu.
Mais aussi le fichier 'exemple.cfg', un fichier d'exemple contenant l'ensemble des paramètres possibles.

Le dossier doc contient la documentation du code format Web.

Le dossier diagramme contient le diagramme UML et le MCD de la base de données.


