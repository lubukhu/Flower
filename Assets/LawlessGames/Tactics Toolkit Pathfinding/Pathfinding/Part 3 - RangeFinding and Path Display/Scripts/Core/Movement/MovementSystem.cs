namespace finished3
{
    public class MovementSystem
    {
        public MovementType GetMovementType(OverlayTile from, OverlayTile to)
        {
            float diff = to.transform.position.z - from.transform.position.z;

            if (diff > 0.1f)
                return MovementType.Climb;

            if (diff < -0.1f)
                return MovementType.Jump;

            return MovementType.Walk;
        }
    }
}