using System.Collections.Generic;

namespace File.System.Stub.IO
{
	internal interface IReadOperation
	{
		IEnumerable<IReadOperation> DoOperation();
	}
}
