using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	//TODO: We need proper handling for this eventually.
	public sealed class ZoneServerAuthenticationTokenRepository : IReadonlyAuthTokenRepository, IAuthTokenRepository
	{
		private string CurrentTokenValue = @"eyJhbGciOiJSUzI1NiIsImtpZCI6IjYyN0YyQUFDMTZERTlENjNDMkY3NDQyQzk1OUFBNjEyQjIyOTlENDciLCJ0eXAiOiJKV1QifQ.eyJzdWIiOiIyIiwibmFtZSI6IkFkbWluIiwicGZpZCI6IkI3RjM0OUNDMkVGQjc0RiIsInRva2VuX3VzYWdlIjoiYWNjZXNzX3Rva2VuIiwianRpIjoiMjM0NThiMzUtM2ExNC00MDFjLTg5NDctZTE2Y2JiYzg3NTE0Iiwic2NvcGUiOiJvcGVuaWQiLCJhdWQiOiJhdXRoLXNlcnZlciIsImF6cCI6IlZSR3VhcmRpYW5zQXV0aGVudGljYXRpb24iLCJuYmYiOjE1NjkyMDg1MzIsImV4cCI6MTU2OTIxMjEzMiwiaWF0IjoxNTY5MjA4NTMyLCJpc3MiOiJodHRwczovL2F1dGgudnJndWFyZGlhbnMubmV0LyJ9.LajIzE4T0zhrgdulYcSFqOCkBioFr68WtA5m14w7dO8agmlNcl7vlUJ4q-KqtB70hCulOp_UCYQ_12XjmrYoEz-eBJtGP6-fV4WYzK_NKSWLt8l556_f6OEo3lzY4P7XiY44lmjr1NRZrbE3W83oeG0XDpFCT5mnC3yHZ-i_XGbRb4tqE_u5y5mIkooeAVyjMIEf7NSqS6cVrJUyABFgh2u6sXU945tlDZOQTLkIsLVeM1nA313GIjEuJgLngDyZnm_hUmZaB2JsGuUQ0pJURTTvqErg-EivU0WMSL0wYMmS-S8YBX1nQKmLXgeudB0H7p7gGlIL2iqvMGFZUaRBN3xzjZMLW4RqjSJabHC4m4tuDDDC6d-d2xc4u6H1fsQs2kaX-p-MauURRhEFsCK7oIEj74TDNJbOoNKo_btNkCkrpAL2LFF1uUycRSBsviuH_rgsUVBxap3LHcdrJrP0eQITw4vgAeeCKpca5lr-mOKX-cbs3bQ22mL6pTY7SEkjed2KAVFRj00t2dXxaeNlqfQDz7yoadqeLduYk6YDorE12ntu396LOvmur6AUGLVgrixVZMdFtIO8A1MrFtY5yYuSfC7Myop_n0Iqh0jTVtyBiAZxvPDNtZvh6BgUlI4M0r2fMZU4s0eCvlkUjQLUebiXcIo8p22kTimCD3jeSFM";

		public string Retrieve()
		{
			return CurrentTokenValue;
		}

		public string RetrieveWithType()
		{
			return $"{RetrieveType()} {Retrieve()}";
		}

		public string RetrieveType()
		{
			return "Bearer";
		}

		public void Update([NotNull] string authToken)
		{
			if(string.IsNullOrWhiteSpace(authToken)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(authToken));

			CurrentTokenValue = authToken;
		}
	}
}
