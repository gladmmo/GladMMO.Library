using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// Base type for managed response models.
	/// </summary>
	/// <typeparam name="TModelType">The response model type.</typeparam>
	/// <typeparam name="TResponseCodeType">The response code type.</typeparam>
	public abstract class ResponseModel<TModelType, TResponseCodeType> : IResponseModel<TResponseCodeType>, ISucceedable 
		where TResponseCodeType : Enum
		where TModelType : class
	{
		[JsonProperty]
		public TResponseCodeType ResultCode { get; private set; }

		//This assumes that success is ALWAYS equal to 1.
		[JsonIgnore]
		public bool isSuccessful => ConvertResponseCodeToInt() == 1;

		[JsonProperty]
		public TModelType Result { get; private set; }

		protected ResponseModel(TResponseCodeType resultCode, [NotNull] TModelType result)
		{
			//order is this way for conversion purposes
			ResultCode = resultCode;
			if(!Enum.IsDefined(typeof(TResponseCodeType), resultCode)) throw new InvalidEnumArgumentException(nameof(resultCode), ConvertResponseCodeToInt(), typeof(TResponseCodeType));
			
			Result = result ?? throw new ArgumentNullException(nameof(result));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private int ConvertResponseCodeToInt()
		{
			return Generic.Math.GenericMath.Convert<TResponseCodeType, int>(ResultCode);
		}

		[JsonConstructor]
		protected ResponseModel()
		{
			if (isSuccessful && Result == null)
				throw new InvalidOperationException($"Received successful {typeof(TModelType).Name} Response Model BUT the {nameof(Result)} field was null.");
		}
	}
}
