using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using SaveSystem;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class ControlsPresenter : IControlsPresenter, IDisposable
    {
        private readonly SaveManager _saveManager;
        private readonly CancellationTokenSource _cts = new();

        public ControlsPresenter(SaveManager saveManager)
        {
            _saveManager = saveManager;
        }

        void IControlsPresenter.Save(Action<bool, int> callback) =>
            SaveInternal(callback).Forget();

        void IControlsPresenter.Load(string version, Action<bool, int> callback) =>
            LoadInternal(version, callback).Forget();

        private async UniTaskVoid SaveInternal(Action<bool, int> callback)
        {
            var token = _cts.Token;
            try
            {
                token.ThrowIfCancellationRequested();
                var result = await _saveManager.Save(token);
                token.ThrowIfCancellationRequested();
                callback?.Invoke(result, 1);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                callback?.Invoke(false, 0);
            }
        }

        private async UniTaskVoid LoadInternal(string version, Action<bool, int> callback)
        {
            var token = _cts.Token;
            try
            {
                token.ThrowIfCancellationRequested();

                var result = await _saveManager.Load(version, token);

                token.ThrowIfCancellationRequested();
                // callback?.Invoke(true, result);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                callback?.Invoke(false, 0);
            }
        }

        void IDisposable.Dispose()
        {
            if (_cts.IsCancellationRequested)
                return;

            _cts.Cancel();
            _cts.Dispose();
        }
    }
}