using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FifteenPuzzle
{
    public class Tile : MonoBehaviour
    {
       
        [SerializeField] private Sprite _imagePart;
        Vector3 startPos;
        Vector3 targetPos;
        SpriteRenderer _renderer;

        public Vector3 TargetPosition
        {
            get { return targetPos; }
        }

        void Start()
        {
            startPos = transform.position;
            targetPos = startPos;
            _renderer.sprite = _imagePart;
        }

       

        public bool IsInPlace()
        {
            return targetPos.Equals(startPos);
        }

        public void MoveToPos(Vector3 newPos)
        {
            targetPos = newPos;

            StartCoroutine(MoveToTarget());
        }

        public void MoveToPosNoAnim(Vector3 newPos)
        {
            targetPos = newPos;
            transform.position = targetPos;

            ColorAdjustment();
        }
        public void SetImage(Sprite sprite)
        {
            _imagePart = sprite;
            _renderer = GetComponent<SpriteRenderer>();
            _renderer.sprite = _imagePart;
            _renderer.transform.localScale = new Vector3(0.8f, 0.8f, 1f);
        }

        IEnumerator MoveToTarget()
        {
            while(Vector3.Distance(transform.position, targetPos) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 10f);
                yield return null;
            }
            transform.position = targetPos;

            ColorAdjustment();
        }

        void ColorAdjustment()
        {
            _renderer.color = IsInPlace() ? new Color(1f, 0.85f, 0f) : Color.white;
        }

        void OnMouseDown()
        {
            GameManager.Instance.ClickedTile(this);
        }
    }
}
