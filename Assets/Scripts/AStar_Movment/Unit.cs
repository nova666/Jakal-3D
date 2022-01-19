using UnityEngine;
using System.Collections;


namespace ArmyOfOne
{
	public class Unit : UnitState
	{
		public enum _UnitType
		{
			Soldier,
			Turret,
			Tank,
			Civilian
		};

		[Header("The tipe of behaviour each character/unit has")]
		public _UnitType _Unit = _UnitType.Civilian;
		const float minPathUpdateTime = .2f;
		const float pathUpdateMoveThreshold = .5f;

		public Steering _Steerig;
		public FlockManager myManager;

		//public Transform target; /// --> this is what is needed to be followed
		public Vector3 _Target;

		public float speed = 20;
		public float turnSpeed = 3;
		public float turnDst = 5;
		public float stoppingDst = 10;

		Path path;
		//UnitState _State;
		public GameObject Bullet;
		public Transform muzzle;

		private void Awake()
		{
			UnitType = _Unit.ToString();

		}
		void Start()
		{

			ChangeBehaviour();

		}
		public void ChangeBehaviour()
		{

			StopAllCoroutines();
			if (_UnitBehaviour == UnitState._UnitState.Patrol)
			{
				_Target = ReturnNextTarget();
				StartCoroutine(UpdatePath()); // --> looking for path // this random point on map
			}
			else if (_UnitBehaviour == UnitState._UnitState.Chasing)
			{
				_Target = ReturnNextTarget();
				StartCoroutine(UpdatePath()); // --> looking for path // this is following the player
			}
			else if (_UnitBehaviour == UnitState._UnitState.Attacking)
			{
				// attack
				_Target = ReturnNextTarget();
				StartCoroutine(AttackEnemy());
			}
			else if (_UnitBehaviour == UnitState._UnitState.Hiding)
			{
				_Target = ReturnNextTarget();
				StartCoroutine(UpdatePath());
			}
		}

		IEnumerator AttackEnemy()
		{
			while (true)
			{
				yield return new WaitForSeconds(0.3f); /// Rate of fire
				if ((_UnitBehaviour != UnitState._UnitState.Attacking))
				{
					Debug.Log("Behaviour Changed");
					ChangeBehaviour();
				}
				else
				{
					// turn to enemy 
					transform.LookAt(ReturnNextTarget());
					// shoot
					/// Shoot stright projectile
					GameObject bullet = Instantiate(Bullet, muzzle.position, muzzle.rotation) as GameObject;

				}

			}
		}

		#region Methods to control pathFinding
		public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
		{
			if (pathSuccessful)
			{
				path = new Path(waypoints, transform.position, turnDst, stoppingDst);

				StopCoroutine("FollowPath");
				StartCoroutine("FollowPath");
			}
		}

		IEnumerator UpdatePath()
		{

			if (Time.timeSinceLevelLoad < .3f)
			{
				yield return new WaitForSeconds(.3f);
			}
			PathRequestManager.RequestPath(new PathRequest(transform.position, _Target, OnPathFound));

			float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
			Vector3 targetPosOld = _Target;

			while (true)
			{
				
				yield return new WaitForSeconds(minPathUpdateTime);
				//print (((point - targetPosOld).sqrMagnitude) + "    " + sqrMoveThreshold);
				//Debug.Log("Magnitude " + (transform.position - point).magnitude);

				if ((_Target - targetPosOld).sqrMagnitude > sqrMoveThreshold)
				{
					PathRequestManager.RequestPath(new PathRequest(transform.position, _Target, OnPathFound));
					targetPosOld = _Target;
				}


				if ((_UnitBehaviour == UnitState._UnitState.Chasing))
				{
					_Target = ReturnNextTarget();
				}
				else if ((transform.position - _Target).magnitude < 3)
				{
					if ((_UnitBehaviour != UnitState._UnitState.Patrol))
					{
						Debug.Log("Behaviour Changed");
						ChangeBehaviour();
					}
					else
					{
						_Target = ReturnNextTarget();
					}
				}
			}


		}

		IEnumerator FollowPath()
		{

			bool followingPath = true;
			int pathIndex = 0;
			//transform.LookAt(path.lookPoints[0]);
			float speedPercent = 1;

			while (followingPath)
			{
				Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
				while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
				{
					if (pathIndex == path.finishLineIndex)
					{
						followingPath = false;
						break;
					}
					else
					{
						pathIndex++;
					}
				}

				if (followingPath)
				{

					if (pathIndex >= path.slowDownIndex && stoppingDst > 0)
					{
						speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDst);
						if (speedPercent < 0.01f)
						{
							followingPath = false;
						}
					}

					//ApplyRules(path.lookPoints[pathIndex] - transform.position);
					_Steerig.target = path.lookPoints[pathIndex] - transform.position;
					//Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
					//transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
					//transform.Translate(Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
				}

				yield return null;

			}
		}

		private void ApplyRules(Vector3 Target)
		{
			GameObject[] gos;
			gos = myManager._AllItems;

			Vector3 vcentre = Vector3.zero;
			Vector3 vavoid = Vector3.zero;
			float gSpeed = 5f;
			Vector3 goalPos = Target; // this os the target

			float nDistance;
			int groupSize = 0;

			foreach (var item in gos)
			{
				if (item != this.gameObject)
				{
					nDistance = Vector3.Distance(item.transform.position, this.transform.position);
					if (nDistance <= myManager.neighbourDistance)
					{
						vcentre += item.transform.position;
						groupSize++;

						if (nDistance < 1.0f)
						{
							vavoid = vavoid + (this.transform.position - item.transform.position);
						}

						//Flock anotherFlock = item.GetComponent<Flock>();
						//gSpeed = gSpeed + anotherFlock.speed;
					}
				}
			}

			if (groupSize > 0)
			{
				vcentre = vcentre / groupSize + (goalPos - this.transform.position);
				speed = gSpeed / groupSize;

				Vector3 direction = (vcentre + vavoid) - transform.position;
				//if (direction != Vector3.zero)
					//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
						//myManager.rotationSpeed * Time.deltaTime);
			}

		}

		private void ApplyRules()
		{
			GameObject[] gos;
			gos = myManager._AllItems;

			Vector3 vcentre = Vector3.zero;
			Vector3 vavoid = Vector3.zero;
			float gSpeed = 0.01f;
			float nDistance;
			int groupSize = 0;

			foreach (var item in gos)
			{
				if (item != this.gameObject)
				{
					nDistance = Vector3.Distance(item.transform.position, this.transform.position);
					if (nDistance <= myManager.neighbourDistance)
					{
						vcentre += item.transform.position;
						groupSize++;

						if (nDistance == 1.0f)
						{
							vavoid = vavoid + (this.transform.position - item.transform.position);
						}

						///Flock anotherFlock = item.GetComponent<Flock>();
						//gSpeed = gSpeed + 3;
					}
				}
			}

			//if (groupSize > 0)
			//{
			//	vcentre = vcentre / groupSize;
			//	speed = gSpeed / groupSize;
			//
				//Vector3 direction = (vcentre + vavoid) - transform.position;
				// (direction != Vector3.zero)
					//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
					//	myManager.rotationSpeed * Time.deltaTime);
			//}

		}

		public void OnDrawGizmos()
		{
			if (path != null)
			{
				path.DrawWithGizmos();
			}


			Gizmos.color = new Color(1f, 0, 0);
			Gizmos.DrawWireSphere(transform.position, 10f);
		}
		#endregion
	}

}

