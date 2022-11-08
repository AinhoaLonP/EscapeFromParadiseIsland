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
using System.Text;

namespace Assets.Scripts.Actions
{
    /// <summary>
    /// Clase correspondiente a la acción del psicólogo
    /// . Implementa a <see cref="Assets.Scripts.Actions.BaseDoubleAction" />
    /// </summary>
    /// <seealso cref="Assets.Scripts.Actions.BaseDoubleAction" />
    public class PsychologistTreatAction : BaseDoubleAction
    {
        /// <summary>
        /// Nombre del objeto al que deben dirigirse para realizar una acción
        /// </summary>
        public override string DestinationName { get => "PsychologistPosition"; set { } }
        /// <summary>
        /// Nombre de la acción en el Animator
        /// </summary>
        public override string ActionName { get => "psychologistTreat"; set { } }
        /// <summary>
        /// Texto que se muestra cuando una acción sale mal
        /// </summary>
        public override string MessText { get => StaticLanguageController.textMessages.PsychologistTreatActionMaleMessText; set { } }
        /// <summary>
        /// Acción pasiva complementaria a ésta
        /// </summary>
        public override Action SecondaryAction { get; set; } = new PsychologistBeTreatedAction();
        /// <summary>
        /// Lugar hacia el que el personaje debe mirar al realizar la acción
        /// </summary>
        public override string WhereToLookAt { get => "PsychologistPatientPosition"; set { } }
        /// <summary>
        /// Nombre de la acción para mostrar en los mensajes para el usuario
        /// </summary>
        public override string ActionNameForMessages { get => StaticLanguageController.textMessages.ActionNameForMessagesPsychology; set { } }

