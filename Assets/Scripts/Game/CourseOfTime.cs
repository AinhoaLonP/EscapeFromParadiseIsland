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
using Assets.Scripts.Internationalization;
using Assets.Scripts.UI;
using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Game
{
    /// <summary>
    /// Clase que controla el transcurso del tiempo en el juego
    /// </summary>
    public class CourseOfTime : MonoBehaviour
    {
        /// <summary>
        /// Momento de inicio de la partida
        /// </summary>
        private DateTime startTime;
        /// <summary>
        /// Días transcurridos
        /// </summary>
        public static int elapsedDays;
        /// <summary>
        /// Horas transcurridas
        /// </summary>
        public static int elapsedHours;
        /// <summary>
        /// Panel que indica que el jugador ha perdido la partida
        /// </summary>
        public GameObject gameOverPanel;

        /// <summary>
        /// Se inicializan los valores del tiempo transcurrido a cero y se desactiva el panel "GameOver"
        /// </summary>
        void Start()
        {
            startTime = DateTime.Now;
            elapsedDays = 0;
            elapsedHours = 0;
            gameOverPanel = GameObject.Find("GameOverPanel");
            gameOverPanel.SetActive(false);
        }

        /// <summary>
        /// Llamado cada frame. Cuando es necesario, actualiza el tiempo transcurrido en el juego
        /// </summary>
        void Update()
        {
            double elapsedTimeMilliseconds = (DateTime.Now - startTime).TotalMilliseconds;
            if (elapsedTimeMilliseconds >= Constants.millisecondsInAnHour)
            {
                IncrementHour();
                startTime = DateTime.Now;
            }

            //Se muestra el tiempo transcurrido en el juego
            TextMeshProUGUI mText = transform.GetComponent<TextMeshProUGUI>();
            if (transform.name == "ElapsedTime")
            {
                mText.text = elapsedDays + "d " + elapsedHours + "h";
            }

            //Comprueba si queda algún personaje vivo. Si no es así, muestra el panel de GameOver
            if (!CharacterActionsController.IsThereAnyActiveCharacter())
            {
                GameObject popupPanel = GameObject.Find("PopupMessage");
                if (popupPanel != null)
                    popupPanel.SetActive(false);
                gameOverPanel.SetActive(true);
            }
        }

        /// <summary>
        /// Incrementa un día en el calendario
        /// </summary>
        void IncrementDay()
        {
            elapsedDays++;
            DecreaseMotivation();
        }

        /// <summary>
        /// Cada día transcurrido, los personajes pierden la motivación en todas las tareas un 0.1
        /// </summary>
        void DecreaseMotivation()
        {
            List<CharacterAction> pairs = CharacterActionsController.charActionPairs;
            List<CharacterController> scripts = pairs.Where(p => p.Status != CharacterActionStatus.Dead).Select(p => p.CharacterScript).ToList();
            foreach (CharacterController script in scripts)
            {
                foreach (CharacterStats stat in script.characterStats)
                {
                    double originalAttitude = stat.Attitude;
                    if (originalAttitude - 0.1 >= Constants.lowAttitudeLimitAsTimePassing)
                    {
                        stat.Attitude = stat.Attitude - 0.1;
                    }
                }
            }
        }

        /// <summary>
        /// Se incrementa el reloj en una hora
        /// </summary>
        void IncrementHour()
        {
            if (elapsedHours < 23)
            {
                elapsedHours++;
            }
            else
            {
                elapsedHours = 0;
                IncrementDay();
            }
            Inventory.ConsumeWater();
            Inventory.ConsumeFood();
            Inventory.PeriodicBuildingDamage(Constants.buildingDamageEachTime);

            int generalHealth = Inventory.GetHealth();
            //Cuando la salud está a menos del 33%, cada hora puede morir un personaje
            if (generalHealth < 33)
            {
                PossibleDeath();
            }

            //Si han pasado mucho tiempo al sol, se muestra un mensaje de advertencia y la salud empieza a bajar
            if (elapsedHours == Constants.hoursElapsedForSunstrokeDanger && elapsedDays == 0 && !Inventory.refugeFinished)
            {
                PopupManager.ShowMessage(StaticLanguageController.textMessages.TooMuchSunText);
            }
            else if ((elapsedHours > Constants.hoursElapsedForSunstrokeDanger || elapsedDays > 0) && (!Inventory.refugeFinished || Inventory.refugeBuildPercentage == 0))
            {
                Inventory.HaveSunstroke();
            }
        }

        /// <summary>
        /// Cada vez que se ejecuta, cabe la posibilidad de que uno de los personajes, elegido al azar, muera, dependiendo del valor de "possibleDeathLimit" y de un número aleatorio.
        /// Si muere, se desasignan todas sus acciones relacionadas y se muestra un mensaje al usuario.
        /// </summary>
        void PossibleDeath()
        {
            try
            {
                double random = Math.Round(StaticRandom.Instance.NextDouble(), 1);
                if (random >= Constants.possibleDeathLimit)
                {
                    CharacterAction selectedCharacterAction = CharacterActionsController.SelectRandomCharacterAction();
                    if (selectedCharacterAction != null)
                    {
                        CharacterAction secondary = CharacterActionsController.GetCharacterActionOfSecondaryCharacter(selectedCharacterAction);
                        if (secondary == null)
                        {
                            secondary = CharacterActionsController.GetPrimaryCharacterActionOfSecondaryCharacterAction(selectedCharacterAction);
                        }
                        if (secondary != null)
                        {
                            if (secondary.Action != null && secondary.CharacterScript != null)
                                secondary.Action.FinishAction(secondary, secondary.CharacterScript);
                        }

                        if (selectedCharacterAction.Action is BaseDoubleAction)
                        {
                            ((BaseDoubleAction)selectedCharacterAction.Action).button.interactable = true;
                        }

                        selectedCharacterAction.Action = null;
                        selectedCharacterAction.ObjectToGo = null;
                        selectedCharacterAction.Status = CharacterActionStatus.Dead;
                        if (selectedCharacterAction.CharacterScript != null)
                        {
                            selectedCharacterAction.CharacterScript.Die();
                            Inventory.numberOfActiveCharacters--;
                            string text = StaticLanguageController.textMessages.DieText.Replace("{characterName}", selectedCharacterAction.CharacterScript.characterName);
                            PopupManager.ShowMessage(text);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}