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

using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    /// <summary>
    /// Maneja los mensajes que se le muestran al usuario
    /// </summary>
    public class PopupManager : MonoBehaviour
    {
        /// <summary>
        /// Panel en el que mostrar los mensajes
        /// </summary>
        public static GameObject Panel;

        /// <summary>
        /// Al principio, desactiva el panel
        /// </summary>
        private void Start()
        {
            Panel = GameObject.Find("PopupMessage");
            Panel.SetActive(false);
        }

        /// <summary>
        /// Muestra el mensaje pasado como parámetro
        /// </summary>
        /// <param name="text"></param>
        public static void ShowMessage(string text)
        {
            if (Panel != null)
            {
                Panel.SetActive(true);
                GameObject audioSource = GameObject.Find("MessageSound");
                audioSource.GetComponent<AudioSource>().Play();
                GameObject message = GameObject.Find("MessageText");
                TextMeshProUGUI mText = message.transform.GetComponent<TextMeshProUGUI>();
                mText.text = text;
            }
        }

        /// <summary>
        /// Desactiva el panel
        /// </summary>
        public void HideMessage()
        {
            if (Panel != null)
            {
                Panel.SetActive(false);
            }
        }
    }
}