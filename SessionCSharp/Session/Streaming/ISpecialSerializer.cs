namespace Session.Streaming
{
	public interface ISpecialSerializer : ISerializer
	{
		public bool CanSerialize<T>(T value);
	}
}
