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
using Assets.Scripts.Utils;
using System.Linq;

namespace Assets.Scripts.Actions
{
    /// <summary>
    /// Clase de la acción de curar
    /// . Implementa a <see cref="Assets.Scripts.Actions.BaseDoubleAction" />
    /// </summary>
    /// <seealso cref="Assets.Scripts.Actions.BaseDoubleAction" />
    public class HealAction : BaseDoubleAction
    {
        /// <summary>
        /// Nombre del objeto al que deben dirigirse para realizar una acción
        /// </summary>
        public override string DestinationName { get => "DoctorPosition"; set { } }
        /// <summary>
        /// Nombre de la acción en el Animator
        /// </summary>
        public override string ActionName { get => "heal"; set { } }
        /// <summary>
        /// Texto que se muestra cuando una acción sale mal
        /// </summary>
        public override string MessText { get => StaticLanguageController.textMessages.HealActionMaleMessText; set { } }
        /// <summary>
        /// Acción pasiva complementaria a ésta
        /// </summary>
        public override Action SecondaryAction { get; set; } = new BeHealedAction();
        /// <summary>
        /// Lugar hacia el que el personaje debe mirar al realizar la acción
        /// </summary>
        public override string WhereToLookAt { get => "DoctorPatientPosition"; set { } }
        /// <summary>
        /// Nombre de la acción para mostrar en los mensajes para el usuario
        /// </summary>
        public override string ActionNameForMessages { get => StaticLanguageController.textMessages.ActionNameForMessagesMedicine; set { } }

        /// <summary>
        /// Termina la acción. No muestra mensaje al usuario, sólo sube o baja la barra de salud.
        /// </summary>
        /// <param name="characterMovement">The character movement.</param>
        public override void FinishSpecificAction(Game.CharacterController characterMovement)
        {
            double aptitude = characterMovement.characterStats.Where(a => a.Action.Name == this.GetType().Name).FirstOrDefault().Aptitude;
            CharacterAction secondaryCharacterAction = CharacterActionsController.charActionPairs.Where(c => c.Action is BeHealedAction && c.Character.transform == charAction.SecondaryCharacter.transform).LastOrDefault();
            //Fuerza la acción secundaria a terminar
            if (secondaryCharacterAction != null)
                secondaryCharacterAction.Action.FinishAction(secondaryCharacterAction, secondaryCharacterAction.CharacterScript);
            if (aptitude == 0)
                MakeAMess(characterMovement);
            //Si la habilidad del médico es mayor de 0.2, la salud sube de forma proporcional a su habilidad. En caso contrario, baja.
            else if (aptitude > 0.2)            
                Inventory.Heal(aptitude);            
            else            
                Inventory.DamageHealth(aptitude);            
        }

        /// <summary>
        /// Su paciente muere
        /// </summary>
        /// <param name="characterName">Nombre del personaje</param>
        public override void MakeAMess(CharacterController characterMovement)
        {
            CharacterAction charActionOfSecondaryCharacter = CharacterActionsController.GetCharacterActionOfSecondaryCharacter(charAction);
            string originalText = MessText;
            if (characterMovement.gender == Gender.Female)
                originalText = StaticLanguageController.textMessages.HealActionFemaleMessText;
            string text = originalText.Replace("{medico}", characterMovement.characterName).Replace("{paciente}", charActionOfSecondaryCharacter.CharacterScript.characterName);
            PopupManager.ShowMessage(text);
            charActionOfSecondaryCharacter.Action = null;
            charActionOfSecondaryCharacter.ObjectToGo = null;
            charActionOfSecondaryCharacter.Status = CharacterActionStatus.Dead;
            charActionOfSecondaryCharacter.CharacterScript.Die();
        }
    }
}
