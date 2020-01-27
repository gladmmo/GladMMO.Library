using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// Base type for managed response models.
	/// </summary>
	/// <typeparam name="TModelType">The response model type.</typeparam>
	/// <typeparam name="TResponseCodeType">The response code type.</typeparam>
	[JsonObject]
	public sealed class ResponseModel<TModelType, TResponseCodeType> : IResponseModel<TResponseCodeType>, ISucceedable 
		where TResponseCodeType : Enum
		where TModelType : class
	{
		[JsonProperty]
		public TResponseCodeType ResultCode { get; private set; }

		//This assumes that success is ALWAYS equal to 1.
		[JsonIgnore]
		public bool isSuccessful => ConvertResponseCodeToInt() == ModelsCommonConstants.RESPONSE_CODE_SUCCESS_VALUE;

		[JsonProperty]
		public TModelType Result { get; private set; }

		/// <summary>
		/// Creates a failed <see cref="ResponseModel{TModelType,TResponseCodeType}"/> with the specified
		/// response code <see cref="ResultCode"/>
		/// </summary>
		/// <param name="resultCode">The non-Success response code.</param>
		public ResponseModel(TResponseCodeType resultCode)
		{
			//order is this way for conversion purposes
			ResultCode = resultCode;
			if(!Enum.IsDefined(typeof(TResponseCodeType), resultCode)) throw new InvalidEnumArgumentException(nameof(resultCode), ConvertResponseCodeToInt(), typeof(TResponseCodeType));
			
			//This is the failure ctor so we also check if it's successful
			if(isSuccessful)
				throw new ArgumentException($"Cannot initialize {nameof(resultCode)} with {resultCode}/Success when creating a failure response model",nameof(resultCode));
		}

		public ResponseModel([NotNull] TModelType result)
		{
			Result = result ?? throw new ArgumentNullException(nameof(result));
			ResultCode = Generic.Math.GenericMath.Convert<int, TResponseCodeType>(ModelsCommonConstants.RESPONSE_CODE_SUCCESS_VALUE);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private int ConvertResponseCodeToInt()
		{
			//TODO: Avoid doing this on other non-Unity3D platforms.
			//We have to do this hacky workaround because of AOT issues
			//that cannot be resolved with fake references.
			return (int)(object)ResultCode;
			//return Generic.Math.GenericMath.Convert<TResponseCodeType, int>(ResultCode);
		}

		[JsonConstructor]
		private ResponseModel()
		{
			if (isSuccessful && Result == null)
				throw new InvalidOperationException($"Received successful {typeof(TModelType).Name} Response Model BUT the {nameof(Result)} field was null.");
		}
	}
}
