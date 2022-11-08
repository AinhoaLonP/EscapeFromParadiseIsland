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
using UnityEngine;

namespace Assets.Scripts.Actions
{
    /// <summary>
    /// Clase que representa la acción de recoger comida
    /// . Implementa a <see cref="Assets.Scripts.Actions.BaseSimpleAction" />
    /// </summary>
    /// <seealso cref="Assets.Scripts.Actions.BaseSimpleAction" />
    public class PickFoodAction : BaseSimpleAction
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
        public override string MessText { get => StaticLanguageController.textMessages.PickFoodActionMessText; set { } }
        /// <summary>
        /// Cantidad en la que incrementar las unidades del inventario
        /// </summary>
        public override int IncrementQuantity { get => 40; set { } }
        /// <summary>
        /// Nombre de la acción para mostrar en los mensajes para el usuario
        /// </summary>
        public override string ActionNameForMessages { get => StaticLanguageController.textMessages.ActionNameForMessagesPickFood; set { } }


        /// <summary>
        /// Encuentra la lista de posibles destinos
        /// </summary>
        private void Start()
        {
            destinations = GameObject.Find("Palms");
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
        /// Coge bayas venenosas
        /// </summary>
        public override void MakeAMess(Assets.Scripts.Game.CharacterController characterMovement)
        {
            string text = MessText.Replace("{characterName}", characterMovement.characterName);
            PopupManager.ShowMessage(text);
            Inventory.TakeToxicFood();
        }
    }
}
