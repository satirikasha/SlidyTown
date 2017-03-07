using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour {

    public const int MaxFollowers = 10;

    public int FollowersCount { get; private set; }

    private List<PlayerFollower> _Followers = new List<PlayerFollower>();

	void Awake () {
        this.GetComponent<PlayerController>().OnPickedUp += AddFollower;
	}

    private void AddFollower(PickupItem item) {
        if (FollowersCount < MaxFollowers) {
            var follower = Instantiate(WorldObjectProvider.GetWorldObject<GameObject>("Follower")).GetComponent<PlayerFollower>();
            follower.Index = FollowersCount;
            _Followers.Add(follower);
            FollowersCount++;
        }
    }

    public PlayerFollower GetFollower(int index) {
        return _Followers[index];
    }
}

