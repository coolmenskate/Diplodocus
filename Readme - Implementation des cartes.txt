
TITRE			: Utilisation des fichiers .tmx avec Tiled
AUTEUR			: Nicolas Richard
DERNI�RE MODIFICATION	: 28 octobre 2013
_________________________________________________________________________________________________________________________________
-	Pr�sentement le jeu ne g�re que les objets de type rectangulaire.  
	Ainsi, il ne pourra g�rer les plateformes triangulaires
	ou munies d'une courbature.

-	Lorsque vous incluez une carte au projet, respectez l'arborescence suivante:
	.../DiplodocusContent/Carte/NiveauX/, o� NiveauX est le dossier ou sera contenue votre carte 
	et X le num�ro du niveau (ex: Niveau1, Niveau3, etc.)
	Dans ce dossier, incluez votre fichier .tmx au m�me nom que ledit dossier (ex: Niveau1.tmx pour une carte se trouvant
	dans le dossier Niveau1/) et incluez l'ensemble des tilesets que la carte utilise.

-	Pour �viter les p�pins avec l'arborescence, travaillez toujours � partir du dossier de la solution.
	Ne cr�ez pas votre carte ailleurs pour ensuite la transf�rer dans le projet.

-	Si des p�pins surviennent, n'h�sitez pas � me (Nicolas) le faire savoir.  Vous pouvez aussi modifier le fichier .tmx dans 
	Visual Studio.


