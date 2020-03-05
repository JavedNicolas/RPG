using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LocalizeSound : LocalizeComponent
{
    AudioSource audioSource;
    public override KeyTargetType targetType => KeyTargetType.AudioClip;

    public override void getComponentToModify()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void setBasedOnLocation()
    {
        
        if (audioSource == null)
            return;

        AudioClip audioClip = (AudioClip)Localization.instance.getLocalizationForKey(key, KeyTargetType.AudioClip);
        audioSource.clip = audioClip;
    }
}
