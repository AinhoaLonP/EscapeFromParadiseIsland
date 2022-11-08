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

using Assets.Scripts.Internationalization;
using Lean.Localization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    /// <summary>
    /// Menú principal del juego
    /// . Implementa a <see cref="UnityEngine.MonoBehaviour" />
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class MainMenu : MonoBehaviour
    {
        /// <summary>
        /// Carga la escena de juego
        /// </summary>
        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            string currentLanguage = LeanLocalization.CurrentLanguage;
            LanguageController languageController = new LanguageController();
            if (currentLanguage.Equals("Spanish"))
                languageController.ChangeToSpanish();
        }

        /// <summary>
        /// Cierra la aplicación
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
