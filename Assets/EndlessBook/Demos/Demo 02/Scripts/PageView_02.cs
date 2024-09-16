

namespace echo17.EndlessBook.Demo02
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using echo17.EndlessBook;

    /// <summary>
    /// Table of contents page.
    /// Handles clicks on the chapters to jump to pages in the book
    /// </summary>
    public class PageView_02 : PageView
    {
        [SerializeField] private Canvas _canvas;
        private int firstTime = 0;

        private void OnEnable()
        {
            if (firstTime < 2)
            {
                firstTime++;
                StartCoroutine(CanvasSize());
            }
        }
        
        private IEnumerator CanvasSize()
        {
            Debug.Log("CanvasSize");
            // gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _canvas.gameObject.SetActive(false);
            yield return new WaitForEndOfFrame();
            _canvas.gameObject.SetActive(true);
        }

        protected override bool HandleHit(RaycastHit hit, BookActionDelegate action)
        {
            return true;
        }
    }
}
