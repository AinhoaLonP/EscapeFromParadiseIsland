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
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    /// <summary>
    /// Menú de pausa
    /// . Implementa a <see cref="UnityEngine.MonoBehaviour" />
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class PauseMenu : MonoBehaviour
    {
        /// <summary>
        /// Nombre del menú principal
        /// </summary>
        private readonly string menuSceneName = "Menu";
        /// <summary>
        /// Determina si el juego está en pausa
        /// </summary>
        public static bool gameIsPaused = false;
        /// <summary>
        /// Interfaz del menú pausa
        /// </summary>
        public GameObject pauseMenuUI;

        /// <summary>
        /// Detecta la pulsación de la tecla Escape para activar o desactivar el menú pausa
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gameIsPaused)
                    Resume();
                else
                    Pause();
            }
        }

        /// <summary>
        /// Desactiva el menú pausa
        /// </summary>
        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            gameIsPaused = false;
        }

        /// <summary>
        /// Activa el menú pausa
        /// </summary>
        void Pause()
        {
            pauseMenuUI.SetActive(true);
            gameIsPaused = true;
        }

        /// <summary>
        /// Carga el menú principal
        /// </summary>
        public void LoadMainMenu()
        {
            SceneManager.LoadScene(menuSceneName);
        }

        /// <summary>
        /// Sale del juego
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}