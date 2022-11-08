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

namespace Assets.Scripts.Actions
{
    /// <summary>
    /// Clase que representa la acción de recoger agua
    /// . Implementa a <see cref="Assets.Scripts.Actions.BaseSimpleAction" />
    /// </summary>
    /// <seealso cref="Assets.Scripts.Actions.BaseSimpleAction" />
    public class PickWaterAction : BaseSimpleAction
    {
        /// <summary>
        /// Nombre del objeto al que deben dirigirse para realizar una acción
        /// </summary>
        public override string DestinationName { get => "Lake"; set { } }
        /// <summary>
        /// Nombre de la acción en el Animator
        /// </summary>
        public override string ActionName { get => "pickup"; set { } }
        /// <summary>
        /// Texto que se muestra cuando una acción sale mal
        /// </summary>
        public override string MessText { get => StaticLanguageController.textMessages.PickWaterActionMessMaleText; set { } }
        /// <summary>
        /// Cantidad en la que incrementar las unidades del inventario
        /// </summary>
        public override int IncrementQuantity { get => 40; set { } }
        /// <summary>
        /// Nombre de la acción para mostrar en los mensajes para el usuario
        /// </summary>
        public override string ActionNameForMessages { get => StaticLanguageController.textMessages.ActionNameForMessagesPickWater; set { } }

        /// <summary>
        /// Derrama el agua
        /// </summary>
        public override void MakeAMess(CharacterController characterMovement)
        {
            string text = MessText;
            if (characterMovement.gender == Utils.Gender.Female)
                text = StaticLanguageController.textMessages.PickWaterActionMessFemaleText;
            text = text.Replace("{characterName}", characterMovement.characterName);
            PopupManager.ShowMessage(text);
            Inventory.PourWater();
        }
    }
}
