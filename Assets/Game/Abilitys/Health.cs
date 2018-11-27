using System;
using UnityEngine;

namespace Assets.Game.Abilitys {
    /// <summary>
    /// Здоровье и всё что с ним связано
    /// </summary>
    class Health : Ability {
        public enum HealthStatus { Alive, Dead };
        public delegate void Delegate();

        public HealthStatus status { get; private set; }
        public Delegate Die;
        public Delegate Relive;



        [SerializeField] private float maxHp = 100, startHp = 100;
        public float hp { get; private set; }

        public override void Cast() {
            throw new NotSupportedException();
        }

        void Start() {
            number = AbilityNumber.Hidden;
            mode = AbilityMode.Passive;
            abilityTarget = AbilityTarget.Self;

            GetComponent<Unit>().AddAbility(this);

            Die = DefaultDie;
            Relive = DefaultRelive;

            SetHp(startHp);
        }


        public void Damade(float hp) {
            SetHp(this.hp + hp);
        }


        private void SetHp(float hp) {
            this.hp = Mathf.Max(0, Mathf.Min(maxHp, hp));
            if (hp == 0) Die();
        }



        public void DefaultRelive() {
            status = HealthStatus.Alive;
        }

        public void DefaultDie() {
            status = HealthStatus.Dead;
        }


    }
}
