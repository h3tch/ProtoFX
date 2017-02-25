using System.ComponentModel;

namespace System
{
    public static class SynchronizationExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="action"></param>
        public static void SyncAction<T>(this T @this, Action<T> action) where T : ISynchronizeInvoke
        {
            if (@this.InvokeRequired)
                @this.Invoke(action, new object[] { @this });
            else
                action(@this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="this"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TResult SyncFunc<T, TResult>(this T @this, Func<T, TResult> func) where T : ISynchronizeInvoke
        {
            if (@this.InvokeRequired)
                return (TResult)@this.Invoke(func, new object[] { @this });
            else
                return func(@this);
        }
    }
}
