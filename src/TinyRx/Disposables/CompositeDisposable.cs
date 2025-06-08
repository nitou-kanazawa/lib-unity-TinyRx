using System;
using System.Collections.Generic;

namespace TinyRx {

	internal sealed class CompositeDisposable : IDisposable {

		private bool _isDisposed = false;
		private readonly List<IDisposable> _disposables;

		public int Count => _disposables.Count;
		public bool IsDisposed => _isDisposed;

		public bool IsReadOnly => throw new NotImplementedException();

		public CompositeDisposable() {
			_disposables = new();
		}

		public CompositeDisposable(int capacity) {
			if (capacity < 0)
				throw new ArgumentOutOfRangeException("Capacity must be positive number.");

			_disposables = new(capacity);
		}

		public void Dispose() {
			Clear();
			_isDisposed = true;
		}

		public void Add(IDisposable item) {
			if (item == null)
				throw new ArgumentNullException(nameof(item));

			if (_isDisposed) {
				item.Dispose();
				return;
			}

			_disposables.Add(item);
		}

		public bool Remove(IDisposable item) {
			if (item == null)
				throw new ArgumentNullException(nameof(item));

			if (_isDisposed) {
				return false;
			}

			item.Dispose();
			_disposables.Remove(item);
			return true;
		}

		public void Clear() {
			foreach (var disposable in _disposables)
				disposable.Dispose();

			_disposables.Clear();
		}

		public bool Contains(IDisposable item) {
			if (item == null)
				throw new ArgumentNullException(nameof(item));

			return _disposables.Contains(item);
		}
	}
}
