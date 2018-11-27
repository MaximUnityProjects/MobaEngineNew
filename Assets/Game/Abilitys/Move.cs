using Assets.Game.Navigation;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace Assets.Game.Abilitys {
    /// <summary>
    /// Класс отвечающий за движение персонажа
    /// </summary>
    public sealed class Move : Ability {
        enum UpdateMode { Update, FixedUpdate }
        private readonly Quaternion up = new Quaternion(0, 0, 0, 1);
        private Vector3? target;
        private Coroutine prevCoro = null;
        private Quaternion rotate = new Quaternion(0, 0, 0, 1), vertical = new Quaternion(0, 0, 0, 1);
        private Path path;

        [SerializeField] private float maxSpeed = 5f, height = 1, timeToDespair, stopDistanse = 0.2f;
        [Space]
        [SerializeField] private bool fixVertical;
        [SerializeField] [Range(0, 1)] private float verticalLerpTime = 0.2f;
        [Space]
        [SerializeField] private bool rotateToMovement = true;
        [SerializeField] [Range(0, 1)] private float rotateLerpTime = 0.2f;
        [Space]
        [SerializeField] private UpdateMode updateMode = UpdateMode.FixedUpdate;


        [HideInInspector] public float speed;
        [HideInInspector] public Vector3 speedVector;
        private Vector3 oldPosition;

        IEnumerator SpeedTest(bool mode) {
            speedVector = transform.position - oldPosition;
            speed = speedVector.magnitude / (mode ? Time.fixedDeltaTime : Time.deltaTime);
            print(speed);
            oldPosition = transform.position;
            yield break;
        }

        private void OnEnable() {
            oldPosition = transform.position;
            Enable();
            EventTrigger.OnMouseRightClick += Cast;
            ContinueMove();
        }

        private void OnDisable() {
            Disable();
            EventTrigger.OnMouseRightClick -= Cast;
            PauseMove();
        }

        public override void Cast() {
            StartMove(EventTrigger.cursorRaycast);
        }

        void StartMove(Vector3? target) {
            this.target = target;
            ContinueMove();
        }
        void PauseMove() {
            if (path != null) path.OnFinish -= StopMove;
            if (prevCoro != null) StopCoroutine(prevCoro);
            path = null;
        }
        void ContinueMove() {
            if (target != null) {
                if (prevCoro != null) StopCoroutine(prevCoro);
                prevCoro = StartCoroutine(Moving());
            }
        }
        void StopMove() {
            PauseMove();
            target = null;
        }


        /// <summary>
        /// Перемещение по прямой
        /// </summary>
        IEnumerator Moving() {
            path = Navigator.GetPath();
            if (!path.GeneratePath(transform.position, (Vector3)target)) {
                throw new Exception("Невозможно проложить путь");
            }
            path.stopDistanse = stopDistanse;
            var mode = updateMode == UpdateMode.FixedUpdate;
            var delta = mode ? Time.fixedDeltaTime : Time.deltaTime;
            path.OnFinish += StopMove;
            while (true) {
                var currentPos = transform.position;
                var moveVector = (-currentPos + path.GetNextPoint(currentPos)).normalized;
                moveVector.y = 0;
                var floor = GetFloor(currentPos + moveVector * maxSpeed * delta);
                vertical = fixVertical ? up : Quaternion.Lerp(vertical, Quaternion.FromToRotation(Vector3.up, floor.normal), verticalLerpTime);
                if (rotateToMovement) rotate = Quaternion.Lerp(rotate, Quaternion.LookRotation(moveVector), rotateLerpTime);
                transform.position = floor.point;
                transform.rotation = vertical * rotate;
                yield return mode ? new WaitForFixedUpdate() : null;
            }
        }



        /// <summary>
        /// Прижимает позицию к нижнему коллайдеру (земле)
        /// </summary>
        private RaycastHit GetFloor(Vector3 position) {
            RaycastHit hit;
            Physics.Raycast(position + Vector3.up * height, Vector3.down, out hit, float.MaxValue, ~LayerMask.GetMask("Unit"));
            return hit;
        }

    }
}
