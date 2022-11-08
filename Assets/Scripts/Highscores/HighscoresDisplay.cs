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
using UnityEngine;
using System.Collections;
using TMPro;
using System;

namespace Assets.Scripts.Highscores
{
    /// <summary>
    /// Clase encargada de mostrar el ranking de usuarios
    /// . Implementa a <see cref="UnityEngine.MonoBehaviour" />
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class HighscoresDisplay : MonoBehaviour
    {
        public TextMeshProUGUI[] highscoreFields;
        Highscores highscoresManager;
        /// <summary>
        /// Puntuación del último jugador del ranking
        /// </summary>
        int lastHighscoreInLeaderboard;

        void Start()
        {
            for (int i = 0; i < highscoreFields.Length; i++)
            {
                highscoreFields[i].text = i + 1 + ". Loading...";
            }


            highscoresManager = GetComponent<Highscores>();
            StartCoroutine("RefreshHighscores");
        }

        /// <summary>
        /// Formatea los datos descargados para mostrarlos
        /// </summary>
        /// <param name="highscoreList"></param>
        public void OnHighscoresDownloaded(Highscore[] highscoreList)
        {
            int listLength = highscoreList.Length;
            if (listLength < 5)
                lastHighscoreInLeaderboard = 0;
            else
                lastHighscoreInLeaderboard = highscoreList[Math.Min(listLength - 1, 4)].score;
            for (int i = 0; i < highscoreFields.Length; i++)
            {
                highscoreFields[i].text = i + 1 + ". ";
                if (i < highscoreList.Length)
                {
                    highscoreFields[i].text += highscoreList[i].username + " - " + highscoreList[i].score;
                }
            }
        }

        /// <summary>
        /// Periódicamente actualiza el ranking
        /// </summary>
        /// <returns></returns>
        IEnumerator RefreshHighscores()
        {
            while (true)
            {
                highscoresManager.DownloadHighscores();
                yield return new WaitForSeconds(30);
            }
        }

        /// <summary>
        /// Obtiene la puntuación más baja del ranking
        /// </summary>
        /// <returns></returns>
        public int GetLastScoreInLeaderboard()
        {
            return lastHighscoreInLeaderboard;
        }
    }
}