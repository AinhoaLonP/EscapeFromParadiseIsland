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
using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts.Game
{
    /// <summary>
    /// Enumerado de los estados que puede tener un CharacterAction
    /// </summary>
    public enum CharacterActionStatus : int
    {
        Free = 1,
        MovingToActionLocation = 2,
        DoingAction = 3,
        ReturningFromActionLocation = 4,
        WaitingForSecondaryCharacter = 5,
        Dead = 6,
        Refusing = 7
    }

    /// <summary>
    /// Objeto que agrupa a cada personaje con la acción que está realizando, además de otros datos de interés
    /// </summary>
    public class CharacterAction
    {
        /// <summary>
        /// GameObject del personaje principal
        /// </summary>
        public GameObject Character { get; set; }
        /// <summary>
        /// GameObject del personaje secundario
        /// </summary>
        public GameObject SecondaryCharacter { get; set; }
        /// <summary>
        /// Acción principal
        /// </summary>
        public Action Action { get; set; }
        /// <summary>
        /// Acción secundaria
        /// </summary>
        public Action SecondaryAction { get; set; }
        /// <summary>
        /// Estado de la acción
        /// </summary>
        public CharacterActionStatus Status { get; set; }
        /// <summary>
        /// Lugar al que el personaje se debe desplazar antes de comenzar la acción
        /// </summary>
        public GameObject ObjectToGo { get; set; }
        /// <summary>
        /// Script del personaje principal
        /// </summary>
        public CharacterController CharacterScript { get; set; }
        /// <summary>
        /// Script del personaje secundario
        /// </summary>
        public CharacterController SecondaryCharacterScript { get; set; }
        /// <summary>
        /// Momento de inicio de la acción
        /// </summary>
        public System.DateTime StartTime { get; set; }
        /// <summary>
        /// Determina si el personaje es el último seleccionado
        /// </summary>
        public bool LastSelected { get; set; }

        public bool RefusePlayed { get; set; }
    }
}
