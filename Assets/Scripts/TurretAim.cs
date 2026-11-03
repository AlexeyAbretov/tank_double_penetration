using UnityEngine;

// Крутит башню за курсором по земле и по левой кнопке пускает снаряд.
public class TurretAim : MonoBehaviour
{
    public Transform muzzle;
    public GameObject shellPrefab;
    public float turnSpeed = 240f;
    public float fireCooldown = 0.4f;

    float nextShotAt;

    void Update()
    {
        Aim();
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Aim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, Vector3.zero);
        if (!ground.Raycast(ray, out float distance))
        {
            return;
        }

        Vector3 hit = ray.GetPoint(distance);
        Vector3 flat = hit - transform.position;
        flat.y = 0f;
        if (flat.sqrMagnitude < 0.001f)
        {
            return;
        }

        Quaternion target = Quaternion.LookRotation(flat, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            target,
            turnSpeed * Time.deltaTime);
    }

    void Fire()
    {
        if (shellPrefab == null || muzzle == null)
        {
            return;
        }
        if (Time.time < nextShotAt)
        {
            return;
        }
        nextShotAt = Time.time + fireCooldown;
        Instantiate(shellPrefab, muzzle.position, muzzle.rotation);
    }
}