        /// <summary>
        /// Termina la acción y le muestra un mensaje al usuario para informarle de cómo le ha ido al paciente
        /// </summary>
        public override void FinishSpecificAction(Game.CharacterController characterMovement)
        {
            double aptitude = characterMovement.characterStats.Where(a => a.Action.Name == this.GetType().Name).FirstOrDefault().Aptitude;
            CharacterAction secondaryCharacterAction = CharacterActionsController.charActionPairs.Where(c => c.Action is PsychologistBeTreatedAction 
            && c.Character.transform == charAction.SecondaryCharacter.transform).LastOrDefault();
            //Fuerza la acción secundaria a terminar
            if (secondaryCharacterAction != null)
                secondaryCharacterAction.Action.FinishAction(secondaryCharacterAction, secondaryCharacterAction.CharacterScript);
            if (aptitude == 0)
            {
                MakeAMess(characterMovement);
            }
            //Si la habilidad del psicólogo es mayor que 0.3, motivará a su paciente en una serie de actividades, elegidas aleatoriamente.
            //Cuanto mayor sea la habilidad, en más actividades le motivará.
            else if (aptitude > 0.3)
            {
                int numberOfActionsToIncreaseAttitudeIn = Convert.ToInt32((aptitude * 10) - 3);
                List<CharacterStats> allStats = secondaryCharacterAction.CharacterScript.characterStats.ToList();
                List<Action> elegibleActions = ActionsManager.Actions;
                List<CharacterStats> elegibleStats = allStats.Where(s => elegibleActions.Select(a => a.GetType().Name).Contains(s.Action.Name)).ToList();
                List<CharacterStats> selectedStats = new List<CharacterStats>();
                for (int i = 0; i < numberOfActionsToIncreaseAttitudeIn; i++)
                {
                    int index = StaticRandom.Instance.Next(elegibleStats.Count);
                    selectedStats.Add(elegibleStats.ElementAt(index));
                    elegibleStats.RemoveAt(index);
                }
                foreach (CharacterStats stat in selectedStats)
                {
                    stat.Attitude = stat.Attitude + 0.2;
                    if (stat.Attitude > 1)
                    {
                        stat.Attitude = 1;
                    }
                }
                List<Type> selectedActions = selectedStats.Select(s => s.Action).ToList();
                List<string> selectedActionNames = elegibleActions.Where(a => selectedActions.Select(s => s.Name).Contains(a.GetType().Name)).Select(a => a.ActionNameForMessages).ToList();
                StringBuilder sb = new StringBuilder();
                sb.Append(selectedActionNames.ElementAt(0));
                if (selectedActionNames.Count > 1)
                {
                    for (int i = 1; i < selectedActionNames.Count; i++)
                    {
                        sb.Append(", ");
                        sb.Append(selectedActionNames.ElementAt(i));
                    }
                }
                string messageText;
                if (secondaryCharacterAction.CharacterScript.gender == Gender.Male)
                    messageText = StaticLanguageController.textMessages.PsychologistTreatActionUpgradeMaleText.Replace("{paciente}", secondaryCharacterAction.CharacterScript.characterName) + sb.ToString();
                else
                    messageText = StaticLanguageController.textMessages.PsychologistTreatActionUpgradeFemaleText.Replace("{paciente}", secondaryCharacterAction.CharacterScript.characterName) + sb.ToString();

                PopupManager.ShowMessage(messageText);
            }
            //Si la habilidad del psicólogo está entre 0.1 y 0.3, desmotivará en 0.1 puntos al paciente en un conjunto aleatorio de actividades.
            //Cuanto menor sea la habilidad, mayor será el número de actividades para las que perderá motivación.
            else
            {
                int numberOfActionsToDecreaseAttitudeIn = Convert.ToInt32(4 - (aptitude * 10));
                List<CharacterStats> allStats = secondaryCharacterAction.CharacterScript.characterStats.ToList();
                List<Action> elegibleActions = ActionsManager.Actions; 
                List<CharacterStats> elegibleStats = allStats.Where(s => elegibleActions.Select(a => a.GetType().Name).Contains(s.Action.Name)).ToList();
                List<CharacterStats> selectedStats = new List<CharacterStats>();
                for (int i = 0; i < numberOfActionsToDecreaseAttitudeIn; i++)
                {
                    int index = StaticRandom.Instance.Next(elegibleStats.Count);
                    selectedStats.Add(elegibleStats.ElementAt(index));
                    elegibleStats.RemoveAt(index);
                }
                foreach (CharacterStats stat in selectedStats)
                {
                    stat.Attitude = stat.Attitude - 0.1;
                    if (stat.Attitude < 0)
                    {
                        stat.Attitude = 0;
                    }
                }
                List<Type> selectedActions = selectedStats.Select(s => s.Action).ToList();
                List<string> selectedActionNames = elegibleActions.Where(a => selectedActions.Select(s => s.Name).Contains(a.GetType().Name)).Select(a => a.ActionNameForMessages).ToList();
                StringBuilder sb = new StringBuilder();
                sb.Append(selectedActionNames.ElementAt(0));
                if (selectedActionNames.Count > 1)
                {
                    for (int i = 1; i < selectedActionNames.Count; i++)
                    {
                        sb.Append(", ");
                        sb.Append(selectedActionNames.ElementAt(i));
                    }
                }
                string messageText = StaticLanguageController.textMessages.PsychologistTreatActionDowngradeText.Replace("{paciente}", secondaryCharacterAction.CharacterScript.characterName) + sb.ToString();
                PopupManager.ShowMessage(messageText);
            }
        }

        /// <summary>
        /// El paciente perderá por completo la motivación por realizar la mitad de las actividades
        /// </summary>
        /// <param name="characterName">Nombre del personaje</param>
        public override void MakeAMess(CharacterController characterMovement)
        {
            CharacterAction charActionOfSecondaryCharacter = CharacterActionsController.GetCharacterActionOfSecondaryCharacter(charAction);
            string originalText = MessText;
            if (charActionOfSecondaryCharacter.CharacterScript.gender == Gender.Female)
                originalText = StaticLanguageController.textMessages.PsychologistTreatActionFemaleMessText;
            string text = originalText.Replace("{psicologo}", characterMovement.characterName).Replace("{paciente}", charActionOfSecondaryCharacter.CharacterScript.characterName);
            PopupManager.ShowMessage(text);

            List<CharacterStats> allStats = charActionOfSecondaryCharacter.CharacterScript.characterStats.ToList();
            List<CharacterStats> selectedStats = new List<CharacterStats>();
            int allStatsCount = allStats.Count;
            for (int i = 0; i < allStatsCount/2; i++)
            {
                int index = StaticRandom.Instance.Next(allStats.Count);
                selectedStats.Add(allStats.ElementAt(index));
                allStats.RemoveAt(index);
            }
            foreach (CharacterStats stat in selectedStats)
            {
                stat.Attitude = 0;
            }
        }
    }
}
