using Newtonsoft.Json;

namespace File.System.Stub
{
	internal static class Serializer
	{
		private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
		{
			PreserveReferencesHandling = PreserveReferencesHandling.All
		};

		public static string Serialize(object o)
		{
			return JsonConvert.SerializeObject(o, Formatting.Indented, SerializerSettings);
		}

		public static T Deserialize<T>(string s)
		{
			return JsonConvert.DeserializeObject<T>(s, SerializerSettings);
		}
	}
}