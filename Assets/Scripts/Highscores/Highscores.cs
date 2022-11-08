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

using Assets.Scripts.Game;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Highscores
{
    /// <summary>
    /// Clase que se encarga de obtener el ranking de usuarios o añadir nuevos registros
    /// . Implementa a <see cref="UnityEngine.MonoBehaviour" />
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class Highscores : MonoBehaviour
    {
        HighscoresDisplay highscoreDisplay;
        public Highscore[] highscoresList;
        static Highscores instance;

        void Awake()
        {
            highscoreDisplay = GetComponent<HighscoresDisplay>();
            instance = this;
        }
    
        public static void AddNewHighscore(string username, int score)
        {
            instance.StartCoroutine(instance.UploadNewHighscore(username, score));
        }

        /// <summary>
        /// Subir una nueva puntuación
        /// </summary>
        /// <param name="username"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        IEnumerator UploadNewHighscore(string username, int score)
        {
            WWW www = new WWW(Constants.webURL + Constants.privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                print("Upload Successful");
                DownloadHighscores();
            }
            else
            {
                print("Error uploading: " + www.error);
            }
        }

        public void DownloadHighscores()
        {
            StartCoroutine("DownloadHighscoresFromDatabase");
        }

        /// <summary>
        /// Descargar la lista de puntuaciones
        /// </summary>
        /// <returns></returns>
        IEnumerator DownloadHighscoresFromDatabase()
        {
            WWW www = new WWW(Constants.webURL + Constants.publicCode + "/pipe/");
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                FormatHighscores(www.text);
                highscoreDisplay.OnHighscoresDownloaded(highscoresList);
            }
            else
            {
                print("Error Downloading: " + www.error);
            }
        }

        /// <summary>
        /// Formatea los datos obtenidos
        /// </summary>
        /// <param name="textStream"></param>
        void FormatHighscores(string textStream)
        {
            string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            highscoresList = new Highscore[entries.Length];

            for (int i = 0; i < entries.Length; i++)
            {
                string[] entryInfo = entries[i].Split(new char[] { '|' });
                string username = entryInfo[0];
                int score = int.Parse(entryInfo[1]);
                highscoresList[i] = new Highscore(username, score);
            }
        }

    }

    public struct Highscore
    {
        public string username;
        public int score;

        public Highscore(string _username, int _score)
        {
            username = _username;
            score = _score;
        }
    }
}
