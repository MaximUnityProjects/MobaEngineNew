using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Assets.Game.Abilitys {
    class Shot : Ability {
        private Coroutine moveBullet;
        private Vector3 target;

        public override void Cast() {

            if (EventTrigger.cursorRaycast != null)
            target = (Vector3)EventTrigger.cursorRaycast;
            var bullet = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), transform.position, transform.rotation);
            bullet.AddComponent<SphereCollider>();
            moveBullet = StartCoroutine(MoveBullet(bullet));

           

        }


        IEnumerator MoveBullet(GameObject bullet) {
            var currentCourotine = moveBullet;
            var time = Time.realtimeSinceStartup - 5;
            var bulletTransform = bullet.transform;

            while (time <= Time.realtimeSinceStartup) {
                var colliders = Physics.OverlapSphere(bulletTransform.position, 0.2f);
                foreach (var col in colliders) {
                    var health = col.GetComponent<Health>();
                    if (health != null) {
                        health.Damade(-10);
                        Destroy(bullet);
                        goto END;
                    }
                }
                //bulletTransform = Vector3.MoveTowards();

                yield return new WaitForFixedUpdate();
            }

            END:;

        }




        void Start() {
            number = AbilityNumber.First;
            mode = AbilityMode.Active;
            abilityTarget = AbilityTarget.EnemyHero;

            GetComponent<Unit>().AddAbility(this);

        }

    }
}
