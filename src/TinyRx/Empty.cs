using System;

namespace TinyRx {
	[Serializable]
	public readonly struct Empty : IEquatable<Empty> {
		public bool Equals(Empty other) => true;
		public override bool Equals(object obj) => obj is Empty;
		public override int GetHashCode() => 0;


		public static Empty Default { get; } = new();
		public static bool operator ==(Empty left, Empty right) => true;
		public static bool operator !=(Empty left, Empty right) => false;
	}
}
