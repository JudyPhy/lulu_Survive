using System;

namespace EventTransmit {
	[Serializable]
	public class EventException : Exception {
		public EventException(string message)
			: base(message) {
		}

		public EventException(string message, Exception innerException)
			: base(message, innerException) {
		}
	}

}
