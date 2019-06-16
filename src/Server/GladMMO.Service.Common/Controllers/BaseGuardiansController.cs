using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	/// <summary>
	/// The base controller Type for guardians ASP MVC controllers.
	/// </summary>
	public abstract class BaseGuardiansController : Controller
	{
		/// <summary>
		/// The logging service for the controller.
		/// </summary>
		protected ILogger<BaseGuardiansController> Logger { get; }

		/// <inheritdoc />
		protected BaseGuardiansController([FromServices] ILogger<BaseGuardiansController> logger)
		{
			if(logger == null) throw new ArgumentNullException(nameof(logger));

			Logger = logger;
		}

		//TODO: Fix doc
		/// <summary>
		/// Builds a successful JsonResult of ResponseModel
		/// with a successful result code an the value of result.
		/// </summary>
		/// <typeparam name="TModelType">The model type.</typeparam>
		/// <param name="result">The result.</param>
		/// <returns></returns>
		protected JsonResult BuildSuccessfulResponseModel<TModelType>([JetBrains.Annotations.NotNull] TModelType result)
			where TModelType : class
		{
			if (result == null) throw new ArgumentNullException(nameof(result));
			return Json(new ResponseModel<TModelType, StubbedResponseCode>(result)); //We use a stubbed response code since we don't actually known the result.
		}

		/// <summary>
		/// TODO: Fix doc, intellisense not working as I'm writing this.
		/// </summary>
		/// <typeparam name="TResponseCodeType">The response code type.</typeparam>
		/// <param name="failedResponseCode">The failure code.</param>
		/// <returns></returns>
		protected JsonResult BuildFailedResponseModel<TResponseCodeType>([JetBrains.Annotations.NotNull] TResponseCodeType failedResponseCode)
			where TResponseCodeType : Enum
		{
			//TODO: add enum check.
			return Json(new ResponseModel<object, TResponseCodeType>(failedResponseCode)); //We use a stubbed response code since we don't actually known the result.
		}
	}
}
