namespace TinyRx {
	public static class ObservablePropertyExtensions {
		public static ReadOnlyObservableProperty<T> ToReadOnly<T>(this IObservableProperty<T> source) {
			return new ReadOnlyObservableProperty<T>(source);
		}
	}
}
