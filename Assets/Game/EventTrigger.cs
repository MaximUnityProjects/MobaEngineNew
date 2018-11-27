using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Вызывает глобальные события вроде клика мыши или смерти героя;
/// </summary>
public sealed class EventTrigger : MonoBehaviour {

    public delegate void MethodContainer();

    public static event MethodContainer OnMouseLeftClick;
    public static event MethodContainer OnMouseRightClick;

    public static Vector3 cursor2D = Vector3.zero;
    public static Vector3 cursor3D;
    /// <summary>
    /// Точка приземления луча выпущенного из камеры. null если кликнули в пустоту
    /// </summary>
    public static Vector3? cursorRaycast;

    /// <summary>
    /// Юнит на которую наведена камера
    /// </summary>



    private void Update() {
        cursorRaycast = GetCursorRaycast(cursor2D);
    }


    private void OnGUI() {
        // мыш
        var e = Event.current;
        cursor2D = e.mousePosition;
        cursor2D.y = Screen.height - cursor2D.y;

        if (OnMouseLeftClick != null && e.type == EventType.MouseDown && e.button == 0) OnMouseLeftClick();
        if (OnMouseRightClick != null && e.type == EventType.MouseDown && e.button == 1) OnMouseRightClick();

        // клава



    }


    static Vector3? GetCursorRaycast(Vector3 cursor2D) {
        RaycastHit hit;
        return Physics.Raycast(Camera.main.ScreenPointToRay(cursor2D), out hit) ? (Vector3?)hit.point : null;
    }


}
