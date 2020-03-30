using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LocalizeImage : LocalizeComponent
{
    Image image;
    public override KeyTargetType targetType => KeyTargetType.Sprite;

    public override void getComponentToModify()
    {
        image = GetComponent<Image>();
    }

    public override void setBasedOnLocation()
    {
        if (image == null)
            return;

        Texture2D texture = (Texture2D)Localization.instance.getLocalizationForKey(key, KeyTargetType.Sprite);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.height, texture.width), new Vector2(0.5f, 0.5f));
        sprite.name = texture.name;
        image.sprite = sprite;
    }
}
