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
using System;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    /// <summary>
    /// Acción de descansar
    /// . Implementa a <see cref="Assets.Scripts.Actions.BasePasiveAction" />
    /// </summary>
    /// <seealso cref="Assets.Scripts.Actions.BasePasiveAction" />
    public class RestAction : BasePasiveAction
    {
        /// <summary>
        /// Lista de objetos hacia los cuales el personaje se puede dirigir para realizar la acción
        /// </summary>
        private GameObject destinations;

        /// <summary>
        /// Nombre del objeto al que deben dirigirse para realizar una acción
        /// </summary>
        public override string DestinationName { get => GetRandomDestination(destinations); set { } }
        /// <summary>
        /// Nombre de la acción en el Animator
        /// </summary>
        public override string ActionName { get => "rest"; set { } }

        /// <summary>
        /// Lugar hacia el que el personaje debe mirar
        /// </summary>
        public override string WhereToLookAt { get => "HorizonPosition"; set { } }

        /// <summary>
        /// Obtiene un lugar aleatorio en la playa para dirigirse a él
        /// </summary>
        /// <param name="destinations">El conjunto de posibles lugares a los que ir</param>
        /// <returns>System.String.</returns>
        private string GetRandomDestination(GameObject destinations)
        {
            destinations = GameObject.Find("Sand");
            System.Random random = new System.Random();
            int index = random.Next(0, destinations.transform.childCount);
            return destinations.transform.GetChild(index).name;
        }

        /// <summary>
        /// Se asigna al último personaje seleccionado y enviado a realizar una actividad que no quiere hacer
        /// </summary>
        public override void SelectAction()
        {
            CharacterAction charAction = CharacterActionsController.GetLastSelectedCharacterAction();
            if (charAction != null)
            {
                charAction.Character.GetComponent<Animator>().SetTrigger("refuse");
                PlayRandomNoSound(charAction);
                charAction.ObjectToGo = GameObject.Find(DestinationName);
                charAction.Action = this;
                charAction.Status = CharacterActionStatus.Refusing;
            }
        }

        public void PlayRandomNoSound(CharacterAction charAction)
        {
            AudioClip[] audioSources = charAction.CharacterScript.audioSources;
            AudioSource audioSource = charAction.Character.GetComponent<AudioSource>();
            if (audioSources.Length > 0)
            {
                audioSource.clip = audioSources[UnityEngine.Random.Range(0, audioSources.Length)];
                audioSource.Play();
            }

        }

        /// <summary>
        /// El personaje comienza a realizar la acción
        /// </summary>
        /// <param name="characterMovement">Script del personaje seleccionado</param>
        public override void StartAction(Game.CharacterController characterMovement)
        {
            CharacterAction charAction = CharacterActionsController.GetCharacterActionOfCharacter(characterMovement.transform);
            charAction.StartTime = DateTime.Now;
            characterMovement.actionRemainingTime = ActionDurationMilliseconds / 1000;
            characterMovement.ShowActionRemainingTime();
            characterMovement.transform.Rotate(0, 90, 0);
        }
    }
}
