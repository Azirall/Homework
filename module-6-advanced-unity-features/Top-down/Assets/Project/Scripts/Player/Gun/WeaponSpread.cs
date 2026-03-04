using UnityEngine;

public class WeaponSpread
{
    private readonly float _spreadAngle;

    public WeaponSpread(float spreadAngle)
    {
        _spreadAngle = spreadAngle;
    }

    public Vector3 ApplySpread(Vector3 direction)
    {
        direction = direction.normalized;

        if (_spreadAngle <= 0f)
        {
            return direction;
        }

        float yaw = Random.Range(-_spreadAngle, _spreadAngle);
        float pitch = Random.Range(-_spreadAngle, _spreadAngle);

        Quaternion spreadRotation = Quaternion.Euler(pitch, yaw, 0f);
        return spreadRotation * direction;
    }
}
