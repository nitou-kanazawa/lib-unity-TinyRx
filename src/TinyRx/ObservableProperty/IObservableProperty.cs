using System;

namespace TinyRx {
	public interface IObservableProperty<T> : IObservable<T>, IDisposable {

		/// <summary>
		/// Current value.
		/// </summary>
		T Value { get; set; }

		/// <summary>
		/// Set a value and force nortify.
		/// </summary>
		/// <param name="value">new value</param>
		void SetValueAndNotify(T value);
	}


	public interface IReadOnlyObservebleProperty<T> : IObservable<T>, IDisposable {

		/// <summary>
		/// Current value.
		/// </summary>
		T Value { get; }
	}
}

