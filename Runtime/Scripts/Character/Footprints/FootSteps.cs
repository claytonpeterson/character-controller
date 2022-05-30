using UnityEngine;

namespace CharacterMovement
{
    public class FootSteps : MonoBehaviour
    {
        [SerializeField] private LayerMask terrainLayer;
        [SerializeField] private CharacterController cc;
        [SerializeField] private Sprite[] footImages;

        [Header("Footstep Spawning")]
        [SerializeField] private int stepPoolMax = 64;
        [SerializeField] private GameObject footstepPrefab;

        [SerializeField] private float footstepsPerSecond = 0.25f;
        [SerializeField] private float spawnDistance = 0.25f;

        [Header("Spawn Positions")]
        [SerializeField] private Transform leftFoot;
        [SerializeField] private Transform rightFoot;
        [SerializeField] private float footDistance;

        private GameObject objectPool;
        private GameObject[] footsteps;
        private int footstepIndex;
        private float stepSpawnLerp;

        private bool spawnLeft;
        private Vector3 lastPosition;

        void Start()
        {
            objectPool = CreateObjectPool();
            footsteps = CreateFootstepPrefabs(objectPool.transform);

            SetFeet();
        }

        void Update()
        {
            stepSpawnLerp += Time.deltaTime / footstepsPerSecond;

            if (stepSpawnLerp >= 1)
            {
                stepSpawnLerp = 0;
                PlaceFootstep();
            }
        }

        private void SetFeet()
        {
            leftFoot.position += new Vector3(-footDistance, 0, 0);
            rightFoot.position += new Vector3(+footDistance, 0, 0);
        }

        private GameObject CreateObjectPool()
        {
            GameObject objectPool = new GameObject("Footsteps");
            objectPool.transform.position = new Vector3(0, -100, 0);
            return objectPool;
        }

        private GameObject[] CreateFootstepPrefabs(Transform parent)
        {
            GameObject[] footsteps = new GameObject[stepPoolMax];
            for (int i = 0; i < stepPoolMax; i++)
            {
                footsteps[i] = Instantiate(footstepPrefab);
                footsteps[i].GetComponentInChildren<SpriteRenderer>().sprite = footImages[0];
                footsteps[i].transform.position = transform.position;
                footsteps[i].transform.SetParent(parent);
                footsteps[i].SetActive(false);
            }
            return footsteps;
        }

        private void PlaceFootstep()
        {
            if (HasNotMoved() || !cc.isGrounded)
            {
                return;
            }

            PositionFootstep(GetNextFootstep());

            lastPosition = transform.position;
        }

        private bool HasNotMoved()
        {
            return Vector3.Distance(transform.position, lastPosition) < spawnDistance;
        }

        private GameObject GetNextFootstep()
        {
            var footstep = footsteps[footstepIndex];
            footstep.SetActive(true);
            footstepIndex = (footstepIndex + 1) % footsteps.Length;
            return footstep;
        }

        private void PositionFootstep(GameObject footstep)
        {
            Transform foot = spawnLeft ? leftFoot : rightFoot;
            PositionOnTerrain(foot, footstep.transform);
            spawnLeft = !spawnLeft;
        }

        private void PositionOnTerrain(Transform foot, Transform footstep)
        {
            if (Physics.Raycast(foot.position + Vector3.up * 5, Vector3.down, out RaycastHit hit, Mathf.Infinity, terrainLayer))
            {
                footstep.position = HitPosition(hit);
                footstep.rotation = HitRotation(hit, foot.forward);
            }
        }

        private Vector3 HitPosition(RaycastHit hit)
        {
            return hit.point + hit.normal * .1f;
        }

        private Quaternion HitRotation(RaycastHit hit, Vector3 forward)
        {
            return Quaternion.LookRotation(forward, hit.normal);
        }
    }
}