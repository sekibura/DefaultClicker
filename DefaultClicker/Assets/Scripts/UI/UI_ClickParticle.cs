using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SekiburaGames.DefaultClicker.Effects
{
    public class UI_ClickParticle : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _particleSystem;

        [SerializeField]
        private Camera _camera;

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Clicked(touch.position);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                Vector3 mousePos = Input.mousePosition;
                {
                    Vector2 vector = new Vector2(mousePos.x, mousePos.y);
                    Clicked(vector);
                }
            }
        }

        private void Clicked(Vector2 position)
        {
            Debug.Log("Touch Position : " + position);
            var worldPosition = _camera.ScreenToWorldPoint(position);
            worldPosition.z = 0;
            _particleSystem.transform.position = worldPosition;
            _particleSystem.Stop();
            _particleSystem.Play();

        }

   
    }
}
