using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
	public interface ISmsService
	{
		void SendAsync(string phone, string content);
	}
}
