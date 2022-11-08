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
using Assets.Scripts.Internationalization;
using Assets.Scripts.UI;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Actions
{
    /// <summary>
    /// Clase que representa la acción de recoger recursos
    /// . Implementa a <see cref="Assets.Scripts.Actions.BaseSimpleAction" />
    /// </summary>
    /// <seealso cref="Assets.Scripts.Actions.BaseSimpleAction" />
    public class PickResourcesAction : BaseSimpleAction
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
        public override string ActionName { get => "pickup"; set { } }
        /// <summary>
        /// Texto que se muestra cuando una acción sale mal
        /// </summary>
        public override string MessText { get => StaticLanguageController.textMessages.PickResourcesActionMessMaleText; set { } }
        /// <summary>
        /// Cantidad en la que incrementar las unidades del inventario
        /// </summary>
        public override int IncrementQuantity { get => 40; set { } }
        /// <summary>
        /// Nombre de la acción para mostrar en los mensajes para el usuario
        /// </summary>
        public override string ActionNameForMessages { get => StaticLanguageController.textMessages.ActionNameForMessagesPickResources; set { } }

        /// <summary>
        /// Encuentra la lista de posibles destinos
        /// </summary>
        private void Start()
        {
            destinations = GameObject.Find("Logs");
        }

        /// <summary>
        /// De la lista de posibles lugares donde realizar la acción, obtiene uno aleatorio
        /// </summary>
        /// <returns>System.String.</returns>
        private string GetRandomDestination(GameObject destinations)
        {
            System.Random random = new System.Random();
            int index = random.Next(0, destinations.transform.childCount);
            return destinations.transform.GetChild(index).name;
        }

        /// <summary>
        /// Se tuerce un tobillo
        /// </summary>
        public override void MakeAMess(Assets.Scripts.Game.CharacterController characterMovement)
        {
            string text = MessText;
            if (characterMovement.gender == Utils.Gender.Female)
                text = StaticLanguageController.textMessages.PickResourcesActionMessFemaleText;
            text = text.Replace("{characterName}", characterMovement.characterName);
            PopupManager.ShowMessage(text);
            GoRest(characterMovement.characterName);
        }

        /// <summary>
        /// En caso de tener habilidad 0 en esta tarea, el personaje se irá a descansar durante "timeMultiplierInRest" veces el tiempo usual de reposo
        /// </summary>
        /// <param name="characterName">Name of the character.</param>
        private void GoRest(string characterName)
        {
            CharacterAction characterAction = CharacterActionsController.charActionPairs.Where(c => c.Action is PickResourcesAction && c.CharacterScript.characterName.Equals(characterName)).LastOrDefault();

            RestAction restAction = new RestAction();
            int newDuration = restAction.ActionDurationMilliseconds * Constants.pickResourcesActionTimeMultiplierInRest;
            restAction.ActionDurationMilliseconds = newDuration;
            characterAction.ObjectToGo = GameObject.Find(restAction.DestinationName);
            characterAction.Action = restAction;
            characterAction.Status = CharacterActionStatus.MovingToActionLocation;
        }
    }
}
