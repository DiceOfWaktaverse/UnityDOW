using System;
using System.Collections;
using System.Globalization;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace DOW
{
    public static class Func
    {
        private static readonly StringBuilder builder = new StringBuilder();
        public static string StrBuilder(params object[] arr)
        {
            builder.Clear();

            var arrCount = arr.Length;
            for(var i = 0; i < arrCount; ++i)
            {
                builder.Append(arr[i]);
            }

            return builder.ToString();
        }
        public static int GetActiveChildCount(Transform parentTr)
        {
            int result = 0;

            foreach (Transform childTr in parentTr)
            {
                if (childTr.gameObject.activeInHierarchy)
                {
                    result++;
                }
            }

            return result;
        }
        public static float Sinusoidal(float curTime, float cycle, float power = 1f)
        {
            var curPos = curTime * 6f / cycle;
            return power * Mathf.Sin(curPos);
        }
        public static IEnumerator CallBackCoroutine(VoidDelegate callback)
        {
            if (callback != null)
                callback();

            yield break;
        }
        public static string GetPath(this eResourcePath path, string fileName = "")
        {
            var value = path switch
            {
                eResourcePath.POPUP => "Prefabs/Popup",
                eResourcePath.DATA => "Data",
                eResourcePath.SOUND => "Sound",
                eResourcePath.ILLUST => "Prefabs/Illust",
                _ => ""
            };

            if (fileName == "")
                return value;

            return StrBuilder(value, "/", fileName);
        }
        #region Time
        public static string TimeString(int sec)
        {
            var day = sec >= Define.Day ? Mathf.FloorToInt(sec / Define.Day) : 0;
            sec -= day * Define.Day;
            var hour = sec >= Define.Hour ? Mathf.FloorToInt(sec / Define.Hour) : 0;
            sec -= hour * Define.Hour;
            var min = sec >= Define.Minute ? Mathf.FloorToInt(sec / Define.Minute) : 0;
            sec -= min * Define.Minute;

            if (sec < 0)
                sec = 0;

            if (day > 0)
                return StrBuilder(day.ToString(), "D ", hour.ToString("D2"), ":", min.ToString("D2"), ":", sec.ToString("D2"));

            if (hour > 0)
                return StrBuilder(hour.ToString("D2"), ":", min.ToString("D2"), ":", sec.ToString("D2"));

            if (min > 0)
                return StrBuilder(min.ToString("D2"), ":", sec.ToString("D2"));

            return StrBuilder("00:", sec.ToString("D2"));
        }
        public static string TimeString(float sec)
        {
            return TimeString(Mathf.FloorToInt(sec));
        }

        public static DateTime TimeStampToDateTime(int _timeStamp)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(_timeStamp).ToLocalTime();
            return dt;
        }
        public static DateTime TimeStampToDateTime(long _timeStamp)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(_timeStamp).ToLocalTime();
            return dt;
        }
        public static long GetDateTimeToTimeStamp()
        {
            var now = DateTime.Now.ToLocalTime();
            var span = (now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            int timestamp = (int)span.TotalSeconds;

            return timestamp;
        }
        #endregion
        #region Between
        public static bool IsBetween(int target, int min, int max)
        {
            if (min > target || max < target)
            {
                return false;
            }
            return true;
        }
        public static bool IsBetween(int target, int min, float max)
        {
            if (min > target || max < target)
            {
                return false;
            }
            return true;
        }
        public static bool IsBetween(int target, float min, int max)
        {
            if (min > target || max < target)
            {
                return false;
            }
            return true;
        }
        public static bool IsBetween(int target, float min, float max)
        {
            if (min > target || max < target)
            {
                return false;
            }
            return true;
        }
        public static bool IsBetween(float target, int min, int max)
        {
            if (min > target || max < target)
            {
                return false;
            }
            return true;
        }
        public static bool IsBetween(float target, int min, float max)
        {
            if (min > target || max < target)
            {
                return false;
            }
            return true;
        }
        public static bool IsBetween(float target, float min, int max)
        {
            if (min > target || max < target)
            {
                return false;
            }
            return true;
        }
        public static bool IsBetween(float target, float min, float max)
        {
            if (min > target || max < target)
            {
                return false;
            }
            return true;
        }
        #endregion
        #region Random
        public static int Random(int min, int max)
        {
            return UnityEngine.Random.Range(min, max);
        }
        public static float Random(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }
        #endregion
        #region Comma
        public static string CommaFromNumber(int item)
        {
            NumberFormatInfo nfi = new CultureInfo("ko-KR", false).NumberFormat;
            nfi.NumberDecimalDigits = 0;
            return item.ToString("N", nfi);
        }
        public static string CommaFromNumber(float item)
        {
            return item.ToString("N");
        }
        public static string CommaFromNumber(double item)
        {
            return item.ToString("N");
        }
        public static string CommaFromMoney(int item)
        {
            return item.ToString("C");
        }
        public static string CommaFromMoney(float item)
        {
            return item.ToString("C");
        }
        public static string CommaFromMoney(double item)
        {
            return item.ToString("C");
        }
        #endregion
        #region UI

        public static Vector2 WorldToUICanvasPosition(Vector3 worldPos)
        {
            Vector2 screenCanvasPos = Vector2.zero;

            Camera uiCamera = UICanvas.Instance.GetCamera();
            if (uiCamera != null)
            {
                Vector3 viewPos = uiCamera.WorldToViewportPoint(worldPos);
                RectTransform canvasRect = UICanvas.Instance.GetCanvasRectTransform();

                screenCanvasPos = new Vector2(
                    ((viewPos.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
                    ((viewPos.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));
            }

            return screenCanvasPos;
        }

        public static Vector2 WorldToCanvasPosition(Vector3 worldPos)
        {
            Vector2 screenCanvasPos = Vector2.zero;

            Camera camera = Camera.main;
            if (camera != null)
            {
                Vector3 viewPos = camera.WorldToViewportPoint(worldPos);
                RectTransform canvasRect = UICanvas.Instance.GetCanvasRectTransform();

                screenCanvasPos = new Vector2(
                    ((viewPos.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
                    ((viewPos.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));
            }

            return screenCanvasPos;
        }

        #endregion
        #region Childrens
        public static GameObject[] GetChildren(GameObject parent)
        {
            var count = parent.transform.childCount;
            GameObject[] children = new GameObject[count];

            for (int i = 0; i < count; i++)
            {
                children[i] = parent.transform.GetChild(i).gameObject;
            }

            return children;
        }
        public static Transform GetChildrensByName(Transform target, params string[] targets)
        {
            var count = targets.Length;
            for(var i = 0; i < count; ++i)
            {
                if (target == null)
                    break;
                target = target.Find(targets[i]);
            }

            return target;
        }
        public static void RemoveAllChildrens(Transform target)
        {
            var count = target.childCount;

            for(var i = 0; i < count; ++i)
            {
                var cur = target.GetChild(i);
                if (cur != null)
                    UnityEngine.Object.Destroy(cur.gameObject);
            }

            return;
        }
        public static Transform[] GetChildren(Transform parent)
        {
            var count = parent.childCount;
            Transform[] children = new Transform[count];

            for (int i = 0; i < count; i++)
            {
                children[i] = parent.GetChild(i);
            }

            return children;
        }
        public static void SetLayer(Transform parent, int layer)
        {
            if (parent == null)
                return;

            parent.gameObject.layer = layer;
            var count = parent.childCount;

            for (int i = 0; i < count; i++)
            {
                var target = parent.GetChild(i);
                if (target == null)
                    continue;

                SetLayer(target, layer);
            }
        }
        public static void SetLayer(Transform parent, string layerName)
        {
            SetLayer(parent, LayerMask.NameToLayer(layerName));
        }
        public static void SetLayer(GameObject obj, int layer)
        {
            if (obj == null)
                return;

            SetLayer(obj.transform, layer);
        }
        public static void SetLayer(GameObject obj, string layerName)
        {
            if (obj == null)
                return;

            SetLayer(obj.transform, LayerMask.NameToLayer(layerName));
        }
        #endregion
        #region Bezier
        public static float BezierCurve(float start, float end, float curTime, float maxTime) {
            var min = 0f;
            if (start != 0f)
            {
                min = start;
                start = 0;
                end -= min;
            }
            var normalT = curTime / maxTime;
            var normalS = 1 - normalT;
            var startS = start * normalS;
            var endT = end * normalT;
            return min + startS + endT;
        }
        public static float BezierCurve2(float start, float wayPoint, float end, float curTime, float maxTime) {
            var normalT = curTime / maxTime;
            var normalS = 1 - normalT;
            var startS = Mathf.Pow(normalS, 2) * start;
            var wayPointST = 2 * normalS * normalT * wayPoint;
            var endT = Mathf.Pow(normalT, 2) * end;
            return startS + wayPointST + endT;
        }
        public static Vector3 BezierCurveVec3(Vector3 start, Vector3 end, float curTime, float maxTime) {
            var min = Vector3.zero;
            if(start.x != 0 || start.y != 0 || start.z != 0)
            {
                min.x = start.x;
                min.y = start.y;
                min.z = start.z;
                start = Vector3.zero;
                end.x -= min.x;
                end.y -= min.y;
                end.z -= min.z;
            }
            var normalT = curTime / maxTime;
            var normalS = 1 - normalT;
            var startS = new Vector3(start.x * normalS, start.y * normalS, start.z * normalS);
            var endT = new Vector3(end.x * normalT, end.y * normalT, end.z * normalT);
            return new Vector3(min.x + startS.x + endT.x, min.y + startS.y + endT.y, min.z + startS.z + endT.z);
        }
        public static Vector3 BezierCurve2Vec3(Vector3 start, Vector3 wayPoint, Vector3 end, float curTime, float maxTime)
        {
            var min = Vector3.zero;
            if (start.x != 0 || start.y != 0 || start.z != 0)
            {
                min.x = start.x;
                min.y = start.y;
                min.z = start.z;
                start = Vector3.zero;
                wayPoint.x -= min.x;
                wayPoint.y -= min.y;
                wayPoint.z -= min.z;
                end.x -= min.x;
                end.y -= min.y;
                end.z -= min.z;
            }
            var normalT = curTime / maxTime;
            var normalS = 1 - normalT;
            var S = Mathf.Pow(normalS, 2);
            var W = 2 * normalS * normalT;
            var T = Mathf.Pow(normalT, 2);
            var startS = new Vector3(start.x * S, start.y * S, start.z * S);
            var wayPointST = new Vector3(wayPoint.x * W, wayPoint.y * W, wayPoint.z * W);
            var endT = new Vector3(end.x * T, end.y * T, end.z * T);
            return new Vector3(min.x + startS.x + wayPointST.x + endT.x, min.y + startS.y + wayPointST.y + endT.y, min.x + startS.z + wayPointST.z + endT.z);
        }
        public static Vector2 BezierCurve3Vec2(Vector2 start, Vector2 wayPoint1, Vector2 wayPoint2, Vector2 end, float curTime, float maxTime)
        {
            var min = Vector2.zero;
            if (start.x != 0 || start.y != 0)
            {
                min.x = start.x;
                min.y = start.y;
                start = Vector2.zero;
                wayPoint1.x -= min.x;
                wayPoint1.y -= min.y;
                wayPoint2.x -= min.x;
                wayPoint2.y -= min.y;
                end.x -= min.x;
                end.y -= min.y;
            }
            var normalT = curTime / maxTime;
            var normalS = 1 - normalT;
            var S = Mathf.Pow(normalS, 3);
            var W1 = 3 * Mathf.Pow(normalS, 2) * normalT;
            var W2 = 3 * Mathf.Pow(normalT, 2) * normalS;
            var T = Mathf.Pow(normalT, 3);
            var startS = new Vector2(S * start.x, S * start.y);
            var wayPointST = new Vector2(W1 * wayPoint1.x, W1 * wayPoint1.y);
            var wayPointTS = new Vector2(W2 * wayPoint2.x, W2 * wayPoint2.y);
            var endT = new Vector2(T * end.x, T * end.y);
            return new Vector2(min.x + startS.x + wayPointST.x + wayPointTS.x + endT.x, min.y + startS.y + wayPointST.y + wayPointTS.y + endT.y);
        }
        public static Vector3 BezierCurve3Vec3(Vector3 start, Vector3 wayPoint1, Vector3 wayPoint2, Vector3 end, float curTime, float maxTime)
        {
            var min = Vector3.zero;
            if (start.x != 0 || start.y != 0 || start.z != 0)
            {
                min.x = start.x;
                min.y = start.y;
                min.z = start.z;
                start = Vector3.zero;
                wayPoint1.x -= min.x;
                wayPoint1.y -= min.y;
                wayPoint1.z -= min.z;
                wayPoint2.x -= min.x;
                wayPoint2.y -= min.y;
                wayPoint2.z -= min.z;
                end.x -= min.x;
                end.y -= min.y;
                end.z -= min.z;
            }
            var normalT = curTime / maxTime;
            var normalS = 1 - normalT;
            var S = Mathf.Pow(normalS, 3);
            var W1 = 3 * Mathf.Pow(normalS, 2) * normalT;
            var W2 = 3 * Mathf.Pow(normalT, 2) * normalS;
            var T = Mathf.Pow(normalT, 3);
            var startS = new Vector3(S * start.x, S * start.y, S * start.z);
            var wayPointST = new Vector3(W1 * wayPoint1.x, W1 * wayPoint1.y, W1 * wayPoint1.z);
            var wayPointTS = new Vector3(W2 * wayPoint2.x, W2 * wayPoint2.y, W2 * wayPoint2.z);
            var endT = new Vector3(T * end.x, T * end.y, T * end.z);
            return new Vector3(min.x + startS.x + wayPointST.x + wayPointTS.x + endT.x, min.y + startS.y + wayPointST.y + wayPointTS.y + endT.y, min.z + startS.z + wayPointST.z + wayPointTS.z + endT.z);
        }
        public static float BezierCurveSpeed(float start, float end, float curTime, float maxTime, Vector4 bezierSpeedVec)
        {
            var min = 0f;
            if (start != 0f)
            {
                min = start;
                start = 0;
                end -= min;
            }

            var normalT = BezierCurve3Vec2(new Vector2(0, 0), new Vector2(bezierSpeedVec.x, bezierSpeedVec.y), new Vector2(bezierSpeedVec.z, bezierSpeedVec.w), new Vector2(1, 1), curTime, maxTime).y;
            var normalS = 1 - normalT;
            var startS = start * normalS;
            var endT = end * normalT;
            return min + startS + endT;
        }
        public static float BezierCurve2Speed(float start, float wayPoint, float end, float curTime, float maxTime, Vector4 bezierSpeedVec) {
            var normalT = BezierCurve3Vec2(new Vector2(0, 0), new Vector2(bezierSpeedVec.x, bezierSpeedVec.y), new Vector2(bezierSpeedVec.z, bezierSpeedVec.w), new Vector2(1, 1), curTime, maxTime).y;
            var normalS = 1 - normalT;
            var startS = Mathf.Pow(normalS, 2) * start;
            var wayPointST = 2 * normalS * normalT * wayPoint;
            var endT = Mathf.Pow(normalT, 2) * end;
            return startS + wayPointST + endT;
        }
        #endregion
        #region Casts
        public static RaycastHit2D RayCast(Vector2 rayOriginPoint, Vector2 rayDirection, float rayDistance, LayerMask mask, Color color, bool drawGizmo = false)
        {
            if (drawGizmo)
            {
                Debug.DrawRay(rayOriginPoint, rayDirection * rayDistance, color);
            }
            return Physics2D.Raycast(rayOriginPoint, rayDirection, rayDistance, mask);
        }
        public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float length, LayerMask mask, Color color, bool drawGizmo = false)
        {
            if (drawGizmo)
            {
                Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

                Vector3[] points = new Vector3[8];

                float halfSizeX = size.x / 2f;
                float halfSizeY = size.y / 2f;

                points[0] = rotation * (origin + (Vector2.left * halfSizeX) + (Vector2.up * halfSizeY)); // top left
                points[1] = rotation * (origin + (Vector2.right * halfSizeX) + (Vector2.up * halfSizeY)); // top right
                points[2] = rotation * (origin + (Vector2.right * halfSizeX) - (Vector2.up * halfSizeY)); // bottom right
                points[3] = rotation * (origin + (Vector2.left * halfSizeX) - (Vector2.up * halfSizeY)); // bottom left

                points[4] = rotation * ((origin + Vector2.left * halfSizeX + Vector2.up * halfSizeY) + length * direction); // top left
                points[5] = rotation * ((origin + Vector2.right * halfSizeX + Vector2.up * halfSizeY) + length * direction); // top right
                points[6] = rotation * ((origin + Vector2.right * halfSizeX - Vector2.up * halfSizeY) + length * direction); // bottom right
                points[7] = rotation * ((origin + Vector2.left * halfSizeX - Vector2.up * halfSizeY) + length * direction); // bottom left

                Debug.DrawLine(points[0], points[1], color);
                Debug.DrawLine(points[1], points[2], color);
                Debug.DrawLine(points[2], points[3], color);
                Debug.DrawLine(points[3], points[0], color);

                Debug.DrawLine(points[4], points[5], color);
                Debug.DrawLine(points[5], points[6], color);
                Debug.DrawLine(points[6], points[7], color);
                Debug.DrawLine(points[7], points[4], color);

                Debug.DrawLine(points[0], points[4], color);
                Debug.DrawLine(points[1], points[5], color);
                Debug.DrawLine(points[2], points[6], color);
                Debug.DrawLine(points[3], points[7], color);

            }
            return Physics2D.BoxCast(origin, size, angle, direction, length, mask);
        }
        public static RaycastHit2D MonoRayCastNonAlloc(RaycastHit2D[] array, Vector2 rayOriginPoint, Vector2 rayDirection, float rayDistance, LayerMask mask, Color color, bool drawGizmo = false)
        {
            if (drawGizmo)
            {
                Debug.DrawRay(rayOriginPoint, rayDirection * rayDistance, color);
            }
            if (Physics2D.RaycastNonAlloc(rayOriginPoint, rayDirection, array, rayDistance, mask) > 0)
            {
                return array[0];
            }
            return new RaycastHit2D();
        }
        public static RaycastHit Raycast3D(Vector3 rayOriginPoint, Vector3 rayDirection, float rayDistance, LayerMask mask, Color color, bool drawGizmo = false)
        {
            if (drawGizmo)
            {
                Debug.DrawRay(rayOriginPoint, rayDirection * rayDistance, color);
            }
            RaycastHit hit;
            Physics.Raycast(rayOriginPoint, rayDirection, out hit, rayDistance, mask);
            return hit;
        }
        #endregion
        #region DebugDraw
        public static void DrawGizmoArrow(Vector3 origin, Vector3 direction, Color color, float arrowHeadLength = 3f, float arrowHeadAngle = 25f)
        {
            Gizmos.color = color;
            Gizmos.DrawRay(origin, direction);

            DrawArrowEnd(true, origin, direction, color, arrowHeadLength, arrowHeadAngle);
        }
        public static void DebugDrawArrow(Vector3 origin, Vector3 direction, Color color, float arrowHeadLength = 0.2f, float arrowHeadAngle = 35f)
        {
            Debug.DrawRay(origin, direction, color);

            DrawArrowEnd(false, origin, direction, color, arrowHeadLength, arrowHeadAngle);
        }
        public static void DebugDrawArrow(Vector3 origin, Vector3 direction, Color color, float arrowLength, float arrowHeadLength = 0.20f, float arrowHeadAngle = 35.0f)
        {
            Debug.DrawRay(origin, direction * arrowLength, color);

            DrawArrowEnd(false, origin, direction * arrowLength, color, arrowHeadLength, arrowHeadAngle);
        }
        public static void DebugDrawCross(Vector3 spot, float crossSize, Color color)
        {
            Vector3 tempOrigin = Vector3.zero;
            Vector3 tempDirection = Vector3.zero;

            tempOrigin.x = spot.x - crossSize / 2;
            tempOrigin.y = spot.y - crossSize / 2;
            tempOrigin.z = spot.z;
            tempDirection.x = 1;
            tempDirection.y = 1;
            tempDirection.z = 0;
            Debug.DrawRay(tempOrigin, tempDirection * crossSize, color);

            tempOrigin.x = spot.x - crossSize / 2;
            tempOrigin.y = spot.y + crossSize / 2;
            tempOrigin.z = spot.z;
            tempDirection.x = 1;
            tempDirection.y = -1;
            tempDirection.z = 0;
            Debug.DrawRay(tempOrigin, tempDirection * crossSize, color);
        }
        private static void DrawArrowEnd(bool drawGizmos, Vector3 arrowEndPosition, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 40.0f)
        {
            if (direction == Vector3.zero)
            {
                return;
            }
            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(arrowHeadAngle, 0, 0) * Vector3.back;
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(-arrowHeadAngle, 0, 0) * Vector3.back;
            Vector3 up = Quaternion.LookRotation(direction) * Quaternion.Euler(0, arrowHeadAngle, 0) * Vector3.back;
            Vector3 down = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -arrowHeadAngle, 0) * Vector3.back;
            if (drawGizmos)
            {
                Gizmos.color = color;
                Gizmos.DrawRay(arrowEndPosition + direction, right * arrowHeadLength);
                Gizmos.DrawRay(arrowEndPosition + direction, left * arrowHeadLength);
                Gizmos.DrawRay(arrowEndPosition + direction, up * arrowHeadLength);
                Gizmos.DrawRay(arrowEndPosition + direction, down * arrowHeadLength);
            }
            else
            {
                Debug.DrawRay(arrowEndPosition + direction, right * arrowHeadLength, color);
                Debug.DrawRay(arrowEndPosition + direction, left * arrowHeadLength, color);
                Debug.DrawRay(arrowEndPosition + direction, up * arrowHeadLength, color);
                Debug.DrawRay(arrowEndPosition + direction, down * arrowHeadLength, color);
            }
        }
        public static void DrawHandlesBounds(Bounds bounds, Color color)
        {
#if UNITY_EDITOR
            Vector3 boundsCenter = bounds.center;
            Vector3 boundsExtents = bounds.extents;

            Vector3 v3FrontTopLeft = new Vector3(boundsCenter.x - boundsExtents.x, boundsCenter.y + boundsExtents.y, boundsCenter.z - boundsExtents.z);  // Front top left corner
            Vector3 v3FrontTopRight = new Vector3(boundsCenter.x + boundsExtents.x, boundsCenter.y + boundsExtents.y, boundsCenter.z - boundsExtents.z);  // Front top right corner
            Vector3 v3FrontBottomLeft = new Vector3(boundsCenter.x - boundsExtents.x, boundsCenter.y - boundsExtents.y, boundsCenter.z - boundsExtents.z);  // Front bottom left corner
            Vector3 v3FrontBottomRight = new Vector3(boundsCenter.x + boundsExtents.x, boundsCenter.y - boundsExtents.y, boundsCenter.z - boundsExtents.z);  // Front bottom right corner
            Vector3 v3BackTopLeft = new Vector3(boundsCenter.x - boundsExtents.x, boundsCenter.y + boundsExtents.y, boundsCenter.z + boundsExtents.z);  // Back top left corner
            Vector3 v3BackTopRight = new Vector3(boundsCenter.x + boundsExtents.x, boundsCenter.y + boundsExtents.y, boundsCenter.z + boundsExtents.z);  // Back top right corner
            Vector3 v3BackBottomLeft = new Vector3(boundsCenter.x - boundsExtents.x, boundsCenter.y - boundsExtents.y, boundsCenter.z + boundsExtents.z);  // Back bottom left corner
            Vector3 v3BackBottomRight = new Vector3(boundsCenter.x + boundsExtents.x, boundsCenter.y - boundsExtents.y, boundsCenter.z + boundsExtents.z);  // Back bottom right corner


            Handles.color = color;

            Handles.DrawLine(v3FrontTopLeft, v3FrontTopRight);
            Handles.DrawLine(v3FrontTopRight, v3FrontBottomRight);
            Handles.DrawLine(v3FrontBottomRight, v3FrontBottomLeft);
            Handles.DrawLine(v3FrontBottomLeft, v3FrontTopLeft);

            Handles.DrawLine(v3BackTopLeft, v3BackTopRight);
            Handles.DrawLine(v3BackTopRight, v3BackBottomRight);
            Handles.DrawLine(v3BackBottomRight, v3BackBottomLeft);
            Handles.DrawLine(v3BackBottomLeft, v3BackTopLeft);

            Handles.DrawLine(v3FrontTopLeft, v3BackTopLeft);
            Handles.DrawLine(v3FrontTopRight, v3BackTopRight);
            Handles.DrawLine(v3FrontBottomRight, v3BackBottomRight);
            Handles.DrawLine(v3FrontBottomLeft, v3BackBottomLeft);
#endif
        }
        public static void DrawSolidRectangle(Vector3 position, Vector3 size, Color borderColor, Color solidColor)
        {
#if UNITY_EDITOR
            Vector3 halfSize = size / 2f;

            Vector3[] verts = new Vector3[4];
            verts[0] = new Vector3(halfSize.x, halfSize.y, halfSize.z);
            verts[1] = new Vector3(-halfSize.x, halfSize.y, halfSize.z);
            verts[2] = new Vector3(-halfSize.x, -halfSize.y, halfSize.z);
            verts[3] = new Vector3(halfSize.x, -halfSize.y, halfSize.z);
            Handles.DrawSolidRectangleWithOutline(verts, solidColor, borderColor);

#endif
        }
        public static void DrawGizmoPoint(Vector3 position, float size, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawWireSphere(position, size);
        }
        public static void DrawCube(Vector3 position, Color color, Vector3 size)
        {
            Vector3 halfSize = size / 2f;

            Vector3[] points = new Vector3[]
            {
                position + new Vector3(halfSize.x,halfSize.y,halfSize.z),
                position + new Vector3(-halfSize.x,halfSize.y,halfSize.z),
                position + new Vector3(-halfSize.x,-halfSize.y,halfSize.z),
                position + new Vector3(halfSize.x,-halfSize.y,halfSize.z),
                position + new Vector3(halfSize.x,halfSize.y,-halfSize.z),
                position + new Vector3(-halfSize.x,halfSize.y,-halfSize.z),
                position + new Vector3(-halfSize.x,-halfSize.y,-halfSize.z),
                position + new Vector3(halfSize.x,-halfSize.y,-halfSize.z),
            };

            Debug.DrawLine(points[0], points[1], color);
            Debug.DrawLine(points[1], points[2], color);
            Debug.DrawLine(points[2], points[3], color);
            Debug.DrawLine(points[3], points[0], color);
        }
        public static void DrawGizmoCube(Transform transform, Vector3 offset, Vector3 cubeSize, bool wireOnly)
        {
            Matrix4x4 rotationMatrix = transform.localToWorldMatrix;
            Gizmos.matrix = rotationMatrix;
            if (wireOnly)
            {
                Gizmos.DrawWireCube(offset, cubeSize);
            }
            else
            {
                Gizmos.DrawCube(offset, cubeSize);
            }
        }
        public static void DrawGizmoRectangle(Vector2 center, Vector2 size, Color color)
        {
            Gizmos.color = color;

            Vector3 v3TopLeft = new Vector3(center.x - size.x / 2, center.y + size.y / 2, 0);
            Vector3 v3TopRight = new Vector3(center.x + size.x / 2, center.y + size.y / 2, 0); ;
            Vector3 v3BottomRight = new Vector3(center.x + size.x / 2, center.y - size.y / 2, 0); ;
            Vector3 v3BottomLeft = new Vector3(center.x - size.x / 2, center.y - size.y / 2, 0); ;

            Gizmos.DrawLine(v3TopLeft, v3TopRight);
            Gizmos.DrawLine(v3TopRight, v3BottomRight);
            Gizmos.DrawLine(v3BottomRight, v3BottomLeft);
            Gizmos.DrawLine(v3BottomLeft, v3TopLeft);
        }
        public static void DrawGizmoRectangle(Vector2 center, Vector2 size, Matrix4x4 rotationMatrix, Color color)
        {
            GL.PushMatrix();

            Gizmos.color = color;

            Vector3 v3TopLeft = rotationMatrix * new Vector3(center.x - size.x / 2, center.y + size.y / 2, 0);
            Vector3 v3TopRight = rotationMatrix * new Vector3(center.x + size.x / 2, center.y + size.y / 2, 0); ;
            Vector3 v3BottomRight = rotationMatrix * new Vector3(center.x + size.x / 2, center.y - size.y / 2, 0); ;
            Vector3 v3BottomLeft = rotationMatrix * new Vector3(center.x - size.x / 2, center.y - size.y / 2, 0); ;


            Gizmos.DrawLine(v3TopLeft, v3TopRight);
            Gizmos.DrawLine(v3TopRight, v3BottomRight);
            Gizmos.DrawLine(v3BottomRight, v3BottomLeft);
            Gizmos.DrawLine(v3BottomLeft, v3TopLeft);
            GL.PopMatrix();
        }
        public static void DrawRectangle(Rect rectangle, Color color)
        {
            Vector3 pos = new Vector3(rectangle.x + rectangle.width / 2, rectangle.y + rectangle.height / 2, 0.0f);
            Vector3 scale = new Vector3(rectangle.width, rectangle.height, 0.0f);

            DrawRectangle(pos, color, scale);
        }
        public static void DrawRectangle(Vector3 position, Color color, Vector3 size)
        {
            Vector3 halfSize = size / 2f;

            Vector3[] points = new Vector3[]
            {
                position + new Vector3(halfSize.x,halfSize.y,halfSize.z),
                position + new Vector3(-halfSize.x,halfSize.y,halfSize.z),
                position + new Vector3(-halfSize.x,-halfSize.y,halfSize.z),
                position + new Vector3(halfSize.x,-halfSize.y,halfSize.z),
            };

            Debug.DrawLine(points[0], points[1], color);
            Debug.DrawLine(points[1], points[2], color);
            Debug.DrawLine(points[2], points[3], color);
            Debug.DrawLine(points[3], points[0], color);
        }
        public static void DrawPoint(Vector3 position, Color color, float size)
        {
            Vector3[] points = new Vector3[]
            {
                position + (Vector3.up * size),
                position - (Vector3.up * size),
                position + (Vector3.right * size),
                position - (Vector3.right * size),
                position + (Vector3.forward * size),
                position - (Vector3.forward * size)
            };

            Debug.DrawLine(points[0], points[1], color);
            Debug.DrawLine(points[2], points[3], color);
            Debug.DrawLine(points[4], points[5], color);
            Debug.DrawLine(points[0], points[2], color);
            Debug.DrawLine(points[0], points[3], color);
            Debug.DrawLine(points[0], points[4], color);
            Debug.DrawLine(points[0], points[5], color);
            Debug.DrawLine(points[1], points[2], color);
            Debug.DrawLine(points[1], points[3], color);
            Debug.DrawLine(points[1], points[4], color);
            Debug.DrawLine(points[1], points[5], color);
            Debug.DrawLine(points[4], points[2], color);
            Debug.DrawLine(points[4], points[3], color);
            Debug.DrawLine(points[5], points[2], color);
            Debug.DrawLine(points[5], points[3], color);
        }
        #endregion
        #region Info
        public static string GetSystemInfo()
        {
            string result = "SYSTEM INFO";
#if UNITY_IOS
            result += "\n[iPhone generation]iPhone.generation.ToString()";
#endif
#if UNITY_ANDROID
            result += "\n[system info]" + SystemInfo.deviceModel;
#endif

            result += "\n<color=#FFFFFF>Device Type :</color> " + SystemInfo.deviceType;
            result += "\n<color=#FFFFFF>OS Version :</color> " + SystemInfo.operatingSystem;
            result += "\n<color=#FFFFFF>System Memory Size :</color> " + SystemInfo.systemMemorySize;
            result += "\n<color=#FFFFFF>Graphic Device Name :</color> " + SystemInfo.graphicsDeviceName + " (version " + SystemInfo.graphicsDeviceVersion + ")";
            result += "\n<color=#FFFFFF>Graphic Memory Size :</color> " + SystemInfo.graphicsMemorySize;
            result += "\n<color=#FFFFFF>Graphic Max Texture Size :</color> " + SystemInfo.maxTextureSize;
            result += "\n<color=#FFFFFF>Graphic Shader Level :</color> " + SystemInfo.graphicsShaderLevel;
            result += "\n<color=#FFFFFF>Compute Shader Support :</color> " + SystemInfo.supportsComputeShaders;

            result += "\n<color=#FFFFFF>Processor Count :</color> " + SystemInfo.processorCount;
            result += "\n<color=#FFFFFF>Processor Type :</color> " + SystemInfo.processorType;
            result += "\n<color=#FFFFFF>3D Texture Support :</color> " + SystemInfo.supports3DTextures;
            result += "\n<color=#FFFFFF>Shadow Support :</color> " + SystemInfo.supportsShadows;

            result += "\n<color=#FFFFFF>Platform :</color> " + Application.platform;
            result += "\n<color=#FFFFFF>Screen Size :</color> " + Screen.width + " x " + Screen.height;
            result += "\n<color=#FFFFFF>DPI :</color> " + Screen.dpi;

            return result;
        }
        #endregion
    }
}
