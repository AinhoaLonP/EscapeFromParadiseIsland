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

namespace Assets.Scripts.Actions
{
    /// <summary>
    /// Interfaz Action con los métodos principales
    /// </summary>
    public interface Action
    {
        /// <summary>
        /// Duración de la acción en ms
        /// </summary>
        int ActionDurationMilliseconds { get; set; }
        /// <summary>
        /// Nombre del lugar al que el personaje debe dirigirse para realizar la acción
        /// </summary>
        string DestinationName { get; set; }
        /// <summary>
        /// Nombre de la acción para mostrar en los mensajes al usuario
        /// </summary>
        string ActionNameForMessages { get; set; }

        /// <summary>
        /// Selecciona la acción
        /// </summary>
        void SelectAction();

        /// <summary>
        /// Comienza la acción
        /// </summary>
        void StartAction(CharacterController characterMovement);

        /// <summary>
        /// Realiza la acción. Llamado cada frame
        /// </summary>
        void MakeAction(CharacterController characterMovement);

        /// <summary>
        /// Finaliza la acción
        /// </summary>
        void FinishAction(CharacterAction charAction, CharacterController characterMovement);
    }
}
