using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FuncWorks.XNA.XTiled;
using Microsoft.Xna.Framework;

namespace Diplodocus.Carte
{
    /******************************************************************************/
    /* Classe         : CollisionCarte                                            */
    /* Auteur         : Nicolas Richard                                           */
    /* Modification   : 27 octobre 2013                                           */
    /******************************************************************************/
    /// <summary>
    /// Gère les collsions entres les objets et la carte spécifiée et permet d'obtenir des informations quant aux collisions.
    /// </summary>
    static public class CollisionCarte
    {
        /// <summary>
        /// Vérifie si la position désirée n'est pas source de collision et aujuste celle-ci s'il y a lieu.
        /// *Ne gère les collisions qu'avec les objets de forme rectangulaire*
        /// </summary>
        /// <param name="carte">Carte sur laquelle l'objet évolue</param>
        /// <param name="calquePlateforme">Le calque où sont situés les objets où l'on veut vérifer la collision.</param>
        /// <param name="positionActuelle">Position résultante du mouvement</param>
        /// <param name="positionPrecedente">Position précédente au mouvement</param>
        /// <returns>Position corrigée qui n'est plus en collision de l'objet</returns>
        static public Rectangle PositionApresCollisions(Map carte, int calquePlateforme, Rectangle positionActuelle, Rectangle positionPrecedente)
        {
            //On modifie la position de l'objet en considérant chaque plateforme à laquelle il touche
            foreach (var objet in carte.GetObjectsInRegion(calquePlateforme, positionActuelle))
            {
                //Gestion des plateformes de type "Rectangle" seulement
                if ((objet as MapObject).Polygon == null)
                {
                    Rectangle plateforme = (objet as MapObject).Bounds;
                    Vector2 deplacement = new Vector2(positionActuelle.X - positionPrecedente.X, positionActuelle.Y - positionPrecedente.Y);
                    Rectangle intersection = Rectangle.Intersect(plateforme, positionActuelle);
                    Rectangle deplacementPartiel = positionPrecedente;

                    //On effectue le déplacement supposé seulement dans un axe et on modifie la position dans cet axe seulement s'il y a contact
                    //Axe X
                    deplacementPartiel.Offset((int)deplacement.X, 0);
                    if (Rectangle.Intersect(deplacementPartiel, plateforme).IsEmpty == false)
                        positionActuelle.Offset(Math.Sign(-deplacement.X) * intersection.Width, 0);

                    //Axe Y
                    deplacementPartiel = positionPrecedente;
                    deplacementPartiel.Offset(0, (int)deplacement.Y);
                    if (Rectangle.Intersect(deplacementPartiel, plateforme).IsEmpty == false)
                        positionActuelle.Offset(0, Math.Sign(-deplacement.Y) * intersection.Height);
                }
            }
            return positionActuelle;
        }

        /// <summary>
        /// Vérifie si la position désirée n'est pas source de collision et aujuste celle-ci s'il y a lieu.
        /// *Ne gère les collisions qu'avec les objets de forme rectangulaire*
        /// </summary>
        /// <param name="carte">Carte sur laquelle l'objet évolue</param>
        /// <param name="calquePlateforme">Indice du calque où sont situés les objets où l'on veut vérifer la collision.</param>
        /// <param name="positionActuelle">Position résultante du mouvement</param>
        /// <param name="deplacement">Déplacement effectué par l'objet</param>
        /// <returns>Position corrigée qui n'est plus en collision de l'objet</returns>
        static public Rectangle PositionApresCollisions(Map carte, int calquePlateforme, Rectangle positionActuelle, Vector2 deplacement)
        {
            //On modifie la position de l'objet en considérant chaque plateforme à laquelle il touche
            foreach (var objet in carte.GetObjectsInRegion(calquePlateforme, positionActuelle))
            {
                //Gestion des plateformes de type "Rectangle" seulement
                if ((objet as MapObject).Polygon == null)
                {
                    Rectangle plateforme = (objet as MapObject).Bounds;
                    Rectangle positionPrecedente = positionActuelle;
                    positionPrecedente.Offset((int)-deplacement.X, (int)-deplacement.Y);
                    Rectangle intersection = Rectangle.Intersect(plateforme, positionActuelle);
                    Rectangle deplacementPartiel = positionPrecedente;

                    //On effectue le déplacement supposé seulement dans un axe et on modifie la position dans cet axe seulement s'il y a contact
                    //Axe X
                    deplacementPartiel.Offset((int)deplacement.X, 0);
                    if (Rectangle.Intersect(deplacementPartiel, plateforme).IsEmpty == false)
                        positionActuelle.Offset(Math.Sign(-deplacement.X) * intersection.Width, 0);
                    deplacementPartiel = positionPrecedente;

                    //Axe Y
                    deplacementPartiel.Offset(0, (int)deplacement.Y);
                    if (Rectangle.Intersect(deplacementPartiel, plateforme).IsEmpty == false)
                        positionActuelle.Offset(0, Math.Sign(-deplacement.Y) * intersection.Height);
                }
            }
            return positionActuelle;
        }

        /// <summary>
        /// Indique si l'objet spécifié touche au sol (S'il est en contact avec le dessus d'un rectangle de collision).
        /// </summary>
        /// <param name="carte">Carte où sont situés les objets où l'on veut vérifer la collision.</param>
        /// <param name="calquePlateforme">Indice du calque où évolue</param>
        /// <param name="position">Rectangle de collision de l'objet</param>
        /// <returns>True:  L'objet touche au sol, False:   L'objet ne touche pas au sol.</returns>
        static public bool contactAvecLeSol(Map carte, int calquePlateforme, Rectangle position)
        {
            //On augmente la hauteur du rectangle de détection de 1 pixel vers le bas pour vérifier s'ily a des objets dessous. 
            Rectangle detection = new Rectangle(position.X, position.Y, position.Width, position.Height + 1);
            List<MapObject> objetsDessous = carte.GetObjectsInRegion(calquePlateforme, detection).ToList<MapObject>();
            if (objetsDessous.Count == 0)
            {
                Console.WriteLine(@"Vol");
                return false;
            }
            else
            {
                Console.WriteLine(@"plancher");
                return true;
            }
        }

        /// <summary>
        /// Indique si l'objet spécifié est en contact avec un objet au-dessus de lui
        /// </summary>
        /// <param name="carte">Carte dans laquelle l'objet évolue</param>
        /// <param name="calquePlateforme">Indice du calque contenant les plateformes avec lesquels l'objet pourrait entrer en collision.</param>
        /// <param name="position">Rectangle de collision de l'objet.</param>
        /// <returns>True si l'objet entre en collision avec une plateforme au-dessus de lui.</returns>
        static public bool contactAuDessus(Map carte, int calquePlateforme, Rectangle position)
        {
            //On augmente la hauteur du rectangle de détection de collision d'un pixel vers le haut pour vérifier les collisions au-dessus.
            Rectangle detection = new Rectangle(position.X, position.Y-1, position.Width, position.Height + 1);
            List<MapObject> objetsDessus = carte.GetObjectsInRegion(calquePlateforme, detection).ToList<MapObject>();
            if (objetsDessus.Count == 0)
                return false;
            else
                return true;
        }
    }
}
