// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.PlayerDataStorage
{
	/// <summary>
	/// Metadata information for a specific file
	/// </summary>
	public struct FileMetadata
	{
		/// <summary>
		/// The total size of the file in bytes (Includes file header in addition to file contents)
		/// </summary>
		public uint FileSizeBytes { get; set; }

		/// <summary>
		/// The MD5 Hash of the entire file (including additional file header), in hex digits
		/// </summary>
		public Utf8String MD5Hash { get; set; }

		/// <summary>
		/// The file's name
		/// </summary>
		public Utf8String Filename { get; set; }

		/// <summary>
		/// The POSIX timestamp when the file was saved last time.
		/// </summary>
		public System.DateTimeOffset? LastModifiedTime { get; set; }

		/// <summary>
		/// The size of data (payload) in file in unencrypted (original) form.
		/// </summary>
		public uint UnencryptedDataSizeBytes { get; set; }

		internal void Set(ref FileMetadataInternal other)
		{
			FileSizeBytes = other.FileSizeBytes;
			MD5Hash = other.MD5Hash;
			Filename = other.Filename;
			LastModifiedTime = other.LastModifiedTime;
			UnencryptedDataSizeBytes = other.UnencryptedDataSizeBytes;
		}
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct FileMetadataInternal : IGettable<FileMetadata>, ISettable<FileMetadata>, System.IDisposable
	{
		private int m_ApiVersion;
		private uint m_FileSizeBytes;
		private System.IntPtr m_MD5Hash;
		private System.IntPtr m_Filename;
		private long m_LastModifiedTime;
		private uint m_UnencryptedDataSizeBytes;

		public uint FileSizeBytes
		{
			get
			{
				return m_FileSizeBytes;
			}

			set
			{
				m_FileSizeBytes = value;
			}
		}

		public Utf8String MD5Hash
		{
			get
			{
				Utf8String value;
				Helper.Get(m_MD5Hash, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_MD5Hash);
			}
		}

		public Utf8String Filename
		{
			get
			{
				Utf8String value;
				Helper.Get(m_Filename, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_Filename);
			}
		}

		public System.DateTimeOffset? LastModifiedTime
		{
			get
			{
				System.DateTimeOffset? value;
				Helper.Get(m_LastModifiedTime, out value);
				return value;
			}

			set
			{
				Helper.Set(value, ref m_LastModifiedTime);
			}
		}

		public uint UnencryptedDataSizeBytes
		{
			get
			{
				return m_UnencryptedDataSizeBytes;
			}

			set
			{
				m_UnencryptedDataSizeBytes = value;
			}
		}

		public void Set(ref FileMetadata other)
		{
			m_ApiVersion = PlayerDataStorageInterface.FilemetadataApiLatest;
			FileSizeBytes = other.FileSizeBytes;
			MD5Hash = other.MD5Hash;
			Filename = other.Filename;
			LastModifiedTime = other.LastModifiedTime;
			UnencryptedDataSizeBytes = other.UnencryptedDataSizeBytes;
		}

		public void Set(ref FileMetadata? other)
		{
			if (other.HasValue)
			{
				m_ApiVersion = PlayerDataStorageInterface.FilemetadataApiLatest;
				FileSizeBytes = other.Value.FileSizeBytes;
				MD5Hash = other.Value.MD5Hash;
				Filename = other.Value.Filename;
				LastModifiedTime = other.Value.LastModifiedTime;
				UnencryptedDataSizeBytes = other.Value.UnencryptedDataSizeBytes;
			}
		}

		public void Dispose()
		{
			Helper.Dispose(ref m_MD5Hash);
			Helper.Dispose(ref m_Filename);
		}

		public void Get(out FileMetadata output)
		{
			output = new FileMetadata();
			output.Set(ref this);
		}
	}
}