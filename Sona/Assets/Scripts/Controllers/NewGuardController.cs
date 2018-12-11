using System.Collections.Generic;
using UnityEngine;
using ApplicationConstants;

public class NewGuardController : MonoBehaviour {
    GuardState state;

    float viewDistance;
    float viewAngle = GuardConstants.guardVisionAngle;
    float heightMultiplier = 1.6f; //Only for debug purpose
    SphereCollider sphereCollider;

    public List<Player> playersInRange = new List<Player>();

    NewGuardGroup guardGroup;
    List<Player> visiblePlayers = new List<Player>();
    GuardMovement movement;

    void Start(){
        guardGroup = GetComponentInParent<NewGuardGroup>();
        sphereCollider = GetComponent<SphereCollider>();
        movement = GetComponent<GuardMovement>();
        ChangeState(new AllertState());
    }

    void Update(){
        Investigate();
        CheckIfCatch();
    }

    /**
     * This method is used by guards to find the player
     */
    void Investigate(){
        //visual rappresentation of this ray
        DrawRays();

        //Look for player in his view
        LookForPlayers();

        //Chase the nearest player in the group targets
        ChaseNearestPlayer();
    }

    private void CheckIfCatch(){
        foreach(Player player in playersInRange){
            if (!characterInvisible(player)){
                if (Vector3.Distance(transform.position, player.transform.position) < GuardConstants.playerCatchedMaxDistance){
                    FindObjectOfType<CheckpointController>().LoadLastCheckPoint();
                }
            }
        }
    }

    private void DrawRays(){
        Vector3 rightRay = (Quaternion.AngleAxis(viewAngle, Vector3.up) * transform.forward).normalized;
        Vector3 leftRay = (Quaternion.AngleAxis(-viewAngle, Vector3.up) * transform.forward).normalized;
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * viewDistance, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, rightRay * viewDistance, Color.green);
        Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, leftRay * viewDistance, Color.green);
    }

    public List<Player> GetVisiblePlayers() { return visiblePlayers; }

    private void LookForPlayers(){
        foreach(Player player in playersInRange){
            //printSomething(player);
            if (!characterInvisible(player) && PlayerIsInSightAngle(player) && !SomethingBetweenPlayerAndEnemy(player)){
                //print("Visto");
                if (!visiblePlayers.Contains(player)) visiblePlayers.Add(player);
                guardGroup.AddPlayer(player);
            } else {
                visiblePlayers.Remove(player);
                guardGroup.RemoveTarget(player);
            }
        }
    }

    private void printSomething(Player player)
    {
        if (transform.parent.name == "Group (2)")
        {
            print("Character visible: " + !characterInvisible(player));
            print("Player in sight angle: " + PlayerIsInSightAngle(player));
            print("Something between? " + SomethingBetweenPlayerAndEnemy(player));
        }
    }

    private void ChaseNearestPlayer(){
        List<Player> playersToFollow = guardGroup.GetGroupTargets();
        if (playersToFollow.Count == 0) movement.FollowPlayer(false, transform.position);
        else {
            Player player = IdentifyNearestPlayer(playersToFollow);
            movement.FollowPlayer(true, player.transform.position);
        }
    }

    private bool characterInvisible(Player player){
        Hannah hannah = player.GetComponent<Hannah>();
        return (hannah && hannah.enabled && hannah.isPowerActive());
    }

    private bool PlayerIsInSightAngle(Player player){
        return (CalculateAngle(player) <= viewAngle);
    }

    private float CalculateAngle(Player player){
        Vector3 directionToPlayer = player.transform.position - transform.position;
        return Vector3.Angle(directionToPlayer, transform.forward);
    }

    private bool SomethingBetweenPlayerAndEnemy(Player player){
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, player.transform.position - transform.position, distanceToPlayer);
        return (hits.Length > 1);
    }

    private Player IdentifyNearestPlayer(List<Player> players){
        Player nearestPlayer = players[0];
        float nearestDistance = Vector3.Distance(transform.position, nearestPlayer.transform.position);

        foreach (Player player in players) {
            float newDist = Vector3.Distance(transform.position, player.transform.position);
            if (newDist < nearestDistance){
                nearestPlayer = player;
                nearestDistance = newDist;
            }
        }
        return nearestPlayer;
    }

    private void OnTriggerEnter(Collider other){
        if (other.tag == PlayersConstants.playerTag) playersInRange.Add(other.GetComponent<Player>());
    }

    private void OnTriggerExit(Collider other){
        if (other.tag == PlayersConstants.playerTag) playersInRange.Remove(other.GetComponent<Player>());
    }

    /**
     * This method is used to change guard state
     */
    public void ChangeState(GuardState newState){
        state = newState;
        viewDistance = newState.GetRadius();
        sphereCollider.radius = viewDistance;
        //Debug.Log(viewDistance);
    }
}
