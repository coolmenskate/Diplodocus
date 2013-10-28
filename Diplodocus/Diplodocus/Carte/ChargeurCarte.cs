using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FuncWorks.XNA.XTiled;
using Microsoft.Xna.Framework.Content;

namespace Diplodocus.Carte
{
    /******************************************************************************/
    /* Classe         : ChargeurCarte                                             */
    /* Auteur         : Nicolas Richard                                           */
    /* Modification   : 21 octobre 2013                                           */
    /******************************************************************************/
    /// <summary>
    /// Permet de charger les cartes dans l'ordre.
    /// Permet de charger les niveaux à partir d'un niveau en particulier.
    /// Contient les niveaux chargés.
    /// </summary>

    public class ChargeurCarte
    {
        private int niveau = 0;
        private ContentManager ressources;

        /// <summary>
        /// Constructeur.
        /// </summary>
        /// <param name="jeu">Jeu principal</param>
        public ChargeurCarte(Diplodocus jeu)
        {
            ressources = new ContentManager(jeu.Services);
            ressources.RootDirectory = @"Content\Carte";
        }

        /// <summary>
        /// Charge le niveau auquel l'indice entré en paramètre réfère. Ce sera le point de départ pour les prochains chargements.
        /// </summary>
        /// <param name="niveau">Indice du niveau.</param>
        /// <returns>Carte nouvellement chargée.</returns>
        public Map ChargerNiveauPrecis(int niveau)
        {
            this.niveau = niveau;
            ressources.Unload();
            return ressources.Load<Map>(@"Niveau" + niveau.ToString() + @"\Niveau" + niveau.ToString());
        }

        /// <summary>
        /// Charge le niveau suivant logiquement celui présentement chargé.
        /// </summary>
        /// <returns>Carte nouvellement chargée.</returns>
        public Map ChargerNiveauSuivant()
        {
            niveau++;
            ressources.Unload();
            return ressources.Load<Map>(@"Niveau" + niveau.ToString() + @"\Niveau" + niveau.ToString());
        }
    }
}
