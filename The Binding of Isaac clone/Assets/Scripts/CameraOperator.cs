using System.Collections;
using UnityEngine;

namespace IsaacClone
{
    /// <summary>
    /// Оператор камеры
    /// </summary>
    public class CameraOperator : MonoBehaviour
    {
        [SerializeField]
        private float _cameraDelay = 0.01f;
        [SerializeField]
        private float _cameraSpeed = 10f;

        private bool _isCameraMoving = false;

        public bool IsCameraMoving { get => _isCameraMoving; set => _isCameraMoving = value; }

        /// <summary>
        /// Перемещать камеру
        /// </summary>
        /// <param name="goal">Цель камеры</param>
        /// <returns></returns>
        public IEnumerator MoveCamera(Transform goal)
        {
            _isCameraMoving = true;

            if (goal.position.x != transform.position.x)
            {
                if (goal.position.x > transform.position.x)
                {
                    while (goal.position.x > transform.position.x)
                    {
                        transform.localPosition += Vector3.right * _cameraSpeed * Time.deltaTime;

                        yield return new WaitForSeconds(_cameraDelay);
                    }
                }
                else
                {
                    while (goal.position.x < transform.position.x)
                    {
                        transform.localPosition += Vector3.left * _cameraSpeed * Time.deltaTime;

                        yield return new WaitForSeconds(_cameraDelay);
                    }
                }
            }
            else
            {
                if (goal.position.y > transform.position.y)
                {
                    while (goal.position.y > transform.position.y)
                    {
                        transform.localPosition += Vector3.up * _cameraSpeed * Time.deltaTime;

                        yield return new WaitForSeconds(_cameraDelay);
                    }
                }
                else
                {
                    while (goal.position.y < transform.position.y)
                    {
                        transform.localPosition += Vector3.down * _cameraSpeed * Time.deltaTime;

                        yield return new WaitForSeconds(_cameraDelay);
                    }
                }
            }

            transform.localPosition = new Vector3(goal.position.x, goal.position.y, transform.position.z);

            _isCameraMoving = false;
        }
    }
}