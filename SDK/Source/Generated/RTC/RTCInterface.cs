// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.RTC
{
	public sealed partial class RTCInterface : Handle
	{
		public RTCInterface()
		{
		}

		public RTCInterface(System.IntPtr innerHandle) : base(innerHandle)
		{
		}

		/// <summary>
		/// The most recent version of the <see cref="AddNotifyDisconnected" /> API.
		/// </summary>
		public const int AddnotifydisconnectedApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="AddNotifyParticipantStatusChanged" /> API.
		/// </summary>
		public const int AddnotifyparticipantstatuschangedApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="BlockParticipant" /> API.
		/// </summary>
		public const int BlockparticipantApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="JoinRoom" /> API.
		/// </summary>
		public const int JoinroomApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="LeaveRoom" /> API.
		/// </summary>
		public const int LeaveroomApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="ParticipantMetadata" /> struct.
		/// </summary>
		public const int ParticipantmetadataApiLatest = 1;

		public const int ParticipantmetadataKeyMaxcharcount = 256;

		public const int ParticipantmetadataValueMaxcharcount = 256;

		/// <summary>
		/// The most recent version of the <see cref="SetRoomSetting" /> API.
		/// </summary>
		public const int SetroomsettingApiLatest = 1;

		/// <summary>
		/// The most recent version of the <see cref="SetSetting" /> API.
		/// </summary>
		public const int SetsettingApiLatest = 1;

		/// <summary>
		/// Register to receive notifications when disconnected from the room. If the returned NotificationId is valid, you must call
		/// <see cref="RemoveNotifyDisconnected" /> when you no longer wish to have your CompletionDelegate called.
		/// 
		/// This function will always return <see cref="Common.InvalidNotificationid" /> when used with lobby RTC room. To be notified of the connection
		/// status of a Lobby-managed RTC room, use the <see cref="Lobby.LobbyInterface.AddNotifyRTCRoomConnectionChanged" /> function instead.
		/// <seealso cref="Common.InvalidNotificationid" />
		/// <seealso cref="RemoveNotifyDisconnected" />
		/// </summary>
		/// <param name="clientData">Arbitrary data that is passed back in the CompletionDelegate</param>
		/// <param name="completionDelegate">The callback to be fired when a presence change occurs</param>
		/// <returns>
		/// Notification ID representing the registered callback if successful, an invalid NotificationId if not
		/// </returns>
		public ulong AddNotifyDisconnected(ref AddNotifyDisconnectedOptions options, object clientData, OnDisconnectedCallback completionDelegate)
		{
			AddNotifyDisconnectedOptionsInternal optionsInternal = new AddNotifyDisconnectedOptionsInternal();
			optionsInternal.Set(ref options);

			var clientDataAddress = System.IntPtr.Zero;

			var completionDelegateInternal = new OnDisconnectedCallbackInternal(OnDisconnectedCallbackInternalImplementation);
			Helper.AddCallback(out clientDataAddress, clientData, completionDelegate, completionDelegateInternal);

			var funcResult = Bindings.EOS_RTC_AddNotifyDisconnected(InnerHandle, ref optionsInternal, clientDataAddress, completionDelegateInternal);

			Helper.Dispose(ref optionsInternal);

			Helper.AssignNotificationIdToCallback(clientDataAddress, funcResult);

			return funcResult;
		}

		/// <summary>
		/// Register to receive notifications when a participant's status changes (e.g: join or leave the room), or when the participant is added or removed
		/// from an applicable block list (e.g: Epic block list and/or current platform's block list).
		/// If the returned NotificationId is valid, you must call <see cref="RemoveNotifyParticipantStatusChanged" /> when you no longer wish to have your CompletionDelegate called.
		/// 
		/// If you register to this notification before joining a room, you will receive a notification for every member already in the room when you join said room.
		/// This allows you to know who is already in the room when you join.
		/// 
		/// To be used effectively with a Lobby-managed RTC room, this should be registered during the <see cref="Lobby.LobbyInterface.CreateLobby" /> or <see cref="Lobby.LobbyInterface.JoinLobby" /> completion
		/// callbacks when the ResultCode is <see cref="Result.Success" />. If this notification is registered after that point, it is possible to miss notifications for
		/// already-existing room participants.
		/// 
		/// You can use this notification to detect internal automatic RTC blocks due to block lists.
		/// When a participant joins a room and while the system resolves the block list status of said participant, the participant is set to blocked and you'll receive
		/// a notification with ParticipantStatus set to <see cref="RTCParticipantStatus.Joined" /> and bParticipantInBlocklist set to true.
		/// Once the block list status is resolved, if the player is not in any applicable block list(s), it is then unblocked a new notification is sent with
		/// ParticipantStatus set to <see cref="RTCParticipantStatus.Joined" /> and bParticipantInBlocklist set to false.
		/// This notification is also raised when the local user joins the room, but NOT when the local user leaves the room.
		/// <seealso cref="Common.InvalidNotificationid" />
		/// <seealso cref="RemoveNotifyParticipantStatusChanged" />
		/// </summary>
		/// <param name="clientData">Arbitrary data that is passed back in the CompletionDelegate</param>
		/// <param name="completionDelegate">The callback to be fired when a presence change occurs</param>
		/// <returns>
		/// Notification ID representing the registered callback if successful, an invalid NotificationId if not
		/// </returns>
		public ulong AddNotifyParticipantStatusChanged(ref AddNotifyParticipantStatusChangedOptions options, object clientData, OnParticipantStatusChangedCallback completionDelegate)
		{
			AddNotifyParticipantStatusChangedOptionsInternal optionsInternal = new AddNotifyParticipantStatusChangedOptionsInternal();
			optionsInternal.Set(ref options);

			var clientDataAddress = System.IntPtr.Zero;

			var completionDelegateInternal = new OnParticipantStatusChangedCallbackInternal(OnParticipantStatusChangedCallbackInternalImplementation);
			Helper.AddCallback(out clientDataAddress, clientData, completionDelegate, completionDelegateInternal);

			var funcResult = Bindings.EOS_RTC_AddNotifyParticipantStatusChanged(InnerHandle, ref optionsInternal, clientDataAddress, completionDelegateInternal);

			Helper.Dispose(ref optionsInternal);

			Helper.AssignNotificationIdToCallback(clientDataAddress, funcResult);

			return funcResult;
		}

		/// <summary>
		/// Use this function to block a participant already connected to the room. After blocking them no media will be sent or received between
		/// that user and the local user. This method can be used after receiving the OnParticipantStatusChanged notification.
		/// </summary>
		/// <param name="options">structure containing the parameters for the operation.</param>
		/// <param name="clientData">Arbitrary data that is passed back in the CompletionDelegate</param>
		/// <param name="completionDelegate">a callback that is fired when the async operation completes, either successfully or in error</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the operation succeeded
		/// <see cref="Result.InvalidParameters" /> if any of the parameters are incorrect
		/// <see cref="Result.NotFound" /> if either the local user or specified participant are not in the specified room
		/// <see cref="Result.UserIsInBlocklist" /> The user is in one of the platform's applicable block lists and thus an RTC unblock is not allowed.
		/// </returns>
		public void BlockParticipant(ref BlockParticipantOptions options, object clientData, OnBlockParticipantCallback completionDelegate)
		{
			BlockParticipantOptionsInternal optionsInternal = new BlockParticipantOptionsInternal();
			optionsInternal.Set(ref options);

			var clientDataAddress = System.IntPtr.Zero;

			var completionDelegateInternal = new OnBlockParticipantCallbackInternal(OnBlockParticipantCallbackInternalImplementation);
			Helper.AddCallback(out clientDataAddress, clientData, completionDelegate, completionDelegateInternal);

			Bindings.EOS_RTC_BlockParticipant(InnerHandle, ref optionsInternal, clientDataAddress, completionDelegateInternal);

			Helper.Dispose(ref optionsInternal);
		}

		/// <summary>
		/// Get a handle to the Audio interface
		/// eos_rtc_audio.h
		/// eos_rtc_audio_types.h
		/// </summary>
		/// <returns>
		/// <see cref="RTCAudio.RTCAudioInterface" /> handle
		/// </returns>
		public RTCAudio.RTCAudioInterface GetAudioInterface()
		{
			var funcResult = Bindings.EOS_RTC_GetAudioInterface(InnerHandle);

			RTCAudio.RTCAudioInterface funcResultReturn;
			Helper.Get(funcResult, out funcResultReturn);
			return funcResultReturn;
		}

		/// <summary>
		/// Use this function to join a room.
		/// 
		/// This function does not need to called for the Lobby RTC Room system; doing so will return <see cref="Result.AccessDenied" />. The lobby system will
		/// automatically join and leave RTC Rooms for all lobbies that have RTC rooms enabled.
		/// </summary>
		/// <param name="options">structure containing the parameters for the operation.</param>
		/// <param name="clientData">Arbitrary data that is passed back in the CompletionDelegate</param>
		/// <param name="completionDelegate">a callback that is fired when the async operation completes, either successfully or in error</param>
		public void JoinRoom(ref JoinRoomOptions options, object clientData, OnJoinRoomCallback completionDelegate)
		{
			JoinRoomOptionsInternal optionsInternal = new JoinRoomOptionsInternal();
			optionsInternal.Set(ref options);

			var clientDataAddress = System.IntPtr.Zero;

			var completionDelegateInternal = new OnJoinRoomCallbackInternal(OnJoinRoomCallbackInternalImplementation);
			Helper.AddCallback(out clientDataAddress, clientData, completionDelegate, completionDelegateInternal);

			Bindings.EOS_RTC_JoinRoom(InnerHandle, ref optionsInternal, clientDataAddress, completionDelegateInternal);

			Helper.Dispose(ref optionsInternal);
		}

		/// <summary>
		/// Use this function to leave a room and clean up all the resources associated with it. This function has to always be called when the
		/// room is abandoned even if the user is already disconnected for other reasons.
		/// 
		/// This function does not need to called for the Lobby RTC Room system; doing so will return <see cref="Result.AccessDenied" />. The lobby system will
		/// automatically join and leave RTC Rooms for all lobbies that have RTC rooms enabled.
		/// </summary>
		/// <param name="options">structure containing the parameters for the operation.</param>
		/// <param name="clientData">Arbitrary data that is passed back in the CompletionDelegate</param>
		/// <param name="completionDelegate">a callback that is fired when the async operation completes, either successfully or in error</param>
		/// <returns>
		/// <see cref="Result.Success" /> if the operation succeeded
		/// <see cref="Result.InvalidParameters" /> if any of the parameters are incorrect
		/// <see cref="Result.NotFound" /> if not in the specified room
		/// </returns>
		public void LeaveRoom(ref LeaveRoomOptions options, object clientData, OnLeaveRoomCallback completionDelegate)
		{
			LeaveRoomOptionsInternal optionsInternal = new LeaveRoomOptionsInternal();
			optionsInternal.Set(ref options);

			var clientDataAddress = System.IntPtr.Zero;

			var completionDelegateInternal = new OnLeaveRoomCallbackInternal(OnLeaveRoomCallbackInternalImplementation);
			Helper.AddCallback(out clientDataAddress, clientData, completionDelegate, completionDelegateInternal);

			Bindings.EOS_RTC_LeaveRoom(InnerHandle, ref optionsInternal, clientDataAddress, completionDelegateInternal);

			Helper.Dispose(ref optionsInternal);
		}

		/// <summary>
		/// Unregister a previously bound notification handler from receiving room disconnection notifications
		/// </summary>
		/// <param name="notificationId">The Notification ID representing the registered callback</param>
		public void RemoveNotifyDisconnected(ulong notificationId)
		{
			Bindings.EOS_RTC_RemoveNotifyDisconnected(InnerHandle, notificationId);

			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		/// <summary>
		/// Unregister a previously bound notification handler from receiving participant status change notifications
		/// </summary>
		/// <param name="notificationId">The Notification ID representing the registered callback</param>
		public void RemoveNotifyParticipantStatusChanged(ulong notificationId)
		{
			Bindings.EOS_RTC_RemoveNotifyParticipantStatusChanged(InnerHandle, notificationId);

			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		/// <summary>
		/// Use this function to control settings for the specific room.
		/// 
		/// The available settings are documented as part of <see cref="SetRoomSettingOptions" />.
		/// </summary>
		/// <param name="options">structure containing the parameters for the operation</param>
		/// <returns>
		/// <see cref="Result.Success" /> when the setting is successfully set, <see cref="Result.NotFound" /> when the setting is unknown, <see cref="Result.InvalidParameters" /> when the value is invalid.
		/// </returns>
		public Result SetRoomSetting(ref SetRoomSettingOptions options)
		{
			SetRoomSettingOptionsInternal optionsInternal = new SetRoomSettingOptionsInternal();
			optionsInternal.Set(ref options);

			var funcResult = Bindings.EOS_RTC_SetRoomSetting(InnerHandle, ref optionsInternal);

			Helper.Dispose(ref optionsInternal);

			return funcResult;
		}

		/// <summary>
		/// Use this function to control settings.
		/// 
		/// The available settings are documented as part of <see cref="SetSettingOptions" />.
		/// </summary>
		/// <param name="options">structure containing the parameters for the operation</param>
		/// <returns>
		/// <see cref="Result.Success" /> when the setting is successfully set, <see cref="Result.NotFound" /> when the setting is unknown, <see cref="Result.InvalidParameters" /> when the value is invalid.
		/// </returns>
		public Result SetSetting(ref SetSettingOptions options)
		{
			SetSettingOptionsInternal optionsInternal = new SetSettingOptionsInternal();
			optionsInternal.Set(ref options);

			var funcResult = Bindings.EOS_RTC_SetSetting(InnerHandle, ref optionsInternal);

			Helper.Dispose(ref optionsInternal);

			return funcResult;
		}

		[MonoPInvokeCallback(typeof(OnBlockParticipantCallbackInternal))]
		internal static void OnBlockParticipantCallbackInternalImplementation(ref BlockParticipantCallbackInfoInternal data)
		{
			OnBlockParticipantCallback callback;
			BlockParticipantCallbackInfo callbackInfo;
			if (Helper.TryGetAndRemoveCallback(ref data, out callback, out callbackInfo))
			{
				callback(ref callbackInfo);
			}
		}

		[MonoPInvokeCallback(typeof(OnDisconnectedCallbackInternal))]
		internal static void OnDisconnectedCallbackInternalImplementation(ref DisconnectedCallbackInfoInternal data)
		{
			OnDisconnectedCallback callback;
			DisconnectedCallbackInfo callbackInfo;
			if (Helper.TryGetAndRemoveCallback(ref data, out callback, out callbackInfo))
			{
				callback(ref callbackInfo);
			}
		}

		[MonoPInvokeCallback(typeof(OnJoinRoomCallbackInternal))]
		internal static void OnJoinRoomCallbackInternalImplementation(ref JoinRoomCallbackInfoInternal data)
		{
			OnJoinRoomCallback callback;
			JoinRoomCallbackInfo callbackInfo;
			if (Helper.TryGetAndRemoveCallback(ref data, out callback, out callbackInfo))
			{
				callback(ref callbackInfo);
			}
		}

		[MonoPInvokeCallback(typeof(OnLeaveRoomCallbackInternal))]
		internal static void OnLeaveRoomCallbackInternalImplementation(ref LeaveRoomCallbackInfoInternal data)
		{
			OnLeaveRoomCallback callback;
			LeaveRoomCallbackInfo callbackInfo;
			if (Helper.TryGetAndRemoveCallback(ref data, out callback, out callbackInfo))
			{
				callback(ref callbackInfo);
			}
		}

		[MonoPInvokeCallback(typeof(OnParticipantStatusChangedCallbackInternal))]
		internal static void OnParticipantStatusChangedCallbackInternalImplementation(ref ParticipantStatusChangedCallbackInfoInternal data)
		{
			OnParticipantStatusChangedCallback callback;
			ParticipantStatusChangedCallbackInfo callbackInfo;
			if (Helper.TryGetAndRemoveCallback(ref data, out callback, out callbackInfo))
			{
				callback(ref callbackInfo);
			}
		}
	}
}