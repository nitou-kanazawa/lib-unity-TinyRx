using System;
using System.Runtime.ExceptionServices;

namespace TinyRx {
	internal class Observer<T> : IObserver<T> {

		private readonly Action<T> _onNext;
		private readonly Action<Exception> _onError;
		private readonly Action _onCompleted;

		public Observer(Action<T> onNext, Action<Exception> onError, Action onCompleted) {
			_onNext = onNext;
			_onError = onError;
			_onCompleted = onCompleted;
		}

		public void OnNext(T value) {
			_onNext?.Invoke(value);
		}

		public void OnCompleted() {
			_onCompleted?.Invoke();
		}

		public void OnError(Exception error) {
			if (_onError != null) {
				_onError?.Invoke(error);
			}
			else {
				ExceptionDispatchInfo.Capture(error).Throw();
			}
		}
	}
}
