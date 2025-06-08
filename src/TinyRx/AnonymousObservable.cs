using System;

namespace TinyRx {

	internal class AnonymousObservable<T> : IObservable<T> {

		private readonly Func<IObserver<T>, IDisposable> _subscribe;

		public AnonymousObservable(Func<IObserver<T>, IDisposable> subscribe) {
			_subscribe = subscribe ?? throw new ArgumentNullException(nameof(subscribe));
		}

		public IDisposable Subscribe(IObserver<T> observer) {
			return _subscribe(observer);
		}
	}
}
