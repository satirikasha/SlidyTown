using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathData : MonoBehaviour {

    public const float MaxLength = 20;

    public float Length { get; private set; }

    private PlayerController _PlayerController;
    private MovementController _MovementController;
    private Queue<Vector3> _PathPoints;
    private List<Vector3> _QueryPoints;

    void Awake() {
        _PlayerController = this.GetComponent<PlayerController>();
        _MovementController = this.GetComponent<MovementController>();
        _PathPoints = new Queue<Vector3>();
    }

    void Start() {
        _PathPoints.Enqueue(this.transform.position);
    }

	void Update () {
        if (GameManager.Instance.IsPlaying && !_PlayerController.Dead && _MovementController.IsTurning) {

            Length += (this.transform.position - _PathPoints.Last()).magnitude;
            _PathPoints.Enqueue(this.transform.position);

            while (Length > MaxLength) {
                Length -= (_PathPoints.Dequeue() - _PathPoints.First()).magnitude;
            }

            _QueryPoints = _PathPoints.ToList();
        }
	}

#if UNITY_EDITOR
    void OnDrawGizmos() {
        if (_PathPoints != null && _PathPoints.Count > 0) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, _PathPoints.Last());
            for (int i = 1; i < _PathPoints.Count; i++) {
                Gizmos.DrawLine(_PathPoints.ElementAt(i - 1), _PathPoints.ElementAt(i));
            }
        }
    }
#endif
}
