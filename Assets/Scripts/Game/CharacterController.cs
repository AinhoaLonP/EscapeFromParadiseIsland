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

using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Game
{
    /// <summary>
    /// Controlador del personaje
    /// </summary>
    public class CharacterController : MonoBehaviour
    {
        /// <summary>
        /// El NavMesh
        /// </summary>
        private NavMeshAgent navMeshAgent;
        /// <summary>
        /// Determina si el personaje está seleccionado
        /// </summary>
        private bool isSelected = false;
        /// <summary>
        /// Es el color original de la camiseta del personaje
        /// </summary>
        private Color originalColor;
        /// <summary>
        /// Referencia a CharacterStatsManager
        /// </summary>
        private CharacterStatsManager characterStatsManager;
        /// <summary>
        /// Momento de la muerte, de haberla
        /// </summary>
        private DateTime deathTime;
        /// <summary>
        /// Determina si el personaje está muerto
        /// </summary>
        private bool dead = false;

        /// <summary>
        /// Tiempo restante para terminar la acción
        /// </summary>
        public int actionRemainingTime;
        /// <summary>
        /// Lista con los stats del personaje
        /// </summary>
        public List<CharacterStats> characterStats;
        /// <summary>
        /// El género del personaje
        /// </summary>
        public Gender gender;
        /// <summary>
        /// Nombre del personaje
        /// </summary>
        public string characterName;
        /// <summary>
        /// Posición central
        /// </summary>
        public Vector3 centerPosition;
        /// <summary>
        /// Determina si el personaje está libre para realizar alguna acción
        /// </summary>
        public bool free;
        /// <summary>
        /// Animator del personaje
        /// </summary>
        public Animator animator;
        /// <summary>
        /// Posición original del personaje
        /// </summary>
        public Vector3 originalPosition;
        /// <summary>
        /// Determina si el personaje está corriendo
        /// </summary>
        public bool running = false;


        private int healthPoints;
        private int socialPoints;
        private int moodPoints;

        public Canvas individualBarsHUD;
        public AudioClip[] audioSources;

        /// <summary>
        /// Llamado al comienzo de la partida
        /// </summary>
        void Start()
        {
            animator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            free = true;
            originalPosition = transform.position;
            individualBarsHUD.enabled = false;
            transform.Find("SelectionSymbol").GetComponent<SpriteRenderer>().enabled = false;
            //Se obtiene una posición céntrica
            centerPosition = Utils.Utils.GetRandomCenteredPosition();
            //Se le asigna un nombre aleatorio según su género
            characterName = Utils.Utils.GetRandomName(gender);
            characterStatsManager = new CharacterStatsManager();
            characterStats = characterStatsManager.AssignRandomAttitudeAndAptitude(this);

            System.Random random = new System.Random();
            healthPoints = random.Next(1, 99);
            socialPoints = random.Next(1, 99);
            moodPoints = random.Next(1, 99);
            //healthPoints = Constants.maxHealth;
            //socialPoints = Constants.maxSocial;
            //moodPoints = Constants.maxMood;
            //IndividualBarsController.SetMaxValues(healthPoints, socialPoints, moodPoints);

            //Se oculta el texto que indica su nombre y el del tiempo restante para una acción
            Transform characterNameChild = transform.Find("CharacterName");
            if (characterNameChild != null)
            {
                TextMeshPro mText = characterNameChild.GetComponent<TextMeshPro>();
                mText.text = characterName;
                mText.enabled = false;
            }
            Transform actionRemainingTimeChild = transform.Find("ActionRemainingTime");
            if (actionRemainingTimeChild != null)
            {
                TextMeshPro mText = actionRemainingTimeChild.GetComponent<TextMeshPro>();
                mText.text = actionRemainingTime.ToString() + "s";
                mText.enabled = false;
            }
            //Se crea un nuevo CharacterAction, con éste personaje y script, y acción vacía
            CharacterActionsController.charActionPairs.Add(new CharacterAction { Character = gameObject, CharacterScript = this, Status = CharacterActionStatus.Free, LastSelected = false, RefusePlayed = false });
        }

        /// <summary>
        /// Muestra el tiempo que queda para terminar una acción
        /// </summary>
        public void ShowActionRemainingTime()
        {
            Transform actionRemainingTimeChild = transform.Find("ActionRemainingTime");
            TextMeshPro mTextActionRemainingTime = actionRemainingTimeChild.GetComponent<TextMeshPro>();
            mTextActionRemainingTime.text = actionRemainingTime.ToString() + "s";
            mTextActionRemainingTime.enabled = true;
        }

        /// <summary>
        /// Oculta el tiempo que queda para terminar una acción
        /// </summary>
        public void HideActionRemainingTime()
        {
            Transform actionRemainingTimeChild = transform.Find("ActionRemainingTime");
            TextMeshPro mTextActionRemainingTime = actionRemainingTimeChild.GetComponent<TextMeshPro>();
            mTextActionRemainingTime.enabled = false;
        }

        /// <summary>
        /// Función que se ejecuta al clicar sobre un personaje
        /// </summary>
        void OnMouseDown()
        {
            //Comprueba si hay una acción doble seleccionada y si se espera por el personaje que tiene que realizar la acción pasiva.
            //En caso afirmativo, asigna la acción pasiva al personaje. Si no, se deselecciona el personaje anterior y se selecciona el nuevo, que queda a la espera de una acción.
            CharacterAction doubleAction = CharacterActionsController.GetLastWaitingAction();
            if (doubleAction == null)
            {
                CharacterActionsController.UnselectLastCharacter();
                CharacterAction charAction = CharacterActionsController.GetCharacterActionOfCharacter(transform);
                charAction.LastSelected = true;
                isSelected = true;
            }
            else
            {
                if (doubleAction.Character != gameObject)
                {
                    doubleAction.SecondaryCharacter = gameObject;
                    CharacterAction secondaryCharacterAction = CharacterActionsController.GetCharacterActionOfSecondaryCharacter(doubleAction);
                    if (secondaryCharacterAction.Status == CharacterActionStatus.Free)
                    {
                        doubleAction.Status = CharacterActionStatus.MovingToActionLocation;
                        CharacterAction charAction = CharacterActionsController.GetCharacterActionOfCharacter(transform);
                        charAction.Action = doubleAction.SecondaryAction;
                        charAction.ObjectToGo = GameObject.Find(doubleAction.SecondaryAction.DestinationName);
                        charAction.Status = CharacterActionStatus.MovingToActionLocation;
                        charAction.CharacterScript.isSelected = true;
                    }
                }
            }
            GameObject characters = GameObject.Find("Characters");
            
            foreach (Transform child in characters.transform)
            {
                Transform childSelectionSymbol = child.transform.Find("SelectionSymbol");
                SpriteRenderer childSpriteRenderer = childSelectionSymbol.GetComponent<SpriteRenderer>();
                childSpriteRenderer.enabled = false;
                individualBarsHUD.enabled = false;
            }
            if (CharacterActionsController.GetCharacterActionOfCharacter(transform).Status != CharacterActionStatus.DoingAction
                && CharacterActionsController.GetCharacterActionOfCharacter(transform).Status != CharacterActionStatus.MovingToActionLocation
                && CharacterActionsController.GetCharacterActionOfCharacter(transform).Status != CharacterActionStatus.ReturningFromActionLocation)
            {
                Transform selectionSymbol = transform.Find("SelectionSymbol");
                SpriteRenderer spriteRenderer = selectionSymbol.GetComponent<SpriteRenderer>();
                spriteRenderer.enabled = true;
            }
            //IndividualBarsController.ShowValues(healthPoints, socialPoints, moodPoints);
            GameObject nameGameObject = individualBarsHUD.transform.Find("IndividualBars/Nombre").gameObject;
            nameGameObject.GetComponent<TextMeshProUGUI>().SetText(characterName);
            individualBarsHUD.enabled = true;
        }

        /// <summary>
        /// Al pasar el ratón por encima del personaje, se muestra su nombre
        /// </summary>
        private void OnMouseOver()
        {
            Transform characterNameChild = transform.Find("CharacterName");
            TextMeshPro mTextCharacterName = characterNameChild.GetComponent<TextMeshPro>();
            mTextCharacterName.enabled = true;
        }

        /// <summary>
        /// Al quitar el ratón de encima del personaje, se oculta su nombre
        /// </summary>
        private void OnMouseExit()
        {
            Transform characterNameChild = transform.Find("CharacterName");
            TextMeshPro mTextCharacterName = characterNameChild.GetComponent<TextMeshPro>();
            mTextCharacterName.enabled = false;
        }

        /// <summary>
        /// Cuando un personaje se choca con el collider del objeto hacia el que tiene que ir, comienza la acción
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag != "Player")
            {
                CharacterAction charAction = CharacterActionsController.GetCharacterActionOfCharacter(transform);
                if (charAction != null && collision.gameObject == charAction.ObjectToGo)
                {
                    if (charAction.Status == CharacterActionStatus.MovingToActionLocation)
                    {
                        charAction.Status = CharacterActionStatus.DoingAction;
                        charAction.Action.StartAction(this);
                    }
                }
            }
        }

        /// <summary>
        /// Mueve un poco al personaje hacia la posición indicada.
        /// </summary>
        /// <param name="position">Posición a la que el personaje debe moverse</param>
        public void GoToPosition(Vector3 position)
        {
            //TODO Se desactiva constantemente
            //individualBarsHUD.enabled = false;
            transform.Find("SelectionSymbol").GetComponent<SpriteRenderer>().enabled = false;
            Vector3 targetPosition = new Vector3(position.x, transform.position.y, position.z);
            Quaternion targetRotation = Quaternion.LookRotation(position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Constants.characterRotationSpeed * Time.deltaTime);
            float step = Constants.characterSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            running = true;
        }

        /// <summary>
        /// Marca al personaje como muerto
        /// </summary>
        public void Die()
        {
            dead = true;
            deathTime = DateTime.Now;
            CharacterAction charAction = CharacterActionsController.GetCharacterActionOfCharacter(transform);
            charAction.Status = CharacterActionStatus.Dead;

            animator.SetBool("running", false);
            animator.SetBool("cold", false);
            animator.SetBool("pickup", false);
            animator.SetBool("rest", false);
            animator.SetBool("die", true);
        }

        /// <summary>
        /// Llamado cada frame, detecta el estado de la acción que está haciendo el usuario y el tiempo transcurrido desde que la comenzó
        /// </summary>
        void Update()
        {
            DateTime current = DateTime.Now;
            //Obtiene el CharacterAction del personaje
            CharacterAction charAction = CharacterActionsController.GetCharacterActionOfCharacter(transform);

            //Calcula el tiempo transcurrido desde la muerte del personaje. Si ha pasado más del tiempo establecido en "millisecondsCharacterRemainsDead", éste desaparece
            double elapsedTimeSinceDeath = ((TimeSpan)(current - deathTime)).TotalMilliseconds;
            if (elapsedTimeSinceDeath >= Constants.millisecondsCharacterRemainsDead && gameObject.activeSelf && dead)
            {
                gameObject.SetActive(false);
            }         
            
            if (charAction != null && charAction.Action != null && isSelected)
            {
                //Mientras el estado sea "MovingToActionLocation", hace una llamada por frame a "GoToPosition()"
                if (charAction.Status == CharacterActionStatus.MovingToActionLocation)
                {
                    charAction.RefusePlayed = false;
                    GoToPosition(charAction.ObjectToGo.transform.position);
                }
                else if (charAction.Status == CharacterActionStatus.Refusing)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("refuse"))
                    {
                        charAction.RefusePlayed = true;
                    }
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle") && charAction.RefusePlayed)
                    {
                        charAction.Status = CharacterActionStatus.MovingToActionLocation;
                    }                    
                }
                //Mientras el estado sea "DoingAction", hace una llamada por frame a "MakeAction()"
                else if (charAction.Status == CharacterActionStatus.DoingAction)
                {
                    running = false;
                    charAction.Action.MakeAction(this);
                }
                //Si el personaje ha terminado su acción y debe volver al centro, se mueve hasta que está cerca de la posición central.
                //Una vez allí, vuelve a quedar libre para otras actividades
                else if (charAction.Status == CharacterActionStatus.ReturningFromActionLocation)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("run"))
                    {
                        if (Utils.Utils.PositionsAreNear(transform.position, centerPosition))
                        {
                            charAction.Status = CharacterActionStatus.Free;
                            running = false;
                            CharacterActionsController.FinishAction(transform);
                        }
                        else
                        {
                            GoToPosition(centerPosition);
                            running = true;
                        }
                    }
                }
            }
            animator.SetBool("running", running);
        }
    }
}