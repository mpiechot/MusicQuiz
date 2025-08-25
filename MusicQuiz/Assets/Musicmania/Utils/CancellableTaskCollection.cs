using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Musicmania.Utils
{
    /// <summary>
    ///    A collection of async functions that can be cancelled.
    ///    Reusing the collection after calling <see cref="CancelExecution"/> is also supported.
    ///    In this case a new <see cref="CancellationTokenSource"/> will be created.
    /// </summary>
    public sealed class CancellableTaskCollection : IDisposable
    {
        private List<UniTask> tasks = new();

        private CancellationTokenSource cancellationTokenSource = new();

        /// <summary>
        ///     Start the execution of a async function.
        /// </summary>
        /// <param name="asyncFunction">The async function to execute.</param>
        public void StartExecution(Func<CancellationToken, UniTask> asyncFunction)
        {
            _ = RunAsync(asyncFunction);
        }

        /// <summary>
        ///    Cancel the execution of all async functions in the collection.
        /// </summary>
        public void CancelExecution()
        {
            if (cancellationTokenSource.IsCancellationRequested)
            {
                return;
            }

            cancellationTokenSource.Cancel();
            tasks.Clear();
        }

        /// <summary>
        ///   Dispose the collection and cancel all async functions.
        /// </summary>
        public void Dispose()
        {
            CancelExecution();
            cancellationTokenSource.Dispose();
        }


        private async UniTaskVoid RunAsync(Func<CancellationToken, UniTask> asyncFunction)
        {
            if (cancellationTokenSource.IsCancellationRequested)
            {
                cancellationTokenSource.Dispose();
                cancellationTokenSource = new CancellationTokenSource();
            }

            var task = default(UniTask);
            try
            {
                task = asyncFunction(cancellationTokenSource.Token);
                tasks.Add(task);
                await task;
            }
            catch (OperationCanceledException)
            {
                // Ignore cancels
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(e);
            }
            finally
            {
                tasks.Remove(task);
            }
        }
    }
}