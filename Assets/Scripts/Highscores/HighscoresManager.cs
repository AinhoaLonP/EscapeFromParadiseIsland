// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Ainhoa Longo Pérez
// Last Modified On : 06-19-2020
// ***********************************************************************
// <copyright file="BaseDoubleAction.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Highscores
{
    /// <summary>
    /// Clase encargada de añadir puntuaciones al ranking y obtener las ya descargadas
    /// . Implementa a <see cref="UnityEngine.MonoBehaviour" />
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class HighscoresManager : MonoBehaviour
    {
        public TextMeshProUGUI playerName;
        public TextMeshProUGUI score;
        public GameObject insideRankingPanel;
        public GameObject outsideRankingPanel;
        public GameObject insideRankingScoreObject;
        public GameObject outsideRankingScoreObject;
        Highscores highscoresManager;
        HighscoresDisplay highscoresDisplay;

        void Start()
        {
            highscoresManager = GetComponent<Highscores>();
            highscoresDisplay = GetComponent<HighscoresDisplay>();
        }

        /// <summary>
        /// Añade una nueva puntuación al ranking
        /// </summary>
        public void AddHighscore()
        {
            Highscores.AddNewHighscore(playerName.text, int.Parse(score.text));
        }

        /// <summary>
        /// Obtiene las puntuaciones del ranking
        /// </summary>
        /// <returns></returns>
        IEnumerator GetHighscores()
        {
            highscoresManager.DownloadHighscores();
            yield return new WaitForSeconds(1);
        }

        /// <summary>
        /// Comprueba si la puntuación obtenida es lo suficientemente alta como para formar partee del ranking
        /// </summary>
        /// <returns></returns>
        bool CheckInsideRanking()
        {
            int lastScore = highscoresDisplay.GetLastScoreInLeaderboard();
            if (int.Parse(score.text) > lastScore)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Muestra un mensaje u otro dependiendo de si ha entrado en el ranking o no
        /// </summary>
        public void ChangePanel()
        {
            if (CheckInsideRanking())
            {
                TextMeshProUGUI mText = insideRankingScoreObject.transform.GetComponent<TextMeshProUGUI>();
                mText.text = score.text;
                insideRankingPanel.SetActive(true);
            }
            else
            {
                TextMeshProUGUI mText = outsideRankingScoreObject.transform.GetComponent<TextMeshProUGUI>();
                mText.text = score.text;
                outsideRankingPanel.SetActive(true);
            }
        }
    }
}
