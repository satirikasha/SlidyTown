using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathData : MonoBehaviour {

    public const float MaxLength = 30;

    public float Length { get; private set; }

    private PlayerController _PlayerController;
    private MovementController _MovementController;
    private Queue<Vector3> _PathPoints;
    private List<Vector3> _QueryPoints;
    private int _Counter;

    void Awake() {
        _PlayerController = this.GetComponent<PlayerController>();
        _MovementController = this.GetComponent<MovementController>();
        _PathPoints = new Queue<Vector3>();
    }

    void Start() {
        _PathPoints.Enqueue(Vector3.back * 10);
    }

	void Update () {
        if (GameManager.Instance.IsPlaying && !_PlayerController.Dead && _MovementController.IsTurning) {
            _Counter++;
            if (_Counter >= 1) {
                _Counter = 0;

                Length += (this.transform.position - _PathPoints.Last()).magnitude;
                _PathPoints.Enqueue(this.transform.position);

                while (Length > MaxLength) {
                    Length -= (_PathPoints.Dequeue() - _PathPoints.First()).magnitude;
                }

                _QueryPoints = _PathPoints.ToList();
            }
        }
	}

    public Vector3 GetPosition(float distance) {
        if (_QueryPoints == null || _QueryPoints.Count == 0)
            return this.transform.position;

        var prePoint = this.transform.position;
        for (int i = _QueryPoints.Count - 1; i >= 0; i--) {
            distance -= (prePoint - _QueryPoints[i]).magnitude;
            if(distance < 0) {
                return _QueryPoints[i] - (prePoint - _QueryPoints[i]).normalized * distance;
            }
            else {
                prePoint = _QueryPoints[i];
            }
        }
        return _QueryPoints[0];
    }

#if UNITY_EDITOR
    void OnDrawGizmos() {
        if (_PathPoints != null && _PathPoints.Count > 0) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, _PathPoints.Last());
            for (int i = 1; i < _PathPoints.Count; i++) {
                Gizmos.DrawSphere(_PathPoints.ElementAt(i), 0.1f);
                Gizmos.DrawLine(_PathPoints.ElementAt(i - 1), _PathPoints.ElementAt(i));
            }
        }
    }
#endif
}
