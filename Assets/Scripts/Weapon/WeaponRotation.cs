using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform playerPoint;
    [SerializeField] private Transform aimPoint;
    [SerializeField] private Transform weaponVisual;

    [Header("Arc")]
    [SerializeField] private float radius = 1.5f;
    [SerializeField] private float minAngle = -60f;
    [SerializeField] private float maxAngle = 80f;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        UpdateAim();
    }

    private void UpdateAim()
    {
        if (!cam || !playerPoint || !aimPoint) return;

        Vector3 mp = Input.mousePosition;
        mp.z = -cam.transform.position.z;
        Vector3 mouseWorld = cam.ScreenToWorldPoint(mp);

        Vector2 local = playerPoint.InverseTransformPoint(mouseWorld);

        bool flip = local.x < 0f;
        if (flip)
        {
            local.x = -local.x;
        }

        float angle = Mathf.Atan2(local.y, local.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);

        Vector2 dirLocal = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));


        if (flip)
        {
            dirLocal.x = -dirLocal.x;
        }

        Vector2 dirWorld = playerPoint.TransformDirection(dirLocal).normalized;
        aimPoint.position = (Vector2)playerPoint.position + dirWorld * radius;

        float worldAngle = Mathf.Atan2(dirWorld.y, dirWorld.x) * Mathf.Rad2Deg;
        aimPoint.rotation = Quaternion.Euler(0f, 0f, worldAngle);

        if (weaponVisual != null)
        {
            Vector3 s = weaponVisual.localScale;
            s.y = flip ? -Mathf.Abs(s.y) : Mathf.Abs(s.y);
            weaponVisual.localScale = s;
        }
    }
}
