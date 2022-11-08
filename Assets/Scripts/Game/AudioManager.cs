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
using UnityEngine.UI;

namespace Assets.Scripts.Game
{
    /// <summary>
    /// Controla la música de fondo del juego
    /// </summary>
    class AudioManager : MonoBehaviour
    {
        /// <summary>
        /// Botón de apagado y encendido de la música
        /// </summary>
        public GameObject soundControlButton;
        /// <summary>
        /// Imagen de música apagada
        /// </summary>
        public Sprite audioOffSprite;
        /// <summary>
        /// Imagen de música encendida
        /// </summary>
        public Sprite audioOnSprite;

        /// <summary>
        /// Asigna al botón el icono adecuado, según si la música se está reproduciendo o está pausada
        /// </summary>
        void Start()
        {
            if (AudioListener.pause)
            {
                soundControlButton.GetComponent<Image>().sprite = audioOffSprite;
            }
            else
            {
                soundControlButton.GetComponent<Image>().sprite = audioOnSprite;
            }
        }

        /// <summary>
        /// Método encargado de pausar y reanudar la reproducción de la música de fondo, además de cambiar el icono del botón usado para tal fin
        /// </summary>
        public void SoundControl()
        {
            if (AudioListener.pause)
            {
                AudioListener.pause = false;
                soundControlButton.GetComponent<Image>().sprite = audioOnSprite;
            }
            else
            {
                AudioListener.pause = true;
                soundControlButton.GetComponent<Image>().sprite = audioOffSprite;
            }
        }
    }
}
