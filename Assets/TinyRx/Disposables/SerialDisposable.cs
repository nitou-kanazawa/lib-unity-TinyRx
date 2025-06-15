using System;

namespace TinyRx {

	public sealed class SerialDisposable : IDisposable {
		private bool _isDisposed;
		private IDisposable _current;

		public bool IsDisposed => _isDisposed;


		public IDisposable Disposable {
			get => _current;
			set {

				var old = _current;
				_current = value;

				old?.Dispose();
				if (_isDisposed)
					_current?.Dispose();
			}
		}

		public void Dispose() {
			if (_isDisposed)
				return;

			_isDisposed = true;
			_current?.Dispose();
		}
	}

}
