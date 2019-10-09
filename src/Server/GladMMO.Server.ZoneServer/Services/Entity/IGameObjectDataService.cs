using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public interface IGameObjectDataService
	{
		/// <summary>
		/// Loads all the GameObject data async.
		/// This may have to make many remote service calls.
		/// </summary>
		/// <returns>An awaitable.</returns>
		Task LoadDataAsync();

		/// <summary>
		/// All loaded <see cref="GameObjectTemplateModel"/>s.
		/// This will be empty if <see cref="LoadDataAsync"/> is not called.
		/// </summary>
		IReadonlyEntityGuidMappable<GameObjectTemplateModel> GameObjectTemplateMappable { get; }

		/// <summary>
		/// All loaded <see cref="GameObjectInstanceModel"/>s.
		/// This will be empty if <see cref="LoadDataAsync"/> is not called.
		/// </summary>
		IReadonlyEntityGuidMappable<GameObjectInstanceModel> GameObjectInstanceMappable { get; }

		/// <summary>
		/// Gets a specified GameObject behaviour instance.
		/// </summary>
		/// <typeparam name="TBehaviourType"></typeparam>
		/// <param name="entityGuid"></param>
		/// <returns></returns>
		TBehaviourType GetBehaviourInstanceData<TBehaviourType>(NetworkEntityGuid entityGuid)
			where TBehaviourType : IGameObjectLinkable;
	}
}
