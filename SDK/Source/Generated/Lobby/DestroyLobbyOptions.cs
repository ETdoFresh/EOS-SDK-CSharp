// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.Lobby
{
	/// <summary>
	/// Input parameters for the <see cref="LobbyInterface.DestroyLobby" /> function.
	/// </summary>
	public struct DestroyLobbyOptions
	{
		/// <summary>
		/// The Product User ID of the local user requesting destruction of the lobby; this user must currently own the lobby
		/// </summary>
		public ProductUserId LocalUserId { get; set; }

		/// <summary>
		/// The ID of the lobby to destroy
		/// </summary>
		public Utf8String LobbyId { get; set; }
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct DestroyLobbyOptionsInternal : ISettable<DestroyLobbyOptions>, System.IDisposable
	{
		private int m_ApiVersion;
		private System.IntPtr m_LocalUserId;
		private System.IntPtr m_LobbyId;

		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref m_LocalUserId);
			}
		}

		public Utf8String LobbyId
		{
			set
			{
				Helper.Set(value, ref m_LobbyId);
			}
		}

		public void Set(ref DestroyLobbyOptions other)
		{
			m_ApiVersion = LobbyInterface.DestroylobbyApiLatest;
			LocalUserId = other.LocalUserId;
			LobbyId = other.LobbyId;
		}

		public void Set(ref DestroyLobbyOptions? other)
		{
			if (other.HasValue)
			{
				m_ApiVersion = LobbyInterface.DestroylobbyApiLatest;
				LocalUserId = other.Value.LocalUserId;
				LobbyId = other.Value.LobbyId;
			}
		}

		public void Dispose()
		{
			Helper.Dispose(ref m_LocalUserId);
			Helper.Dispose(ref m_LobbyId);
		}
	}
}