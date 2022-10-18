// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.Lobby
{
	/// <summary>
	/// Output parameters for the <see cref="LobbyInterface.QueryInvites" /> function.
	/// </summary>
	public struct QueryInvitesCallbackInfo : ICallbackInfo
	{
		/// <summary>
		/// The <see cref="Result" /> code for the operation. <see cref="Result.Success" /> indicates that the operation succeeded; other codes indicate errors.
		/// </summary>
		public Result ResultCode { get; set; }

		/// <summary>
		/// Context that was passed into <see cref="LobbyInterface.QueryInvites" />
		/// </summary>
		public object ClientData { get; set; }

		/// <summary>
		/// The Product User ID of the local user that made the request
		/// </summary>
		public ProductUserId LocalUserId { get; set; }

		public Result? GetResultCode()
		{
			return ResultCode;
		}

		internal void Set(ref QueryInvitesCallbackInfoInternal other)
		{
			ResultCode = other.ResultCode;
			ClientData = other.ClientData;
			LocalUserId = other.LocalUserId;
		}
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct QueryInvitesCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryInvitesCallbackInfo>, ISettable<QueryInvitesCallbackInfo>, System.IDisposable
	{
		private Result m_ResultCode;
		private System.IntPtr m_ClientData;
		private System.IntPtr m_LocalUserId;

		public Result ResultCode
		{
			get
			{
				return m_ResultCode;
			}

			set
			{
				m_ResultCode = value;
			}
		}

		public object ClientData
		{
			get
			{
				object value;
				Helper.Get(m_ClientData, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_ClientData);
			}
		}

		public System.IntPtr ClientDataAddress
		{
			get
			{
				return m_ClientData;
			}
		}

		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId value;
				Helper.Get(m_LocalUserId, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_LocalUserId);
			}
		}

		public void Set(ref QueryInvitesCallbackInfo other)
		{
			ResultCode = other.ResultCode;
			ClientData = other.ClientData;
			LocalUserId = other.LocalUserId;
		}

		public void Set(ref QueryInvitesCallbackInfo? other)
		{
			if (other.HasValue)
			{
				ResultCode = other.Value.ResultCode;
				ClientData = other.Value.ClientData;
				LocalUserId = other.Value.LocalUserId;
			}
		}

		public void Dispose()
		{
			Helper.Dispose(ref m_ClientData);
			Helper.Dispose(ref m_LocalUserId);
		}

		public void Get(out QueryInvitesCallbackInfo output)
		{
			output = new QueryInvitesCallbackInfo();
			output.Set(ref this);
		}
	}
}