using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace RythmGame
{
    internal class ScoreCounter : MonoBehaviour
    {
        public int score { get; private set; }
        [SerializeField] private TextMeshProUGUI scoreDisplay;
        [SerializeField] private List<TextMeshPro> hitDisplayList;
        [SerializeField] private List<Animator> hitAnimList;
        [SerializeField] private Queue<TextMeshPro> hitDisplay = new Queue<TextMeshPro>();
        [SerializeField] private Queue<Animator> hitAnimation = new Queue<Animator>();

        public void Awake()
        {
            hitAnimation.Clear();
            hitDisplay.Clear();
            hitDisplayList.ForEach(x => hitDisplay.Enqueue(x));
            hitAnimList.ForEach(x => hitAnimation.Enqueue(x));
        }

        public void AddScore(int _scoreMod)
        {
            score += _scoreMod;
            TextMeshPro hitText = hitDisplay.Dequeue();
            hitText.color = _scoreMod < 0 ? Color.red : Color.green;
            hitText.text = _scoreMod.ToString();
            Animator hitAnimator = hitAnimation.Dequeue();
            scoreDisplay.text = score.ToString();
            hitAnimator.SetTrigger("AddScore");
            StartCoroutine(Enqueue(hitText, hitAnimator));
        }

        private IEnumerator Enqueue(TextMeshPro _hitDisplay, Animator _animator)
        {
            yield return new WaitForSeconds(1);
            hitDisplay.Enqueue(_hitDisplay);
            hitAnimation.Enqueue(_animator);
        }
    }
}