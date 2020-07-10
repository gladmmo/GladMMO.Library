using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	class Program
	{
		static void Main(string[] args)
		{
			BuildRepositories<wotlk_worldContext>();
			BuildRepositories<wotlk_charactersContext>();
		}

		private static void BuildRepositories<TDbContextType>()
			where TDbContextType : DbContext
		{
			string databaseName = typeof(TDbContextType).Name
				.Replace("wotlk_", "")
				.Replace("Context", "");

			char[] nameArray = databaseName.ToCharArray();
			nameArray[0] = Char.ToUpper(nameArray[0]);
			databaseName = new string(nameArray);

			GenerateInterfaces<TDbContextType>(databaseName);
			GenerateImplementationClasses<TDbContextType>(databaseName);
		}

		private static void GenerateInterfaces<TDbContextType>(string databaseName) where TDbContextType : DbContext
		{
			string outputPath = Path.Combine(Directory.GetCurrentDirectory(), databaseName, "RepositoryInterfaces");

			if (!Directory.Exists(outputPath))
				Directory.CreateDirectory(outputPath);

			//Generate the Repository Interfaces
			foreach (Type model in IterateDbSetModelTypes<TDbContextType>())
			{
				//First prop is probably the primary key or something.
				PropertyInfo keyProp = model.GetProperties().OrderBy(x => x.MetadataToken).First();
				string interfaceFileData = BuildInterfaceFileTemplate(model, keyProp.PropertyType);

				string repoName = $"ITrinity{model.Name}Repository";

				//Write the packet file out
				File.WriteAllText(Path.Combine(outputPath, $"{repoName}.cs"), interfaceFileData);
			}
		}

		private static void GenerateImplementationClasses<TDbContextType>(string databaseName) where TDbContextType : DbContext
		{
			string outputPath = Path.Combine(Directory.GetCurrentDirectory(), databaseName, "Repositories");

			if (!Directory.Exists(outputPath))
				Directory.CreateDirectory(outputPath);

			//ImplementationFileTemplate

			//Generate the Repository Interfaces
			foreach(Type model in IterateDbSetModelTypes<TDbContextType>())
			{
				//First prop is probably the primary key or something.
				PropertyInfo keyProp = model.GetProperties().OrderBy(x => x.MetadataToken).First();
				string implementationFileData = BuildImplementationFileTemplate(model, keyProp.PropertyType, typeof(TDbContextType));

				string implementationName = $"TrinityCore{model.Name}Repository";

				//Write the packet file out
				File.WriteAllText(Path.Combine(outputPath, $"{implementationName}.cs"), implementationFileData);
			}
		}

		public static IEnumerable<Type> IterateDbSetModelTypes<TDbContextType>()
			where TDbContextType : DbContext
		{
			PropertyInfo[] properties = typeof(TDbContextType).GetProperties();

			foreach(var property in properties)
			{
				if (!property.PropertyType.IsGenericType)
					continue;

				if (property.PropertyType.GetGenericTypeDefinition() != typeof(DbSet<>))
					continue;

				Type modelType = property.PropertyType.GenericTypeArguments.First();
				yield return modelType;
			}
		}

		public const string InterfaceFileTemplate = 
			@"using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public partial interface ITrinity{name}Repository : IGenericRepositoryCrudable<{key}, {name}>
	{

	}
}";

		public const string ImplementationFileTemplate = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed partial class TrinityCore{name}Repository : BaseGenericBackedDatabaseRepository<{dbtype}, {key}, {name}>, ITrinity{name}Repository
	{
		public TrinityCore{name}Repository([JetBrains.Annotations.NotNull] {dbtype} context) 
			: base(context)
		{

		}
	}
}";

		public static string BuildInterfaceFileTemplate(Type modelType, Type keyType)
		{
			return InterfaceFileTemplate
				.Replace("{name}", modelType.Name)
				.Replace("{key}", keyType.Name);
		}

		public static string BuildImplementationFileTemplate(Type modelType, Type keyType, Type dbType)
		{
			return ImplementationFileTemplate
				.Replace("{name}", modelType.Name)
				.Replace("{key}", keyType.Name)
				.Replace("{dbtype}", dbType.Name);
		}
	}
}
