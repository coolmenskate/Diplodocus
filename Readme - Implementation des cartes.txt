
TITRE			: Utilisation des fichiers .tmx avec Tiled
AUTEUR			: Nicolas Richard
DERNIÈRE MODIFICATION	: 28 octobre 2013
_________________________________________________________________________________________________________________________________
-	Présentement le jeu ne gère que les objets de type rectangulaire.  
	Ainsi, il ne pourra gérer les plateformes triangulaires
	ou munies d'une courbature.

-	Lorsque vous incluez une carte au projet, respectez l'arborescence suivante:
	.../DiplodocusContent/Carte/NiveauX/, où NiveauX est le dossier ou sera contenue votre carte 
	et X le numéro du niveau (ex: Niveau1, Niveau3, etc.)
	Dans ce dossier, incluez votre fichier .tmx au même nom que ledit dossier (ex: Niveau1.tmx pour une carte se trouvant
	dans le dossier Niveau1/) et incluez l'ensemble des tilesets que la carte utilise.

-	Pour éviter les pépins avec l'arborescence, travaillez toujours à partir du dossier de la solution.
	Ne créez pas votre carte ailleurs pour ensuite la transférer dans le projet.

-	Si des pépins surviennent, n'hésitez pas à me (Nicolas) le faire savoir.  Vous pouvez aussi modifier le fichier .tmx dans 
	Visual Studio.


