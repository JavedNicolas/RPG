using UnityEngine;
using System.Collections;
using RPG.Data;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG.UI {

    public class BattleActorMenuItem : MonoBehaviour
    {
        public TextMeshProUGUI actorName;
        [SerializeField] TextMeshProUGUI _lifeValue;
        [SerializeField] Image _lifeBarFill;

        public void updateLife(Being actor)
        {
            if (actor == null)
            {
                Debug.LogWarning("Actor empty in the updateLife function !");
                return;
            }
                

            _lifeValue.text = actor.currentLife.ToString();
            _lifeBarFill.fillAmount = (float)actor.currentLife / (float)actor.maxLife;
        }
    }
}
