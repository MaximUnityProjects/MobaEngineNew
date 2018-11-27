using UnityEngine;
using UnityEngine.AI;

namespace Assets.Game.Navigation {
    public class Path {
        public Vector3? endPoint;
        public Vector3? nextPoint;
        public Vector3[] pathCorners;
        int pathLenght;
        int step = 0;
        NavMeshPath path = null;
        public float stopDistanse = 0.2f;
        public delegate void MethodContainer();
        public event MethodContainer OnFinish;

        public bool GeneratePath(Vector3 soursePosition, Vector3 targetPosition) {
            NavMeshPath oldPath = null;

            if (path != null) oldPath = path;
            else path = new NavMeshPath();

            var valid = NavMesh.CalculatePath(soursePosition, targetPosition, NavMesh.AllAreas, path);
            if (valid) {
                pathCorners = path.corners;
                pathLenght = pathCorners.Length;
                step = 0;
            }

            else path = oldPath;
            return valid;
        }

        /// <summary>
        /// Возвращает следующую точку поворота
        /// </summary>
        public Vector3 GetNextPoint(Vector3 soursePosition) {
            if ((soursePosition - pathCorners[step]).sqrMagnitude < stopDistanse * stopDistanse) {
                if (step + 1 < pathLenght) step++;
                else if (OnFinish != null) OnFinish();
            }
            return pathCorners[step];
        }


    }
}
