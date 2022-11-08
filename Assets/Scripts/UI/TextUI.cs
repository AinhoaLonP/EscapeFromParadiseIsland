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

namespace Assets.Scripts.UI
{
    /// <summary>
    /// Clase usada para que el texto que aparece sobre los personajes esté siempre de frente a la cámara
    /// </summary>
    public class TextUI : MonoBehaviour
    {
        /// <summary>
        /// Cámara hacia la que el texto debe mirar
        /// </summary>
        Camera cameraToLookAt;

        /// <summary>
        /// Llamado al inicio de la partida
        /// </summary>
        void Start()
        {
            cameraToLookAt = Camera.main;
        }

        /// <summary>
        /// Llamado cada frame para actualizar la posición del texto
        /// </summary>
        void Update()
        {
            transform.LookAt(cameraToLookAt.transform);
            transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
        }
    }
}