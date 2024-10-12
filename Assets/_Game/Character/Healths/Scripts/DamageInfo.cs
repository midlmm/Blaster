using UnityEngine;

public class DamageInfo
{
    public int Damage;
    public Vector3 Normal;
    public Vector3 Point;
    
    public DamageInfo(int damage)
    {
        Damage = damage;
        Normal = Vector3.zero;
        Point = Vector3.zero;
    }

    public DamageInfo(int damage, Vector3 normal, Vector3 point)
    {
        Damage = damage;
        Normal = normal;
        Point = point;
    }
}
