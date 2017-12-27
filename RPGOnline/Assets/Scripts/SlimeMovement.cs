using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class SlimeMovement : NetworkBehaviour 
{
	public bool onPersecution;

	NavMeshAgent nv;
	Transform target;
	Animator anim;

	// Use this for initialization
	void Start () {
		if (!isServer)
			return;
		nv = GetComponent<NavMeshAgent> ();
		InvokeRepeating ("FollowTarget", 0f, 5f);
		anim = GetComponent<Animator> ();
        InvokeRepeating("Attack", 0f, 5f);
	}

	void FollowTarget()
	{
		if (!isServer)
			return;

		if (!onPersecution) 
			nv.destination = transform.position + new Vector3 (Random.value - Random.value, 0, Random.value - Random.value) * 100;

    }

    void Attack()
    {
        if (!isServer)
            return;

        if (!target)
            return;

        if (Vector3.Distance(transform.position, target.position) <= nv.stoppingDistance + 0.1f)
        {
            target.SendMessage("ChangeHealth", 10);
        }
    }

	// Update is called once per frame
	void Update () 
	{
		if (!isServer)
			return;

		anim.SetFloat ("Speed", nv.velocity.magnitude / nv.speed);
		if (onPersecution)        
			nv.destination = target.position;
		
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag ("Player")) {
			onPersecution = true;
			target = col.transform;
		}
	}
	void OnTriggerExit(Collider col)
	{
		if (col.transform == target) {
			onPersecution = false;
			target = null;		
		}
	}

}
