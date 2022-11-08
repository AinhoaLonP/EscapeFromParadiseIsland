
namespace Assets.Scripts.Internationalization
{
    class EnglishTextMessages : TextMessages
    {
        public override string HealActionMaleMessText { get => "Oh no! {medico} is such a bad doctor that now {paciente} is dead! He better don't treat anyone else."; set { } }
        public override string HealActionFemaleMessText { get => "Oh no! {medico} is such a bad doctor that now {paciente} is dead! She better don't treat anyone else."; set { } }

        public override string PsychologistTreatActionMaleMessText { get => "{psicologo}'s attempt to treat {paciente} has been so disastrous that {paciente}, plunged into a deep depression, has lost the motivation to do most of the tasks."; set { } }
        public override string PsychologistTreatActionFemaleMessText { get => "{psicologo}'s attempt to treat {paciente} has been so disastrous that {paciente}, plunged into a deep depression, has lost the motivation to do most of the tasks."; set { } }

        public override string PsychologistTreatActionUpgradeMaleText { get => "{paciente} has improved his attitude towards the following activities: "; set { } }
        public override string PsychologistTreatActionUpgradeFemaleText { get => "{paciente} has improved her attitude towards the following activities: "; set { } }

        public override string PsychologistTreatActionDowngradeText { get => "{paciente}'s attitude towards the following activities has worsened: "; set { } }

        public override string TeachActionMaleExpertText { get => "Now {alumno} is an expert in this activity: {actividad}."; set { } }
        public override string TeachActionFemaleExpertText { get => "Now {alumno} is an expert in this activity: {actividad}."; set { } }

        public override string TeachActionVeryGoodText { get => "Now {alumno} is very good at this activity: {actividad}."; set { } }

        public override string TeachActionQuiteGoodText { get => "Now {alumno} is quite good at this activity: {actividad}."; set { } }

        public override string TeachActionALittleBetterText { get => "Now {alumno} is a little better at this activity: {actividad}"; set { } }

        public override string TeachActionALittleBetterButBadMaleText { get => "Now {alumno} is a little better at this activity, although he's still quite bad: {actividad}"; set { } }
        public override string TeachActionALittleBetterButBadFemaleText { get => "Now {alumno} is a little better at this activity, although she's still quite bad: {actividad}"; set { } }

        public override string TeachActionBadMaleTeacherText { get => "{maestro} is not a good teacher, so {alumno} didn't learn anything new with him."; set { } }
        public override string TeachActionBadFemaleTeacherText { get => "{maestro} is not a good teacher, so {alumno} didn't learn anything new with her."; set { } }

        public override string TeachActionMaleMessText { get => "{profesor} is such a bad teacher that {alumno} has learned to do things in the wrong way. Now {alumno} is likely to cause some accidents."; set { } }
        public override string TeachActionFemaleMessText { get => "{profesor} is such a bad teacher that {alumno} has learned to do things in the wrong way. Now {alumno} is likely to cause some accidents."; set { } }

        public override string BuildBoatActionMessMaleText { get => "{characterName} is so clumsy that he bumped into the boat and knocked over part of it! Now the others will have to work harder in order to repair the damage."; set { } }
        public override string BuildBoatActionMessFemaleText { get => "{characterName} is so clumsy that she bumped into the boat and knocked over part of it! Now the others will have to work harder in order to repair the damage."; set { } }

        public override string BuildRefugeActionMessMaleText { get => "{characterName} is so clumsy that he bumped into the refuge and knocked over part of it! Now the others will have to work harder in order to repair the damage."; set { } }
        public override string BuildRefugeActionMessFemaleText { get => "{characterName} is so clumsy that she bumped into the refuge and knocked over part of it! Now the others will have to work harder in order to repair the damage."; set { } }

        public override string PickFoodActionMessText { get => "Oh no! {characterName} has collected some poisonous berries and islanders' health has taken a turn for the worse!"; set { } }

        public override string PickResourcesActionMessMaleText { get => "{characterName} sprained an ankle while collecting materials. He better spend some time resting."; set { } }
        public override string PickResourcesActionMessFemaleText { get => "{characterName} sprained an ankle while collecting materials. She better spend some time resting."; set { } }

        public override string PickWaterActionMessMaleText { get => "Oh no! {characterName} has spilled the collected water! He better do something else."; set { } }
        public override string PickWaterActionMessFemaleText { get => "Oh no! {characterName} has spilled the collected water! She better do something else."; set { } }

        public override string DieText { get => "{characterName} has died due to poor health."; set { } }
        public override string TooMuchSunText { get => "The sun is too hot to be outside for so many hours. They better build a shelter fast or they could get heat stroke."; set { } }

        public override string ActionNameForMessagesMedicine { get => "Medicine"; set { } }
        public override string ActionNameForMessagesPsychology { get => "Psychology"; set { } }
        public override string ActionNameForMessagesBeTeacher { get => "Teaching"; set { } }
        public override string ActionNameForMessagesBuildBoat { get => "Build boat"; set { } }
        public override string ActionNameForMessagesBuildRefuge { get => "Build refuge"; set { } }
        public override string ActionNameForMessagesPickFood { get => "Gather food"; set { } }
        public override string ActionNameForMessagesPickResources { get => "Gather materials"; set { } }
        public override string ActionNameForMessagesPickWater { get => "Gather water"; set { } }

        public override string CanBuildBoatText { get => "Now that your castaways have met their basic needs, they can start to build a boat to get off the island."; set { } }

    }
}
