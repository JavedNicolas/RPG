using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;

namespace RPG.Environnement
{
    public class TerrainTextures : MonoBehaviour
    {
        [SerializeField] int width = 2, height = 2;
        [SerializeField] Gradient _gradient;
        [SerializeField] Material _material;
        [SerializeField] string _textureArrayAttributName = "Gradient_Texture";
        [SerializeField] Texture _generatedTexture;

        private void OnEnable()
        {
            setTextures();
        }

        [Button("Set Textures")]
        public void setTextures()
        {
            if (_material == null)
                return;

            /*Texture2DArray texture2DArray = new Texture2DArray(width, height, textures.Count, textures[0].format, false);
            for(int i =0; i < textures.Count; i++)
            {
                texture2DArray.SetPixels(textures[i].GetPixels(0, 0, width, height), i);
            }
 
            texture2DArray.Apply();*/

            Texture2D tex = new Texture2D(1, width);
            for (int i = 0; i < width; i++)
            {
                tex.SetPixel(0, i, _gradient.Evaluate((float)i / (float)width));
            }
            tex.Apply();
            AssetDatabase.CreateAsset(tex, "Assets/Resources/Materials/Textures/Terrain.png");
            _generatedTexture = tex;
            _material.SetTexture(_textureArrayAttributName, tex);
        }

    }

}
