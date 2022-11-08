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
    /// Clase encargada de manejar el zoom y el movimiento de la cámara principal
    /// </summary>
    public class CameraHandler : MonoBehaviour
    {
        /// <summary>
        /// Zoom inicial
        /// </summary>
        private float zoom = 80f;

        /// <summary>
        /// Llamado cada frame para actualizar la posición y el zoom de la cámara
        /// </summary>
        void Update()
        {
            HandleEdgeScrolling();
            HandleZoom();
        }

        /// <summary>
        /// Maneja el movimiento de la cámara hacia los lados, arriba y abajo
        /// </summary>
        void HandleEdgeScrolling()
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, 5, 40),
                Mathf.Clamp(transform.position.y, 10, 38),
                Mathf.Clamp(transform.position.z, -25, 12)
                );

            if (Input.mousePosition.x >= Screen.width - Constants.mDelta && transform.position.x <= Screen.width)
            {
                transform.position += transform.right * Time.deltaTime * Constants.mSpeed;
            }

            if (Input.mousePosition.x <= 0 + Constants.mDelta)
            {
                transform.position -= transform.right * Time.deltaTime * Constants.mSpeed;
            }
            if (Input.mousePosition.y >= Screen.height - Constants.mDelta)
            {
                transform.position += transform.forward * Time.deltaTime * Constants.mSpeed;
            }
            if (Input.mousePosition.y <= 0 + Constants.mDelta)
            {
                transform.position -= transform.up * Time.deltaTime * Constants.mSpeed;
            }
        }

        /// <summary>
        /// Maneja el zoom de la cámara
        /// </summary>
        void HandleZoom()
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                zoom -= Constants.zoomChangeAmount * Time.deltaTime * 10f;
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                zoom += Constants.zoomChangeAmount * Time.deltaTime * 10f;
            }
            zoom = Mathf.Clamp(zoom, Constants.minZoom, Constants.maxZoom);
            Camera.main.fieldOfView = zoom;
        }
    }
}
