using System.Collections.Generic;

namespace File.System.Stub.IO
{
	public interface IReadOperation
	{
		DirectoryStub CurrentStub { get; }
		IEnumerable<IReadOperation> DoOperation();
	}
}
