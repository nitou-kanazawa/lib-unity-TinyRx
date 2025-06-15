using System;

namespace TinyRx {

	internal sealed class AnonymousDisposable : IDisposable {

		private bool _isDisposed;
		private readonly Action _disposeAction;

		public AnonymousDisposable(Action action) {
			_disposeAction = action;
		}

		public void Dispose() {
			if (_isDisposed)
				return;

			_isDisposed = true;
			_disposeAction?.Invoke();
		}
	}


	internal sealed class AnonymousDisposable<T> : IDisposable {
		private bool _isDisposed = false;
		private readonly T _state;
		private readonly Action<T> _disposeAction;

		public AnonymousDisposable(T state, Action<T> dispose) {
			_state = state;
			_disposeAction = dispose;
		}

		public void Dispose() {
			if (_isDisposed)
				return;

			_isDisposed = true;
			_disposeAction?.Invoke(_state);
		}
	}
}
