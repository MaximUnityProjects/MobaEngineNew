using System;
using UnityEngine;

namespace Assets.Game.Abilitys {
    public sealed class EmptyAbility : Ability {

        public override void Cast() {
            throw new NotSupportedException();
        }

        void Start() {
            number = AbilityNumber.Hidden;
            mode = AbilityMode.Passive;
            abilityTarget = AbilityTarget.Self;

            GetComponent<Unit>().AddAbility(this);

        }
    }
}
