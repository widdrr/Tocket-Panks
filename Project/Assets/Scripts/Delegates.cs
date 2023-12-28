using UnityEngine;
public static class Delegates 
{
    public delegate void VoidDelegate();
    public delegate void ExplosionDelegate(Explosion self, Collider2D other);
}