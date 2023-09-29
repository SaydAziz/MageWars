using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    public Projectile spell;
    private Projectile cachedSpell;
    public void Create()
    {
        cachedSpell = Instantiate(spell, transform.parent);
    }
    public void Shoot()
    {
        cachedSpell.rb.detectCollisions = true;
        cachedSpell.gameObject.transform.parent = null;
        cachedSpell.rb.freezeRotation = false;
        cachedSpell.rb.constraints = RigidbodyConstraints.None;
        cachedSpell.rb.velocity = transform.forward * 20;

        cachedSpell.Shoot();
    }
}
