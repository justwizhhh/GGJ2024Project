using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObject : MonoBehaviour
{
    /*
     * Base Object Class
     * Parent class for all gameplay-related objects
     */

    LevelManager level;
    new AudioManager audio;

    SpriteRenderer sr;

    private void Awake()
    {
        level = LevelManager.instance;
        audio = AudioManager.instance;

        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!level.IsPaused)
        {
            ObjectUpdate();
        }
    }

    private void FixedUpdate()
    {
        if (!level.IsPaused)
        {
            ObjectFixedUpdate();
        }
    }

    private void LateUpdate()
    {
        if (!level.IsPaused)
        {
            ObjectLateUpdate();
        }
    }

    public virtual void ObjectUpdate() { }
    public virtual void ObjectFixedUpdate() { }
    public virtual void ObjectLateUpdate() { }
}
