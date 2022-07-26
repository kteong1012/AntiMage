﻿using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using FairyGUI;
using System;
using UnityEngine;

namespace PostMainland
{
    public struct FGUIDoTweenResult
    {
        public UniTask Task { get; set; }
        public Tweener Tweener { get; set; }
    }
    public static class FGUIExtensions
    {
        public delegate void ButtonOnClickDelegate();
        public static void SetOnClick(this GButton button, ButtonOnClickDelegate callback)
        {
            button.onClick.Set(() => callback?.Invoke());
        }
        public delegate UniTask ButtonOnClickAsyncDelegate();
        public static void SetOnClickAsync(this AsyncGButton button, ButtonOnClickAsyncDelegate onClickAsync, int timeoutMilli = 0)
        {
            async UniTask OnClick()
            {
                if (button.isLocking)
                {
                    Log.Error("操作频繁");
                    return;
                }
                if (onClickAsync != null)
                {
                    button.isLocking = true;
                    try
                    {
                        if (timeoutMilli > 0)
                        {
                            int index = await UniTask.WhenAny(UniTask.Delay(timeoutMilli), onClickAsync());
                            if (index == 0)
                            {
                                Log.Error("超时");
                            }
                        }
                        else
                        {
                            await onClickAsync();
                        }
                    }
                    finally
                    {
                        if (button != null)
                        {
                            button.isLocking = false;
                        }
                    }
                }
            }
            button.isLocking = false;
            button.Button.onClick.Set(() =>
            {
                OnClick().Forget();
            });
        }
        public static FGUIDoTweenResult DoMove(this GObject obj, Vector2 endValue, float duration, Action<GObject> onUpdate = null)
        {
            var tcs = new UniTaskCompletionSource();
            var t = DOTween.To(() => obj.position, (Vector2 value) => obj.position = value, endValue, duration)
                .OnUpdate(() => onUpdate?.Invoke(obj))
                .OnComplete(() =>
                {
                    tcs.TrySetResult();
                });
            t.SetTarget(obj);
            return new FGUIDoTweenResult() { Task = tcs.Task, Tweener = t };
        }
        public static FGUIDoTweenResult DoScaleX(this GObject obj, float endValue, float duration, Action<GObject> onUpdate = null)
        {
            var tcs = new UniTaskCompletionSource();
            var t = DOTween.To(() => obj.scaleX, (float value) => obj.scaleX = value, endValue, duration)
                .OnUpdate(() => onUpdate?.Invoke(obj))
                .OnComplete(() =>
                {
                    tcs.TrySetResult();
                });
            t.SetTarget(obj);
            return new FGUIDoTweenResult() { Task = tcs.Task, Tweener = t };
        }
        public static FGUIDoTweenResult DoScaleY(this GObject obj, float endValue, float duration, Action<GObject> onUpdate = null)
        {
            var tcs = new UniTaskCompletionSource();
            var t = DOTween.To(() => obj.scaleY, (float value) => obj.scaleY = value, endValue, duration)
                .OnUpdate(() => onUpdate?.Invoke(obj))
                .OnComplete(() =>
                {
                    tcs.TrySetResult();
                });
            t.SetTarget(obj);
            return new FGUIDoTweenResult() { Task = tcs.Task, Tweener = t };
        }
        public static FGUIDoTweenResult DoScale(this GObject obj, Vector2 endValue, float duration, Action<GObject> onUpdate = null)
        {
            var tcs = new UniTaskCompletionSource();
            var t = DOTween.To(() => obj.scale, (Vector2 value) => obj.scale = value, endValue, duration)
                .OnUpdate(() => onUpdate?.Invoke(obj))
                .OnComplete(() =>
                {
                    tcs.TrySetResult();
                });
            t.SetTarget(obj);
            return new FGUIDoTweenResult() { Task = tcs.Task, Tweener = t };
        }
        public static FGUIDoTweenResult DoFade(this GObject obj, float endValue, float duration, Action<GObject> onUpdate = null)
        {
            var tcs = new UniTaskCompletionSource();
            var t = DOTween.To(() => obj.alpha, (a) => obj.alpha = a, endValue, duration)
                .OnComplete(() =>
                {
                    tcs.TrySetResult();
                });
            t.SetTarget(obj);
            return new FGUIDoTweenResult() { Task = tcs.Task, Tweener = t };
        }
    }
}
