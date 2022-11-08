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
    /// Maneja los botones de subir y bajar en el tutorial
    /// </summary>
    public class TutorialRectTransformSlider : MonoBehaviour
    {
        /// <summary>
        /// GameObject del tutorial
        /// </summary>
        public GameObject gameObjectWithRectTransform;
        /// <summary>
        /// RectTransform del tutorial
        /// </summary>
        private RectTransform rectTransform;

        /// <summary>
        /// Llamado al comienzo de la partida
        /// </summary>
        void Start()
        {
            rectTransform = gameObjectWithRectTransform.GetComponent<RectTransform>();
        }

        /// <summary>
        /// Mueve el tutorial hacia abajo
        /// </summary>
        public void MoveDown()
        {
            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y + 15, rectTransform.localPosition.z);
        }

        /// <summary>
        /// Mueve el tutorial hacia arriba
        /// </summary>
        public void MoveUp()
        {
            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y - 15, rectTransform.localPosition.z);
        }
    }
    
}