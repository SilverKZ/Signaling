using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityAlarm : MonoBehaviour
{
    private AudioSource _sound;
    private Coroutine _volumeChangeRoutine;

    private void Start()
    {
        _sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _sound.volume = 0f;
            _sound.Play();
            float speed = 1f;
            CoroutineControl(speed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            float speed = -1f;
            CoroutineControl(speed);
        }
    }

    private IEnumerator VolumeChange(float speed)
    {
        float minValume = 0f;
        float maxValume = 1f;

        while ((speed < 0 && _sound.volume > minValume) || (speed > 0 && _sound.volume < maxValume))
        {
            _sound.volume = Mathf.MoveTowards(_sound.volume, _sound.maxDistance, speed * Time.deltaTime);
            yield return null;
        }
    }

    private void CoroutineControl(float speed)
    {
        if (_volumeChangeRoutine != null)
        {
            StopCoroutine(_volumeChangeRoutine);
        }

        _volumeChangeRoutine = StartCoroutine(VolumeChange(speed));
    }
}
