using UnityEngine;

namespace Project.Scripts {
    public class Field : MonoBehaviour {
        [SerializeField] private Transform topLeftPoint;
        [SerializeField] private Transform bottomRightPoint;

        public Vector2 GetPositionInRelativeFormat(Vector2 position) {
            var topLeftPos = topLeftPoint.position;
            var bottomRightPos = bottomRightPoint.position;
            var relativeX = (position.x - topLeftPos.x) / (topLeftPos.x - bottomRightPos.x);
            var relativeY= (position.y - topLeftPos.y) / (topLeftPos.y - bottomRightPos.y);
            return new Vector2(relativeX, relativeY);
        }
    }
}