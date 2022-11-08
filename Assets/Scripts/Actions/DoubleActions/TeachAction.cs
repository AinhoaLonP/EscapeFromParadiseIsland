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
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Actions
{
    /// <summary>
    /// Clase correspondiente a la acción del profesor
    /// . Implementa a <see cref="Assets.Scripts.Actions.BaseDoubleAction" />
    /// </summary>
    /// <seealso cref="Assets.Scripts.Actions.BaseDoubleAction" />
    public class TeachAction : BaseDoubleAction
    {
        /// <summary>
        /// Nombre del objeto al que deben dirigirse para realizar una acción
        /// </summary>
        public override string DestinationName { get => "TeacherPosition"; set { } }
        /// <summary>
        /// Nombre de la acción en el Animator
        /// </summary>
        public override string ActionName { get => "teach"; set { } }
        /// <summary>
        /// Texto que se muestra cuando una acción sale mal
        /// </summary>
        public override string MessText { get => StaticLanguageController.textMessages.TeachActionMaleMessText; set { } }
        /// <summary>
        /// Acción pasiva complementaria a ésta
        /// </summary>
        public override Action SecondaryAction { get; set; } = new LearnAction();
        /// <summary>
        /// Lugar hacia el que el personaje debe mirar al realizar la acción
        /// </summary>
        public override string WhereToLookAt { get => "StudentPosition"; set { } }
        /// <summary>
        /// Nombre de la acción para mostrar en los mensajes para el usuario
        /// </summary>
        public override string ActionNameForMessages { get => StaticLanguageController.textMessages.ActionNameForMessagesBeTeacher; set { } }

        /// <summary>
        /// Termina la acción y le muestra un mensaje al usuario para informarle de lo que ha aprendido el alumno
        /// </summary>
        public override void FinishSpecificAction(Game.CharacterController characterMovement)
        {
            double aptitude = characterMovement.characterStats.Where(a => a.Action.Name == this.GetType().Name).FirstOrDefault().Aptitude;
            CharacterAction secondaryCharacterAction = CharacterActionsController.charActionPairs.Where(c => c.Action is LearnAction && c.Character.transform == charAction.SecondaryCharacter.transform).LastOrDefault();
            //Fuerza la acción secundaria a terminar
            if (secondaryCharacterAction != null)
            secondaryCharacterAction.Action.FinishAction(secondaryCharacterAction, secondaryCharacterAction.CharacterScript);
            //Si la habilidad del profesor es 0, desastre
            if (aptitude == 0)
            {
                MakeAMess(characterMovement);
            }
            //Si la habilidad del profesor es mayor de 0.2, el alumno mejorará en una habilidad elegida aleatoriamente, en tantos puntos como la mitad de la habilidad del profesor.
            //Después, se le mostrará un mensaje al usuario para informarle de qué tal le ha ido al alumno.
            else if (aptitude > 0.2)
            {
                double aptitudeIncrement = Math.Round(aptitude / 2, 1);
                List<CharacterStats> allStats = secondaryCharacterAction.CharacterScript.characterStats.ToList();
                List<Action> elegibleActions = ActionsManager.Actions;
                List<CharacterStats> elegibleStats = allStats.Where(s => elegibleActions.Select(a => a.GetType().Name).Contains(s.Action.Name)).ToList();
                int index = StaticRandom.Instance.Next(elegibleStats.Count);
                CharacterStats chosenStat = elegibleStats.ElementAt(index);
                chosenStat.Aptitude += aptitudeIncrement;
                if (chosenStat.Aptitude > 1)
                    chosenStat.Aptitude = 1;
                string text;
                if (chosenStat.Aptitude == 1)
                {
                    if (secondaryCharacterAction.CharacterScript.gender == Gender.Female)
                        text = StaticLanguageController.textMessages.TeachActionFemaleExpertText;
                    else
                        text = StaticLanguageController.textMessages.TeachActionMaleExpertText;
                }
                else if (chosenStat.Aptitude >= 0.8)
                    text = StaticLanguageController.textMessages.TeachActionVeryGoodText;
                else if (chosenStat.Aptitude >= 0.6)
                    text = StaticLanguageController.textMessages.TeachActionQuiteGoodText;
                else if (chosenStat.Aptitude >= 0.4)
                    text = StaticLanguageController.textMessages.TeachActionALittleBetterText;
                else
                {
                    if (secondaryCharacterAction.CharacterScript.gender == Gender.Female)
                        text = StaticLanguageController.textMessages.TeachActionALittleBetterButBadFemaleText;
                    else
                        text = StaticLanguageController.textMessages.TeachActionALittleBetterButBadMaleText;
                }
                Type selectedAction = chosenStat.Action;
                string selectedActionName = elegibleActions.Where(a => a.GetType().Name == selectedAction.Name).Select(a => a.ActionNameForMessages).FirstOrDefault();

                string messageText = text.Replace("{alumno}", secondaryCharacterAction.CharacterScript.characterName).Replace("{actividad}", selectedActionName);
                PopupManager.ShowMessage(messageText);
            }
            //En caso de tener una habilidad de 0.1 o 0.2, no le enseñará nada a su alumno.
            else
            {
                string messageText = "";
                if (characterMovement.gender == Gender.Male)
                    messageText = StaticLanguageController.textMessages.TeachActionBadMaleTeacherText.Replace("{maestro}", characterMovement.characterName).Replace("{alumno}", secondaryCharacterAction.CharacterScript.characterName);
                else
                    messageText = StaticLanguageController.textMessages.TeachActionBadFemaleTeacherText.Replace("{maestro}", characterMovement.characterName).Replace("{alumno}", secondaryCharacterAction.CharacterScript.characterName);

                PopupManager.ShowMessage(messageText);
            }            
        }

        /// <summary>
        /// En caso de habilidad 0, el alumno tendrá habilidad 0 en una cuarta parte de las actividades disponibles, elegida al azar
        /// </summary>
        public override void MakeAMess(CharacterController characterMovement)
        {
            CharacterAction charActionOfSecondaryCharacter = CharacterActionsController.GetCharacterActionOfSecondaryCharacter(charAction);
            string originalText = MessText;
            if (characterMovement.gender == Gender.Female)
                originalText = StaticLanguageController.textMessages.TeachActionFemaleMessText;
            string text = originalText.Replace("{profesor}", characterMovement.characterName).Replace("{alumno}", charActionOfSecondaryCharacter.CharacterScript.characterName);
            PopupManager.ShowMessage(text);

            List<CharacterStats> allStats = charActionOfSecondaryCharacter.CharacterScript.characterStats.ToList();
            List<CharacterStats> selectedStats = new List<CharacterStats>();
            int allStatsCount = allStats.Count;
            for (int i = 0; i < allStatsCount / 4; i++)
            {
                int index = StaticRandom.Instance.Next(allStats.Count);
                selectedStats.Add(allStats.ElementAt(index));
                allStats.RemoveAt(index);
            }
            foreach (CharacterStats stat in selectedStats)
            {
                stat.Aptitude = 0;
            }
        }
    }
}
