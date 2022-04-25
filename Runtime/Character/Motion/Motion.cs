using UnityEngine;

namespace CharacterMovement
{
    [System.Serializable]
    public class Motion
    {
        [SerializeField]
        private ControlledForce movement;

        [SerializeField]
        private CombinedForce forces;

        private readonly float gravity;

        private readonly IForce[] motionSources;

        public CombinedForce Forces { get => forces; }
        public ControlledForce Movement { get => movement; }

        public Motion(Transform body, float gravity)
        {
            this.gravity = gravity;
            
            movement = new ControlledForce(body);
            forces = new CombinedForce();
        }

        public void Update(Vector3 moveDirection, float moveSpeed)
        {
            // it would be cool to move this up a level
            Movement.AddInput(moveDirection, moveSpeed);

            forces.RemoveCompleteForces();
        }

        public Vector3 Velocity(bool useGravity)
        {
            var v = Movement.ForceVelocity();

            if(useGravity)
                v = ApplyGravity(v);

            return v + forces.Velocity();
        } 

        public Vector3 Velocity()
        {
            Vector3 velocity = Vector3.zero;

            if (motionSources == null)
                return velocity;
            
            for(int i = 0; i < motionSources.Length; i++)
            {
                velocity += motionSources[i].ForceVelocity();
            }
            return velocity;
        }

        private Vector3 ApplyGravity(Vector3 velocity)
        {
            velocity.y -= gravity;
            return velocity;
        }
    }
}