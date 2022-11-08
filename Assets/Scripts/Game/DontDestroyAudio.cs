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

namespace Assets.Scripts.Game
{
    /// <summary>
    /// Para no pausar la música entre la escena del menú principal y la de juego
    /// </summary>
    public class DontDestroyAudio : MonoBehaviour
    {
        /// <summary>
        /// Hace que la música siga sonando al cambiar de escena
        /// </summary>
        private void Awake()
        {
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}
