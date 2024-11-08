﻿using System.ComponentModel.DataAnnotations;

namespace FinanceWebSite.API.Dtos.Account
{
	public class LoginDto
	{
		[Required]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }
	}
}